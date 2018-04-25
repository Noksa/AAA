using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace IML_AT_Core.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureStepAttribute : Attribute
    {
        public string StepDescription { get; set; }
    }
}
