using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IML_AT_Core.Extensions.WaitExtensions.Interfaces
{
    public interface IPageWaitConditions
    {
        void TitleEqual(string title);
        void UrlEqual(string url);
        void UrlContain(string url);
        void UrlMatches(string regex);
        void ReadyStateComplete();
        void TitleContain(string title, bool ignoreCase = false);
        void LoaderDissapear();
    }
}
