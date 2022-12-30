using System.Reflection;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("boilerE2E.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("boilerE2E.Core")]
[assembly: AssemblyCopyright("Copyright ©dhq_boiler 2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、このアセンブリ内の型は COM コンポーネントから
// 参照できなくなります。COM からこのアセンブリ内の型にアクセスする必要がある場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("6DDB4783-D41E-4552-A84A-EB115889BE16")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、次を使用してビルド番号とリビジョン番号を既定に設定できます
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(boilersE2E.Core.ThisAssembly.Git.BaseVersion.Major + "." + boilersE2E.Core.ThisAssembly.Git.BaseVersion.Minor + "." + boilersE2E.Core.ThisAssembly.Git.BaseVersion.Patch)]

[assembly: AssemblyFileVersion(boilersE2E.Core.ThisAssembly.Git.SemVer.Major + "." + boilersE2E.Core.ThisAssembly.Git.SemVer.Minor + "." + boilersE2E.Core.ThisAssembly.Git.SemVer.Patch)]

[assembly: AssemblyInformationalVersion(
    "v" +
    boilersE2E.Core.ThisAssembly.Git.SemVer.Major + "." +
    boilersE2E.Core.ThisAssembly.Git.SemVer.Minor + "." +
    boilersE2E.Core.ThisAssembly.Git.SemVer.Patch + "." +
    boilersE2E.Core.ThisAssembly.Git.Commits + "-" +
    boilersE2E.Core.ThisAssembly.Git.Branch + "+" +
    boilersE2E.Core.ThisAssembly.Git.Commit)]