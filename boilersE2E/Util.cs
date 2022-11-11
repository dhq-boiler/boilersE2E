namespace boilersE2E
{
    internal static class Util
    {
        internal static void SetTextToClipboard(string text)
        {
            if (AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == "PresentationFramework") is not null
                && System.Windows.Application.Current is not null) //WPF 実行時
            {
                System.Windows.Clipboard.SetText(text);
            }
            else //Windows Forms 実行時
            {
                System.Windows.Forms.Clipboard.SetText(text);
            }
        }
    }
}
