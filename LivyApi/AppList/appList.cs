using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastacloud.LivyApi.AppList
{
    public class appList
    {
        public int from { get; set; }
        public int total { get; set; }
        public session[] sessions { get; set; }
    }
}
