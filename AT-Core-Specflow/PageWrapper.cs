using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AT_Core_Specflow
{
    public class PageWrapper
    {
        private Dictionary<object, string> _members;

        public Dictionary<object, string> Members => _members ?? (_members = new Dictionary<object, string>());
    }
}
