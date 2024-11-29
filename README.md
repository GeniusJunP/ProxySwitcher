# ProxySwitcher

Windows環境でWi-Fiのネットワーク切り替え時に自動的にプロキシ設定を変更するツールです。

>[!TIP]
>学校でC#の課題が出たので、なるべく実用的なものをと思い、当時悩んでいた**学内ネットワーク接続時のプロキシ設定が煩わしい問題**（AndroidとかiOSはアクセスポイントで切り替えしてくれるのにWindowsとかMacは自動切り替えしてくれないのなんでやねん）を解決するためGitHub Copilotと一緒に書きました。

## 主な機能

### プロキシ設定の自動切り替え
- 指定したSSIDに接続時、自動的にプロキシ設定を適用
- 指定外のSSID（有線接続・未接続を含む）では自動的にプロキシ設定を解除

### 通知機能
- プロキシ設定の変更時に通知を表示（Windows標準のUWP通知機能を使用）
- 設定変更の確認ポップアップ機能
    - 上記確認機能において、**ゲームなど全画面アプリケーション実行時の通知制御**が実装されています。

      具体的には、現在アクティブなウィンドウが全画面表示のとき、全画面表示が解除される、または別のウィンドウにフォーカスされるまでポップアップを待機するため、**ユーザー体験を妨げない通知タイミング**を実現することができます。

### プロキシ設定の手動切り替え
- タスクバーアイコンから右クリック→各オプションから操作可能
- 「手動適用」「手動解除」オプションを提供

### 設定機能

![設定画面](https://github.com/GeniusJunP/ProxySwitcher/blob/Assets/SettingsForm.png "設定画面")

- タスクバーアイコンから右クリック→設定から操作可能

#### プロキシ設定
- 複数SSIDの登録（カンマ区切りで指定可能）
- プロキシサーバーアドレスの設定
- ポート番号の設定
- （要望があればプロキシサーバの資格情報の項目も追加します）
> [!NOTE]
> 環境変数とレジストリ両方変えてるのでほとんどのソフトは再起動すれば反映されるはずですが、**手元で確認した限りだと**~~あのいまいましい~~LINEだけは環境変数を読み取ってないらしくソフト内の設定をいじらないとダメでした（自動変更に非対応）

#### 便利機能
- 設定画面での現在のSSIDの表示
- 設定画面でのワンクリックでのSSIDコピー機能
- 前述の通知機能

#### その他の設定
- 動作ログの保存機能（指定ディレクトリに保存）
- 設定をXMLファイルへ保存
    - 保存先: `%userprofile%\AppData\Roaming\ProxySwitcher\Settings.xml`

### その他仕組みなど
- プログラムの多重起動防止（ミューテックスを使用）
- プロキシ設定の適用方式
    - レジストリ値の変更
        - `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings`
        - `ProxyEnable`, `ProxyServer`, `ProxyOverride`キーを設定
    - 環境変数の設定
        - `HTTP_PROXY`、`HTTPS_PROXY`
        - ユーザー変数とプロセス変数の両方を設定

> [!WARNING]
> - プロキシ設定の変更を反映するには、ネットワークを使用するアプリケーションの再起動が必要な場合があります。
> - 設定はアプリケーション終了後も保持されます。リセットしたい場合は再インストールするか、前述のディレクトリから`Settings.xml`を削除してください。
> - 本ソフトウェアの使用により生じたいかなる損害についても、俺は一切の責任を負いません。自己責任でご使用ください。

## ライセンス
本ソフトウェアはWTFPLライセンスの下で公開されています。詳細は[LICENSE](https://github.com/GeniusJunP/ProxySwitcher/blob/main/LICENCE)ファイルをご覧ください。

Copyright (C) 2024 [GeniusJunP]

This work is free. You can redistribute it and/or modify it under the
terms of the Do What The Fuck You Want To Public License, Version 2,
as published by Sam Hocevar. See the [LICENSE](https://github.com/GeniusJunP/ProxySwitcher/blob/main/LICENCE) file for more details.

[![WTFPL](http://www.wtfpl.net/wp-content/uploads/2012/12/wtfpl-badge-4.png)](http://www.wtfpl.net/)
