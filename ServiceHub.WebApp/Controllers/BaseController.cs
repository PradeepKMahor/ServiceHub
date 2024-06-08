using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceHub.WebApp.Models;
using System.Globalization;

namespace ServiceHub.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public static string ToTitleCase(string str)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(str);

            // return str?.First().ToString().ToUpper() + str?.Substring(1).ToLower();
        }

        public async Task NotifyAsync(string Title,
            string Message,
            string Provider,
            NotificationType notificationType)
        {
            var msg = new
            {
                message = Message,
                title = Title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = Provider//GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        public void Notify(string Title,
            string Message,
            string Provider,
            NotificationType notificationType)
        {
            var msg = new
            {
                message = Message,
                title = Title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = Provider//GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        //public void Notify(
        //    string Title =null,
        //    string Message = null,
        //    string Provider = null,
        //    NotificationType notificationType =1)
        //{
        //    var msg = new
        //    {
        //        message = Message,
        //        title = Title,
        //        icon = notificationType.ToString(),
        //        type = notificationType.ToString(),
        //        provider = Provider//GetProvider()
        //    };

        //    TempData["Message"] = JsonConvert.SerializeObject(msg);
        //}

        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }
    }
}