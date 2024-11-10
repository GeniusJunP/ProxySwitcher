using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ProxySwitcher
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            Program.isSettingsFormOpen = true;
            Program.logger.Log("SettingsForm initialized.");
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Program.logger.Log("SettingsForm_Load started.");
            string commaSeparatedSSIDs = string.Join(",", Program.settings.SSIDs); //配列をカンマ区切りの文字列に変換
            SSIDs.Text = commaSeparatedSSIDs; //SSIDの表示
            ProxyAddress.Text = Program.settings.ProxyAddress;//プロキシアドレスの表示
            PortNumber.Text = Program.settings.ProxyPort.ToString();
            SelectedFilePath.Text = Program.settings.LogFilePath;
            EnableLogging.Checked = Program.settings.EnableLogging;
            EnableConfirming.Checked = Program.settings.EnableConfirming;
            ShowNotification.Checked = Program.settings.ShowNotification;

            if (Program.networkMonitor.GetCurrentSSID() != "")
            {
                CurrentSSIDShow.Text = "現在接続しているネットワークのSSIDは\n\"" + Program.networkMonitor.GetCurrentSSID() + "\"です";
            }
            else
            {
                CurrentSSIDShow.Text = "SSIDを取得できなかった、または有線接続です";
            }
            Program.logger.Log("SettingsForm_Load completed.");
        }

        private void FileBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // 選択されたフォルダのパスを取得
                    string BrowseFilePath = folderBrowserDialog.SelectedPath;
                    // テキストボックスに表示
                    SelectedFilePath.Text = BrowseFilePath;
                }
            }
            Program.logger.Log("FileBrowse_Click completed.");
        }

        private void SSIDCopy_Click(object sender, EventArgs e)
        {
            Program.logger.Log("SSIDCopy_Click started.");
            if (Program.networkMonitor.GetCurrentSSID() != "")
            {
                Clipboard.SetText(Program.networkMonitor.GetCurrentSSID());
            }
            Program.logger.Log("SSIDCopy_Click completed.");
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            Program.logger.Log("Save_Click started.");
            int proxyPortResult;

            if (ProxyAddress.Text == "")
            {
                MessageBoxHelper.ShowWarning("プロキシサーバのアドレスを入力してください");
                return;
            }

            if (PortNumber.Text == "")
            {
                MessageBoxHelper.ShowWarning("ポート番号を入力してください");
                return;
            }
            else
            {
                if (!int.TryParse(PortNumber.Text, out proxyPortResult))
                {
                    MessageBoxHelper.ShowWarning("ポート番号は半角数字で入力してください");
                    return;
                }
            }

            if (!IsDirectoryValid(SelectedFilePath.Text))
            {
                MessageBoxHelper.ShowWarning("ログファイルの保存先が存在しません");
                return;
            }

            Program.settings.SSIDs = SSIDs.Text.Split(',');
            Program.settings.ProxyAddress = ProxyAddress.Text;
            Program.settings.ProxyPort = proxyPortResult;
            Program.settings.EnableLogging = EnableLogging.Checked;
            Program.settings.LogFilePath = SelectedFilePath.Text;
            Program.settings.EnableConfirming = EnableConfirming.Checked;
            Program.settings.ShowNotification = ShowNotification.Checked;

            try
            {
                Program.logger.Log("Saving settings...");
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                await Task.Run(() => Program.settings.Save());
                stopwatch.Stop();
                Program.logger.Log($"Settings saved in {stopwatch.ElapsedMilliseconds} ms");

                stopwatch.Restart();
                Program.logger.Log("Applying proxy settings...");
                await Task.Run(() => Program.networkMonitor.CallProxyAutoApply());
                stopwatch.Stop();
                Program.logger.Log($"Proxy settings applied in {stopwatch.ElapsedMilliseconds} ms");

                this.Close();
                Program.logger.Log("Settings form closed.");
            }
            catch (Exception ex)
            {
                Program.logger.Log($"Error occurred while saving settings: {ex.Message}");
                MessageBoxHelper.ShowError("設定の保存中にエラーが発生しました: " + ex.Message);
            }
            Program.logger.Log("Save_Click completed.");
        }

        private bool IsDirectoryValid(string path)
        {
            Program.logger.Log($"IsDirectoryValid started for path: {path}");
            if (string.IsNullOrEmpty(path))
            {
                Program.logger.Log("Path is null or empty.");
                return false;
            }

            try
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
                bool exists = directoryInfo.Exists;
                Program.logger.Log($"Directory exists: {exists}");
                return exists;
            }
            catch (Exception ex)
            {
                Program.logger.Log($"Exception in IsDirectoryValid: {ex.Message}");
                return false;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Program.logger.Log("Cancel_Click started.");
            this.Close();
            Program.logger.Log("Settings form closed.");
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.logger.Log("SettingsForm_FormClosing started.");
            Program.isSettingsFormOpen = false;
            Program.logger.Log("SettingsForm_FormClosing completed.");
        }
    }
}
