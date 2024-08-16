using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProxySwitcher
{
    public class NetworkMonitor
    {
        private string currentSSID; // currentSSID変数を宣言
        private readonly string REG_KEY = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect); //最大化判定用

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)] 
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount); //最大化判定用

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount); //最大化判定用

        // RECT構造体を定義
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public NetworkMonitor()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
        }

        private void AddressChangedCallback(object sender, EventArgs e)
        {
            // SSIDの取得とプロキシ設定の変更
            if (currentSSID != GetCurrentSSID()) // SSIDが前回と変わっていたら（IPのみ変更での誤作動を防ぐ）
            {
                currentSSID = GetCurrentSSID(); // 現在のSSIDをGetCurrentSSIDで取得、currentSSID変数に代入
                ProxyAutoApply(currentSSID); // currentSSIDに基づくプロキシ設定に
                Program.logger.Log("SSIDが変更されました");
            }
        }
        public void CallProxyAutoApply()
        {
            //強制的に現在のSSIDに基づくプロキシ設定に
            currentSSID = GetCurrentSSID();
            ProxyAutoApply(currentSSID);
        }

        public string GetCurrentSSID()
        {
            try
            {
                // netshコマンドを実行してWi-Fiインターフェースの情報を取得
                ProcessStartInfo startInfo = new ProcessStartInfo("netsh", "wlan show interfaces")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process process = Process.Start(startInfo);
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // 出力からSSIDの行を抽出
                string ssidLine = output.Split('\n').FirstOrDefault(line => line.Trim().StartsWith("SSID"));
                if (ssidLine != null)
                {
                    // SSIDの値を抽出して返す
                    return ssidLine.Split(new[] { ':' }, 2).Last().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                // 例外が発生した場合はログに記録し、空の文字列を返す
                Program.logger.Log($"SSIDの取得中に例外が発生しました: {ex.Message}");
                MessageBoxHelper.ShowError($"SSIDの取得中に例外が発生しました: {ex.Message}");
                return string.Empty;
            }
        }

        private void ProxyAutoApply(string ssid)
        {
            //if EnableComfirming == true 確認通知
            if (Program.settings.EnableConfirming == true)
            {
                if (WindowMaximizeDetection() == false) //確認ポップアップフェーズ
                {
                    if (DialogResult.No == MessageBox.Show("SSIDが変更されました。設定を変更しますか？", "プロキシ設定変更確認", MessageBoxButtons.YesNo)) //Noだったら
                    {
                        return; //ApplyProxySettingsを終了
                    }
                }
                else　//ゲーム中などにウィンドウのフォーカスが移動しないようにする
                {
                    while (WindowMaximizeDetection() == true)　//ウィンドウが最大化されている間は待機
                    {
                        Thread.Sleep(10000); // 10秒待つ
                    }


                    if (DialogResult.No == MessageBox.Show("SSIDが変更されました。設定を変更しますか？", "プロキシ設定変更確認", MessageBoxButtons.YesNo)) //Noだったら
                    {
                        return; //ApplyProxySettingsを終了
                    }
                }
            }

            if (Array.Exists(Program.settings.SSIDs, s => s == ssid)) //SSID確認処理フェーズ
            {
                ProxyApply();//適用

                if (Program.settings.ShowNotification == true) // 通知を表示する設定が有効な場合
                {
                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText($"SSID \"{ssid}\" のためのプロキシ設定が適用されました。\n現在起動しているネットワークを使用するアプリケーションがある場合は変更を適用するために再起動してください。")
                        .Show();
                }
            }
            else
            {
                ProxyRemove(); //SSIDが一致しなかったら解除

                if (Program.settings.ShowNotification == true) // 通知を表示する設定が有効な場合
                {
                    if (ssid == "")
                    {
                        new ToastContentBuilder() //SSIDがない場合の通知
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText($"プロキシ設定が解除されました。\n現在起動しているネットワークを使用するアプリケーションがある場合は変更を適用するために再起動してください。")
                        .Show();
                    }
                    else
                    {
                        new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText($"SSID \"{ssid}\" のためにプロキシ設定が解除されました。\n現在起動しているネットワークを使用するアプリケーションがある場合は変更を適用するために再起動してください。")
                        .Show();
                    }
                }
            }
        }

        public void ProxyApply()
        {
            // プロキシを設定するロジック
            try
            {
                // プロキシホストのアドレスを取得し、IPv4アドレスがあるか確認
                var addresses = Dns.GetHostAddresses(Program.settings.ProxyAddress).Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                if (!addresses.Any())
                {
                    throw new Exception("IPv4アドレスが見つかりません");
                }

                // レジストリにプロキシの有効化、プロキシサーバの設定、ローカルおよび除外アドレスの設定を行う
                Registry.SetValue(REG_KEY, "ProxyEnable", 1);
                Registry.SetValue(REG_KEY, "ProxyServer", $"{Program.settings.ProxyAddress}:{Program.settings.ProxyPort}");
                Registry.SetValue(REG_KEY, "ProxyOverride", "LOCAL_ADDR");

                // 環境変数の設定
                string proxyValue = $"http://{Program.settings.ProxyAddress}:{Program.settings.ProxyPort}";
                Environment.SetEnvironmentVariable("HTTP_PROXY", proxyValue, EnvironmentVariableTarget.Process);
                Environment.SetEnvironmentVariable("HTTPS_PROXY", proxyValue, EnvironmentVariableTarget.Process);
                Environment.SetEnvironmentVariable("HTTP_PROXY", proxyValue, EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("HTTPS_PROXY", proxyValue, EnvironmentVariableTarget.User);

                Program.logger.Log("プロキシ設定が適用されました");
            }
            catch (Exception ex)
            {
                ProxyRemove();
                Program.logger.Log($"プロキシ設定が解除されました（例外処理）: {ex.Message}");
            }
        }

        public void ProxyRemove()
        {
            // プロキシを解除するロジック
            Registry.SetValue(REG_KEY, "ProxyEnable", 0);
            Environment.SetEnvironmentVariable("HTTP_PROXY", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("HTTPS_PROXY", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("HTTP_PROXY", null, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("HTTPS_PROXY", null, EnvironmentVariableTarget.User);
            Program.logger.Log("プロキシ設定が解除されました");
        }


        private bool WindowMaximizeDetection()
        {
            // フォアグラウンドウィンドウのハンドルを取得
            IntPtr hWnd = GetForegroundWindow();
            Program.logger.Log("WindowMaximizeDetectionメソッド開始");

            if (hWnd == IntPtr.Zero)
            {
                Program.logger.Log("ウィンドウのハンドルの取得に失敗しました");
                return false;
            }

            // ウィンドウの矩形を取得
            if (GetWindowRect(hWnd, out RECT rect))
            {
                // 画面のサイズを取得
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                // ウィンドウのサイズが画面全体を覆っているか確認
                bool isFullScreen = rect.Left <= 0 && rect.Top <= 0 && rect.Right >= screenWidth && rect.Bottom >= screenHeight;

                if (isFullScreen)
                {
                    // ウィンドウのクラス名を取得
                    StringBuilder className = new StringBuilder(256);
                    GetClassName(hWnd, className, className.Capacity);

                    // ウィンドウのタイトルを取得
                    StringBuilder windowTitle = new StringBuilder(256);
                    GetWindowText(hWnd, windowTitle, windowTitle.Capacity);

                    // ウィンドウクラス名やタイトルがデスクトップに関連するものでないか,なにもフォーカスされていないかを確認
                    bool isDesktop = className.ToString() == "Progman" || windowTitle.ToString() == "Program Manager";
                    bool isNullFocus = string.IsNullOrEmpty(className.ToString()) || string.IsNullOrEmpty(windowTitle.ToString());
                    if (isDesktop || isNullFocus)
                    {
                        Program.logger.Log("デスクトップがフォーカスされている||なにもフォーカスしていない = falseを返しました");
                        return false;
                    }

                    Program.logger.Log("ウィンドウは全画面です true");
                    return true;
                }
                else
                {
                    Program.logger.Log("ウィンドウは全画面ではないです false");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}