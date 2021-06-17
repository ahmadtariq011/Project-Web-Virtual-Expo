using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualExpo.Model
{
    public static class AppConfiguration
    {
        private static readonly string connectionString;
        
        //private static readonly string payPalMode;
        //private static readonly string smtpHost;
        //private static readonly int smtpPort;
        //private static readonly string smtpUser;
        //private static readonly string smtpPassword;

        static AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            connectionString = root.GetConnectionString("VirtualExpoDB");

            var appSetting = root.GetSection("ApplicationSettings");
            //payPalMode = appSetting["PayPalMode"];
            //smtpHost = appSetting["SmtpHost"]; 
            //smtpPort = Convert.ToInt32(appSetting["SmtpPort"]); 
            //smtpUser = appSetting["SmtpUser"];
            //smtpPassword = appSetting["SmtpPass"];
        }

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        //public static string SmtpHost
        //{
        //    get
        //    {
        //        return smtpHost;
        //    }
        //}

        //public static int SmtpPort
        //{
        //    get
        //    {
        //        return smtpPort;
        //    }
        //}

        //public static string SmtpUser
        //{
        //    get
        //    {
        //        return smtpUser;
        //    }
        //}

        //public static string SmtpPassword
        //{
        //    get
        //    {
        //        return smtpPassword;
        //    }
        //}

        //public static bool IsLivePayPal
        //{
        //    get
        //    {
        //        return payPalMode == "Live" ? true : false;
        //    }
        //}
    }
}
