using Config.Net;
using System.Net;

namespace ProductFactory.LivyApi.Test
{
   class TestSettings : SettingsContainer
   {
      public readonly Option<NetworkCredential> SparkCluster;

      public readonly Option<string> SparkJobFile;

      public readonly Option<string> SparkJobClassName;

      protected override void OnConfigure(IConfigConfiguration configuration)
      {
         configuration.UseIniFile("c:\\tmp\\LivyApi.ini");
         configuration.UseEnvironmentVariables();
      }
   }
}
