namespace ProxySwitcher
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Save = new System.Windows.Forms.Button();
            this.DescriptionOfSSID = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.DescriptionOfProxyServer = new System.Windows.Forms.Label();
            this.DescriptionOfPort = new System.Windows.Forms.Label();
            this.EnableLogging = new System.Windows.Forms.CheckBox();
            this.EnableConfirming = new System.Windows.Forms.CheckBox();
            this.ShowNotification = new System.Windows.Forms.CheckBox();
            this.CurrentSSIDShow = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SelectedFilePath = new System.Windows.Forms.TextBox();
            this.FileBrowse = new System.Windows.Forms.Button();
            this.SSIDs = new System.Windows.Forms.TextBox();
            this.ProxyAddress = new System.Windows.Forms.TextBox();
            this.PortNumber = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.ToolTip(this.components);
            this.FilePathDescription = new System.Windows.Forms.Label();
            this.SSIDCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(301, 222);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(127, 34);
            this.Save.TabIndex = 2;
            this.Save.Text = "保存";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // DescriptionOfSSID
            // 
            this.DescriptionOfSSID.AutoSize = true;
            this.DescriptionOfSSID.CausesValidation = false;
            this.DescriptionOfSSID.Location = new System.Drawing.Point(12, 20);
            this.DescriptionOfSSID.Name = "DescriptionOfSSID";
            this.DescriptionOfSSID.Size = new System.Drawing.Size(196, 18);
            this.DescriptionOfSSID.TabIndex = 3;
            this.DescriptionOfSSID.Text = "プロキシを有効にするSSID";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(434, 222);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(127, 34);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "キャンセル";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // DescriptionOfProxyServer
            // 
            this.DescriptionOfProxyServer.AutoSize = true;
            this.DescriptionOfProxyServer.Location = new System.Drawing.Point(12, 48);
            this.DescriptionOfProxyServer.Name = "DescriptionOfProxyServer";
            this.DescriptionOfProxyServer.Size = new System.Drawing.Size(180, 18);
            this.DescriptionOfProxyServer.TabIndex = 6;
            this.DescriptionOfProxyServer.Text = "プロキシサーバのアドレス";
            // 
            // DescriptionOfPort
            // 
            this.DescriptionOfPort.AutoSize = true;
            this.DescriptionOfPort.Location = new System.Drawing.Point(12, 77);
            this.DescriptionOfPort.Name = "DescriptionOfPort";
            this.DescriptionOfPort.Size = new System.Drawing.Size(147, 18);
            this.DescriptionOfPort.TabIndex = 7;
            this.DescriptionOfPort.Text = "サーバのポート番号";
            // 
            // EnableLogging
            // 
            this.EnableLogging.AutoSize = true;
            this.EnableLogging.Location = new System.Drawing.Point(18, 123);
            this.EnableLogging.Name = "EnableLogging";
            this.EnableLogging.Size = new System.Drawing.Size(130, 22);
            this.EnableLogging.TabIndex = 8;
            this.EnableLogging.Text = "ログの有効化";
            this.EnableLogging.UseVisualStyleBackColor = true;
            // 
            // EnableConfirming
            // 
            this.EnableConfirming.AutoSize = true;
            this.EnableConfirming.Location = new System.Drawing.Point(18, 152);
            this.EnableConfirming.Name = "EnableConfirming";
            this.EnableConfirming.Size = new System.Drawing.Size(106, 22);
            this.EnableConfirming.TabIndex = 9;
            this.EnableConfirming.Text = "確認通知";
            this.EnableConfirming.UseVisualStyleBackColor = true;
            // 
            // ShowNotification
            // 
            this.ShowNotification.AutoSize = true;
            this.ShowNotification.Location = new System.Drawing.Point(18, 178);
            this.ShowNotification.Name = "ShowNotification";
            this.ShowNotification.Size = new System.Drawing.Size(164, 22);
            this.ShowNotification.TabIndex = 10;
            this.ShowNotification.Text = "切り替え時の通知";
            this.ShowNotification.UseVisualStyleBackColor = true;
            // 
            // CurrentSSIDShow
            // 
            this.CurrentSSIDShow.AutoSize = true;
            this.CurrentSSIDShow.Location = new System.Drawing.Point(230, 153);
            this.CurrentSSIDShow.Name = "CurrentSSIDShow";
            this.CurrentSSIDShow.Size = new System.Drawing.Size(283, 54);
            this.CurrentSSIDShow.TabIndex = 11;
            this.CurrentSSIDShow.Text = "現在接続しているネットワークのSSIDは\r\nくぁｗせｄｒｆｔｇｙふじこｌｐ；＠\r\nです";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // SelectedFilePath
            // 
            this.SelectedFilePath.Location = new System.Drawing.Point(233, 121);
            this.SelectedFilePath.Name = "SelectedFilePath";
            this.SelectedFilePath.Size = new System.Drawing.Size(267, 25);
            this.SelectedFilePath.TabIndex = 12;
            // 
            // FileBrowse
            // 
            this.FileBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FileBrowse.Font = new System.Drawing.Font("MS UI Gothic", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FileBrowse.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.FileBrowse.Location = new System.Drawing.Point(506, 121);
            this.FileBrowse.Name = "FileBrowse";
            this.FileBrowse.Size = new System.Drawing.Size(55, 27);
            this.FileBrowse.TabIndex = 13;
            this.FileBrowse.Text = "参照";
            this.FileBrowse.UseVisualStyleBackColor = true;
            this.FileBrowse.Click += new System.EventHandler(this.FileBrowse_Click);
            // 
            // SSIDs
            // 
            this.SSIDs.Location = new System.Drawing.Point(233, 14);
            this.SSIDs.Name = "SSIDs";
            this.SSIDs.Size = new System.Drawing.Size(328, 25);
            this.SSIDs.TabIndex = 14;
            this.Description.SetToolTip(this.SSIDs, "カンマ , を使用して複数のSSIDを指定することができます。（例:SSID1,SSID2）\r\nまた、空白にすると切り替えが無効になります。");
            // 
            // ProxyAddress
            // 
            this.ProxyAddress.Location = new System.Drawing.Point(233, 45);
            this.ProxyAddress.Name = "ProxyAddress";
            this.ProxyAddress.Size = new System.Drawing.Size(189, 25);
            this.ProxyAddress.TabIndex = 15;
            this.Description.SetToolTip(this.ProxyAddress, "プロキシサーバのアドレスを入力します。（例:192.168.0.1）");
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(233, 77);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(80, 25);
            this.PortNumber.TabIndex = 16;
            this.Description.SetToolTip(this.PortNumber, "ポート番号を指定します。（例:8080）");
            // 
            // Description
            // 
            this.Description.StripAmpersands = true;
            // 
            // FilePathDescription
            // 
            this.FilePathDescription.AutoSize = true;
            this.FilePathDescription.Location = new System.Drawing.Point(161, 124);
            this.FilePathDescription.Name = "FilePathDescription";
            this.FilePathDescription.Size = new System.Drawing.Size(66, 18);
            this.FilePathDescription.TabIndex = 17;
            this.FilePathDescription.Text = "保存先:";
            this.FilePathDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SSIDCopy
            // 
            this.SSIDCopy.Location = new System.Drawing.Point(434, 173);
            this.SSIDCopy.Name = "SSIDCopy";
            this.SSIDCopy.Size = new System.Drawing.Size(127, 27);
            this.SSIDCopy.TabIndex = 18;
            this.SSIDCopy.Text = "SSIDをコピー";
            this.Description.SetToolTip(this.SSIDCopy, "現在のSSIDをクリップボードにコピーすることができます。");
            this.SSIDCopy.UseVisualStyleBackColor = true;
            this.SSIDCopy.Click += new System.EventHandler(this.SSIDCopy_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 269);
            this.Controls.Add(this.SSIDCopy);
            this.Controls.Add(this.FilePathDescription);
            this.Controls.Add(this.PortNumber);
            this.Controls.Add(this.ProxyAddress);
            this.Controls.Add(this.SSIDs);
            this.Controls.Add(this.FileBrowse);
            this.Controls.Add(this.SelectedFilePath);
            this.Controls.Add(this.CurrentSSIDShow);
            this.Controls.Add(this.ShowNotification);
            this.Controls.Add(this.EnableConfirming);
            this.Controls.Add(this.EnableLogging);
            this.Controls.Add(this.DescriptionOfPort);
            this.Controls.Add(this.DescriptionOfProxyServer);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.DescriptionOfSSID);
            this.Controls.Add(this.Save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "ProxySwitcherSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label DescriptionOfSSID;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label DescriptionOfProxyServer;
        private System.Windows.Forms.Label DescriptionOfPort;
        private System.Windows.Forms.CheckBox EnableLogging;
        private System.Windows.Forms.CheckBox EnableConfirming;
        private System.Windows.Forms.CheckBox ShowNotification;
        private System.Windows.Forms.Label CurrentSSIDShow;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox SelectedFilePath;
        private System.Windows.Forms.Button FileBrowse;
        private System.Windows.Forms.TextBox SSIDs;
        private System.Windows.Forms.TextBox ProxyAddress;
        private System.Windows.Forms.TextBox PortNumber;
        private System.Windows.Forms.ToolTip Description;
        private System.Windows.Forms.Label FilePathDescription;
        private System.Windows.Forms.Button SSIDCopy;
    }
}