using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace SimpleRestAPI.Shared.Shared.Utils
{
    public static class AppSettings
    {
        private const string vDefaultFileName = "appsettings.json";
        private const string vDefaultJsonValue = "{}";
        private static dynamic? vConfiguration;
        
        public static dynamic Config => vConfiguration ?? GetConfig();

        public static string ConnectionString => Config.ConnectionStrings.DefaultConnection.ToString();
        public static dynamic TokenKey => Config.TokenKey;
        public static string PassowrdSalt => Config.PasswordSalt;
        private static dynamic GetConfig(string pFileName = vDefaultFileName)
        {
            string _appSettings = vDefaultJsonValue;

            if (File.Exists(pFileName))
            {
                _appSettings = File.ReadAllText(pFileName);
            }

            // Set Configuration value from appsettings.json - Thats is not a best pratice.... Need to be changed to environment variables. 
            vConfiguration = JsonConvert.DeserializeObject<ExpandoObject>(_appSettings, new ExpandoObjectConverter());

            return vConfiguration;
        }
    }

}
