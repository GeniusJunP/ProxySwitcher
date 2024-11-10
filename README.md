# ProxySwitcher

Windows環境でWi-Fiのネットワーク切り替え時に自動的にプロキシ設定を変更するツールです。

## 主な機能

### プロキシ設定の自動切り替え
- 指定したSSIDに接続時、自動的にプロキシ設定を適用
- 指定外のSSID（有線接続・未接続を含む）では自動的にプロキシ設定を解除
- プロキシ設定の変更時に通知を表示（Windows標準の通知システムを使用）

### プロキシ設定の手動切り替え
- タスクバーアイコンから右クリックで操作可能
- 「手動適用」「手動解除」オプションを提供

### 設定機能
#### プロキシ設定
- 複数SSIDの登録（カンマ区切りで指定可能）
- プロキシサーバーアドレスの設定
- ポート番号の設定

#### ユーザー補助機能
- 現在のSSIDの表示
- ワンクリックでのSSIDコピー機能
- 設定変更の確認ポップアップ機能
    - 上記確認機能において、**ゲームなど全画面アプリケーション実行時の通知制御**が実装されています。
      具体的には、現在アクティブなウィンドウが全画面表示のとき、全画面表示が解除される・または別のウィンドウにフォーカスされるまで、ポップアップを待機するため、**ユーザー体験を妨げない通知タイミング**を実現することができます。

#### その他の設定
- 動作ログの保存機能（指定ディレクトリに保存）
- 設定のXMLファイルへの保存
    - 保存先: `%userprofile%\AppData\Roaming\ProxySwitcher\Settings.xml`

### システム機能
- プログラムの多重起動防止（ミューテックスを使用）
- プロキシ設定の適用方式
    - レジストリ値の変更
        - `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings`
        - `ProxyEnable`, `ProxyServer`, `ProxyOverride`キーを設定
    - 環境変数の設定
        - `HTTP_PROXY`、`HTTPS_PROXY`
        - ユーザー変数とプロセス変数の両方を設定

## 注意事項
- プロキシ設定の変更を反映するには、ネットワークを使用するアプリケーションの再起動が必要な場合があります
- 設定はアプリケーション終了後も保持されます。リセットしたい場合は前述のディレクトリから`Settings.xml`を削除してください。
