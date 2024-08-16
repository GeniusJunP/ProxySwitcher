using System;
using System.IO;
using System.Windows.Forms;


namespace ProxySwitcher
{
    public class Logger : IDisposable
    {
        private readonly StreamWriter writer;
        private readonly object lockObj = new object();

        public Logger()
        {

            string logFilePath = Path.Combine(Program.settings.LogFilePath, "ProxySwitcherLog.txt");

            if (!File.Exists(logFilePath))
            {
                try
                {
                    using (File.Create(logFilePath)) { }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowError($"ログファイルの作成に失敗しました。指定したディレクトリが有効か、権限があるか確認してください。\nログファイルの保存先は自動的にドキュメントに変更されます。このメッセージボックスを閉じた後、再起動してください。\n詳細:{ex.Message}", "エラー");
                    Program.settings.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    Program.settings.Save(); //LogFilePathの変更を元に戻す
                    Application.Exit();
                }
            }

            try
            {
                writer = new StreamWriter(logFilePath, true);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"ログに失敗しました。指定したディレクトリが有効か、権限があるか確認してください。\nログファイルの保存先は自動的にドキュメントに変更されます。このメッセージボックスを閉じた後、再起動してください。\n詳細:{ex.Message}", "エラー");
                Program.settings.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Program.settings.Save(); //LogFilePathの変更を元に戻す
                Application.Exit();
            }
        }
        public virtual void Log(string message)
        {
            if (!Program.settings.EnableLogging)
            {
                return;
            }

            lock (lockObj)
            {
                try
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowError($"ログに失敗しました。指定したディレクトリが有効か、権限があるか確認してください。\nログファイルの保存先は自動的にドキュメントに変更されます。このメッセージボックスを閉じた後、再起動してください。\n詳細:{ex.Message}", "エラー");
                    Program.settings.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    Program.settings.Save(); //LogFilePathの変更を元に戻す
                    Application.Exit();
                }
            }
        }
        public class NullLogger : Logger
        {
            public override void Log(string message)
            {
                // 何もしない
            }
        }

        public void Dispose()
        {
            writer?.Dispose();
        }
    }
}
