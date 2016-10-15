using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastacloud.LivyApi
{
    public class LivySettings
    {
        public LivySettings(string username, string password, string clusterName)
        {
            Username = username;
            Password = password;
            ClusterName = clusterName;
        }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ClusterName { get; private set; }
    }
}
