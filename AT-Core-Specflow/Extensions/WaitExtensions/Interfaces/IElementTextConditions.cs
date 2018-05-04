using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IML_AT_Core.Extensions.WaitExtensions.Interfaces
{
    public interface IElementTextConditions
    {
        void Contains(string text, bool ignoreCase = false);
        void Equals(string text);
    }
}
