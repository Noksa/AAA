using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IML_AT_Core.CustomElements.Attributes
{
    public class ElementTitleAttribute : Attribute
    {
        public ElementTitleAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

}
