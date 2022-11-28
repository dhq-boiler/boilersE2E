# boilersE2E

boilersE2E は Windows Desktop アプリ向けの E2E テストのためのユーティリティーライブラリです。

boilersE2E は以下のライブラリから構成されます。

* boilersE2E.Core [![boilersE2E.Core](https://img.shields.io/nuget/v/boilersE2E.Core)](https://www.nuget.org/packages/boilersE2E.Core/) ・・・boilersE2Eのコア機能を含むライブラリ。必須。
* boilersE2E.xUnit [![boilersE2E.xUnit](https://img.shields.io/nuget/v/boilersE2E.xUnit)](https://www.nuget.org/packages/boilersE2E.xUnit/) ・・・xUnit向けライブラリ。
* boilersE2E.NUnit [![boilersE2E.NUnit](https://img.shields.io/nuget/v/boilersE2E.NUnit)](https://www.nuget.org/packages/boilersE2E.NUnit/) ・・・NUnit向けライブラリ。
* boilersE2E.MsTest [![boilersE2E.MsTest](https://img.shields.io/nuget/v/boilersE2E.MsTest)](https://www.nuget.org/packages/boilersE2E.MsTest/) ・・・MsTest向けライブラリ。

あなたが使用しているテストフレームワークに合わせて、NuGetからインストールしてください。

以下では、NUnitの使用方法の例を記載しています。

その他のテストフレームワークでの使用方法が知りたい方は、以下を参照ください。

* [boilersE2E.xUnit.Test](https://github.com/dhq-boiler/boilersE2E/tree/develop/boilersE2E.xUnit.Test)
* [boilersE2E.NUnit.Test](https://github.com/dhq-boiler/boilersE2E/tree/develop/boilersE2E.NUnit.Test)
* [boilersE2E.MsTest.Test](https://github.com/dhq-boiler/boilersE2E/tree/develop/boilersE2E.MsTest.Test)

## Requirements / 要件

* Windows 10.0.22000.0 以降のWindows環境
* .NET 6.0 or 7.0

## How to use for NUnit / NUnitでの使用方法

1. WinAppDriver をインストールします。

   [https://github.com/microsoft/WinAppDriver/releases](https://github.com/microsoft/WinAppDriver/releases)

2. E2E テストプロジェクトを作成し、Nugetで boilersE2E.Core と boilersE2E.NUnit を追加します。

3. boilersE2E.E2ETestFixture クラスを継承した任意のクラスを作成します。

4. AppPath、WindowSize をオーバーライドして指定します。

   https://github.com/dhq-boiler/boilersE2E/blob/d19b900ab1daa26f3803b56bf7a480a50d824bfc/boilersE2E.NUnit.Test/E2ETestForWPF.cs#L8-L10

5. スタティックコンストラクタで boilersE2ETestEnvironmentVariableName を指定します。

   https://github.com/dhq-boiler/boilersE2E/blob/d19b900ab1daa26f3803b56bf7a480a50d824bfc/boilersE2E.NUnit.Test/E2ETestForWPF.cs#L11-L14

6. E2Eテストを実行するシステムで、boilersE2ETestEnvironmentVariableName に指定した名前の環境変数を作成し、値を true にします。
   Azure DevOps pipeline でE2Eテストを実行する場合は、 Windows Application Driver タスクを実行するので false を指定してください。

7. お好きなようにE2Eテストメソッドを実装します。
   
   * WPF向けサンプルコード

     https://github.com/dhq-boiler/boilersE2E/blob/d19b900ab1daa26f3803b56bf7a480a50d824bfc/boilersE2E.NUnit.Test/E2ETestForWPF.cs
   
   * WinForms向けサンプルコード
   
     https://github.com/dhq-boiler/boilersE2E/blob/d19b900ab1daa26f3803b56bf7a480a50d824bfc/boilersE2E.NUnit.Test/E2ETestForWinForms.cs

8. E2Eテストを実行します。

## LICENSE / ライセンス

MIT License
