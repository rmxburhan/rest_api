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
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" && txtPassword.Text != "")
            {
                login register = new login() { email = txtEmail.Text, password = txtPassword.Text };
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://reqres.in/");
                using (HttpResponseMessage response = client.PostAsJsonAsync("api/register", register).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Login berhasil");
                        FrmLogin frm = new FrmLogin();
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Registrasi gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FrmRegister_Load(object sender, EventArgs e)
        {

        }
        class login
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
    
}
