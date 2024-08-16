校内Wi-Fiに接続する際、プロキシサーバに接続しないとWANに繋がらない（多分ファイアウォールでプロキシサーバ以外からのトラフィックを止めてる？）せいで毎回プロキシ設定するのめんどいから作った

--後から書く--

環境変数とレジストリ両方変えてるのでほとんどのソフトは再起動すれば反映されるはずですが、
手元で確認した限りだと~~あのいまいましい~~LINEだけは環境変数読み取ってないらしくソフト内の設定をいじらないとダメでした（自動変更に非対応）

3.	プロキシ変更機能

プロキシを変更するロジックとして、
	レジストリ値の変更（HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings内のProxyEnable, ProxyServer ,ProxyOverrideキーの値の設定）
	環境変数の変更（HTTP_PROXY, HTTPS_PROXYそれぞれのユーザ・プロセス変数の変更）
を使用している。

二種類の使い方がある。
	自動変更
	手動変更
それぞれの使用手順は以下の通りである。
	自動変更
設定画面で指定したSSIDの時にプロキシ設定を適用させる。指定以外の場合（有線接続時・未接続時も含む）はプロキシ設定が解除される。

	手動変更
1.	タスクバーのProxySwitcherアイコンを右クリック
2.	メニュー内の“手動適用”、“手動解除”を選択することで利用できる。

4.	設定内で設定できる機能
 

	プロキシ設定機能
設定画面から、
	プロキシ設定を適用するSSIDの指定（カンマで区切ることで複数指定が可能、配列で保存）
	プロキシサーバのアドレス
	ポート番号
を指定できる。
	現在のSSIDの表示
設定を簡単にする。
	SSIDをコピー
ボタンを押すと、現在のSSIDをクリップボードにコピーすることで設定を簡単にする。
	ログの有効化
動作ログを指定したディレクトリに保存できる。
	確認通知
SSIDなどの状態が変更された場合にプロキシ設定を変更するか尋ねるメッセージボックスが表示される。
	また、ゲーム中など、他プロセスが全画面表示の場合に、
ウィンドウフォーカスを移動させてユーザー体験を損なわないように、
	全画面表示が解除される
	別プロセスの非全画面表示ウィンドウにフォーカスされる
まではポップアップの出現を待機させる機能を導入している。

	切り替え時の通知
プロキシ設定が手動・自動で変更された場合に通知が表示される。（Microsoft.Toolkit.Uwp.Notificationsを使用した通知のため他アプリと同様にWindowsの標準通知として表示することができる。）
	保存機能
“%userprofile%\AppData\Roaming\ProxySwitcher\Settings.xml”内に設定を保存できるため、アプリケーションが終了しても設定が保持される。
	キャンセル機能
設定の保存をキャンセルする。

5.	その他の機能
	多重起動防止（プログラム・設定ウィンドウ）
プログラムが多重起動して例外が発生するのを防ぐためミューテックスを使用してすでに起動しているかを判別します。
すでに起動している場合、すでに起動している旨のメッセージボックスを表示する。
