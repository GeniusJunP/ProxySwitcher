using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static ProxySwitcher.Logger;

namespace ProxySwitcher
{
    static class Program
    {
        public readonly static Logger logger;
        public static NetworkMonitor networkMonitor;
        public static Settings settings;
        public static bool isSettingsFormOpen = false; // SettingsForm の状態を管理する変数

        static Program()
        {
            // 設定のロード
            settings = Settings.Load();

            // ログファイルを作成（有効な場合）
            if (settings != null && settings.EnableLogging == true)
            {
                logger = new Logger();
                Program.logger.Log("Logging started");
            }
            else
            {
                logger = new NullLogger(); //なんもしないLogger
            }

            Program.logger.Log("Settings loaded");
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ミューテックスを作成
            Mutex mutex = new Mutex(true, "ProxySwitcherMutex", out bool createdNew);
            if (!createdNew)
            {
                // 既にアプリケーションが起動している場合
                MessageBoxHelper.ShowInfo("既に起動しています");
                Program.logger.Log("Application is already running");
                return;
            }

            Program.logger.Log("Application started");

            // ネットワーク監視の開始 ここからNetworkAddressChangedEventHandlerに追加される
            networkMonitor = new NetworkMonitor();
            Program.logger.Log("NetworkMonitor instance created");

            NotifyIcon trayIcon = new NotifyIcon()
            {
                Icon = new Icon("icon.ico"), // アイコンファイルを指定
                Visible = true
            };
            Program.logger.Log("Tray icon created");

            ContextMenuStrip contextMenu = new ContextMenuStrip(); //右クリックメニュー
            contextMenu.Items.Add("設定", null, (sender, e) =>
            {
                if (isSettingsFormOpen == true)
                {
                    // 既にFormが起動している場合
                    MessageBoxHelper.ShowInfo("既に設定が開いています");
                    Program.logger.Log("Settings form is already open");
                    return;
                }
                else
                {
                    ShowSettings();
                }
            });
            Program.logger.Log("Settings menu item added");

            //プロキシを変更するボタンも追加
            contextMenu.Items.Add("手動適用", null, (sender, e) =>
            {
                networkMonitor.ProxyApply();
                Program.logger.Log("Manual proxy apply executed");
                if (Program.settings.ShowNotification == true) // 通知を表示する設定が有効な場合
                {
                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("手動でプロキシ設定が適用されました")
                        .Show();
                    Program.logger.Log("Notification shown for manual proxy apply");
                }
            });

            contextMenu.Items.Add("手動解除", null, (sender, e) =>
            {
                networkMonitor.ProxyRemove();
                Program.logger.Log("Manual proxy remove executed");
                if (Program.settings.ShowNotification == true) // 通知を表示する設定が有効な場合
                {
                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("手動でプロキシ設定が解除されました")
                        .Show();
                    Program.logger.Log("Notification shown for manual proxy remove");
                }
            });

            contextMenu.Items.Add("終了", null, (sender, e) =>
            {
                Application.Exit();
                trayIcon.Visible = false;
                Program.logger.Log("Application exited");
            });
            Program.logger.Log("Exit menu item added");

            trayIcon.ContextMenuStrip = contextMenu;

            trayIcon.DoubleClick += (sender, e) => //ダブルクリック時にもSettings表示
            {
                if (isSettingsFormOpen == true)
                {
                    // 既にFormが起動している場合
                    MessageBoxHelper.ShowInfo("既に設定が開いています");
                    Program.logger.Log("Settings form is already open (double-click)");
                    return;
                }
                else
                {
                    ShowSettings();
                }
            };
            Program.logger.Log("Tray icon double-click event added");

            // 初回実行を非同期に変更
            networkMonitor.CallProxyAutoApply();
            Program.logger.Log("Initial proxy auto-apply called");

            // メッセージループを開始
            Application.Run();
        }

        static void ShowSettings()
        {
            // 設定フォームを表示
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            Program.logger.Log("Settings form shown");
        }
    }

    public class Settings
    {
        public string[] SSIDs { get; set; } = new string[] { "example" };
        public string ProxyAddress { get; set; } = "1.1.1.1";
        public int ProxyPort { get; set; } = 8080;
        public bool EnableLogging { get; set; } = false;
        public string LogFilePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public bool EnableConfirming { get; set; } = false;
        public bool ShowNotification { get; set; } = true;

        public static readonly string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ProxySwitcher", "Settings.xml");

        public static Settings Load()
        {
            try
            {
                if (System.IO.File.Exists(settingsFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    using (StreamReader reader = new StreamReader(settingsFilePath))
                    {
                        return (Settings)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    Settings defaultSettings = new Settings();
                    defaultSettings.Save(); // 新しい設定ファイルを作成
                    return defaultSettings;
                }
            }
            catch (Exception ex)
            {
                Program.logger.Log($"An error occurred while loading the settings: {ex.Message}");
                throw;
            }
        }

        public void Save()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(settingsFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(settingsFilePath));
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(settingsFilePath))
                {
                    serializer.Serialize(writer, this);
                }
                Program.logger.Log("Settings saved");
            }
            catch (Exception ex)
            {
                Program.logger.Log($"An error occurred while saving the settings: {ex.Message}");
                throw;
            }
        }
    }
}
