namespace Elastacloud.LivyApi
{
   public class LivySettings
   {
      public LivySettings(string username, string password, string clusterName)
      {
         Username = username;
         Password = password;
         ClusterUri = clusterName;
      }
      public string Username { get; private set; }
      public string Password { get; private set; }
      public string ClusterUri { get; private set; }
      // EMR - http://master-public-dns-name:8998/
      // HDI - https://{0}.azurehdinsight.net/livy/
   }
}
