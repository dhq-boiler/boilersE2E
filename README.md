# boilersE2E

boilersE2E は Windows Desktop アプリ向けの E2E テストのためのユーティリティーライブラリです。

## How to use / 使用方法

1. WinAppDriver をインストールします。

[https://github.com/microsoft/WinAppDriver/releases](https://github.com/microsoft/WinAppDriver/releases)

2. E2E テストプロジェクトを作成し、Nugetで boilersE2E を追加します。

3. boilersE2E.E2ETestFixture クラスを継承した任意のクラスを作成します。

4. AppPath、WindowSize をオーバーライドして指定します。

5. スタティックコンストラクタで boilersE2ETestEnvironmentVariableName を指定します。

6. E2Eテストを実行するシステムで、boilersE2ETestEnvironmentVariableName に指定した名前の環境変数を作成し、値を true にします。
   Azure DevOps pipeline でE2Eテストを実行する場合は、 Windows Application Driver タスクを実行するので false を指定してください。

7. お好きなようにE2Eテストメソッドを実装します。

8. E2Eテストを実行します。

## LICENSE / ライセンス

MIT License