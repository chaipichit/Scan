using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp1
{
    public class ApiHelper
    {
        public static System.Net.Http.HttpClient ApiClient { get; set; } = new System.Net.Http.HttpClient();

        public static void initializeClient()
        {
            ApiClient = new System.Net.Http.HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
