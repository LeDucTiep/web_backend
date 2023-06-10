using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Attribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class MSRequiredAttribute : System.Attribute
    {
        public int ErrorCode { get; set; }
    }
}
