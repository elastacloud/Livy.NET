using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastacloud.LivyApi.AppList
{
    public class session
    {
        public string appId { get; set; }
        public int id { get; set; }
        public string state { get; set; }
        public string[] log { get; set; }
        public appInfo appInfo { get; set; }
    }
}
