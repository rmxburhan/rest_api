using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace rest_api
{
    public class RestHelper
    {
        public static async Task<string> getAll()
        {   
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://reqres.in/");
                using (HttpResponseMessage response = await client.GetAsync("api/users"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }    
                    }
                }
            }
            return string.Empty;
        }
    }
}
