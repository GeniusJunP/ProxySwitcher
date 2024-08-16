using System;
using System.Windows.Forms;

namespace ProxySwitcher
{
    public partial class SettingsForm : Form

    {
        private readonly NetworkMonitor networkMonitor = new NetworkMonitor();
        public SettingsForm()
        {
            InitializeComponent();
            Program.isSettingsFormOpen = true;
        }


        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string commaSeparatedSSIDs = string.Join(",", Program.settings.SSIDs); //配列をカンマ区切りの文字列に変換
            SSIDs.Text = commaSeparatedSSIDs; //SSIDの表示
            ProxyAddress.Text = Program.settings.ProxyAddress;//プロキシアドレスの表示
            PortNumber.Text = Program.settings.ProxyPort.ToString();
            EnableLogging.Checked = Program.settings.EnableLogging;
            SelectedFilePath.Text = Program.settings.LogFilePath;
            EnableLogging.Checked = Program.settings.EnableLogging;
            EnableConfirming.Checked = Program.settings.EnableConfirming;
            ShowNotification.Checked = Program.settings.ShowNotification;

            if (networkMonitor.GetCurrentSSID() != "")
            {
                CurrentSSIDShow.Text = "現在接続しているネットワークのSSIDは\n\"" + networkMonitor.GetCurrentSSID() + "\"\nです";
            }
            else
            {
                CurrentSSIDShow.Text = "SSIDを取得できなかった、または有線接続です";
            }


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

        }

        private void SSIDCopy_Click(object sender, EventArgs e)
        {
            if(networkMonitor.GetCurrentSSID() != "")
            {
                Clipboard.SetText(networkMonitor.GetCurrentSSID());
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
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
            Program.settings.Save();
            Program.networkMonitor.CallProxyAutoApply();
            this.Close();
        }
        private bool IsDirectoryValid(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
                return directoryInfo.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.isSettingsFormOpen=false;
        }
    }
}
