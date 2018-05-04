namespace AT_Core_Specflow.Extensions.WaitExtensions.Interfaces
{
    public interface IPageWaitConditions
    {
        void TitleEqual(string title);
        void UrlEqual(string url);
        bool UrlContain(string url);
        void UrlMatches(string regex);
        bool ReadyStateComplete();
        void TitleContain(string title, bool ignoreCase = false);
        void LoaderDissapear();
    }
}