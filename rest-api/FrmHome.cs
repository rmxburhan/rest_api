using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rest_api
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }
        class entityUser
        {
            public int id { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string avatar { get; set; }
        }

        private async void FrmHome_Load(object sender, EventArgs e)
        {
            //var response = await RestHelper.getAll();
            //textBox1.Text = response;
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
                            JObject jObject = JObject.Parse(data);
                            JArray ojObject = (JArray)jObject["data"];
                            List<EntityUserData> list = new List<EntityUserData>();
                            for (int i = 0; i < ojObject.Count; i++)
                            {
                                JObject result = JObject.Parse(ojObject[i].ToString());
                                list.Add(new EntityUserData
                                {
                                    id = Convert.ToInt32(result["id"].ToString()),
                                    email = result["email"].ToString(),
                                    first_name = result["first_name"].ToString(),
                                    last_name = result["last_name"].ToString(),
                                    avatar = result["avatar"].ToString()
                                }); 
                            }
                            entityUserDataBindingSource.DataSource = list;
                        }
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var person = new create() { name = txtName.Text, job = txtJob.Text};

            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://reqres.in/api/users";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                JObject halo = (JObject)JObject.Parse(result);
                MessageBox.Show(halo.ToString());
            }
        }

        class create
        {
            public string name { get; set; }
            public string job { get; set; }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var url = $"https://reqres.in/api/users/{txtId.Text}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Delete success");
            }
            else
            {
                MessageBox.Show("fail");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var person = new create() { name = txtName.Text, job = txtJob.Text };

            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://reqres.in/api/users/{txtId.Text}";
            var client = new HttpClient();

            var response = await client.PutAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
            JObject halo = (JObject)JObject.Parse(result);
            MessageBox.Show(halo.ToString());
        }
    }
}
