using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp1
{
    class ProductProcessor
    {
        public static async Task<string> LoadProduct()
        {
            string url = "https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec";

            using (HttpResponseMessage respone = await ApiHelper.ApiClient.GetAsync(url)) 
            {
                if (respone.IsSuccessStatusCode)
                {
                    ProductModel product = await respone.Content.ReadAsAsync<ProductModel>();
                    Console.WriteLine("Pass");
                    return "Passs";
                }
                else
                {
                    Console.WriteLine("Fail");
                    return "Fail";

                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }
    }
}
