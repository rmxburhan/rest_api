using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rest_api
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        class login
        {
            public string email { get; set; }
            public string password { get; set; }
        }

  


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" && txtPassword.Text != "")
            {
                login login = new login() { email = txtEmail.Text, password = txtPassword.Text };
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://reqres.in/");
                using (HttpResponseMessage response = client.PostAsJsonAsync("api/login", login).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        FrmHome frm = new FrmHome();
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username atau password salah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmail.Clear();
                        txtPassword.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lengkapi semuad data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

      
        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister frm = new FrmRegister();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
    }
}
