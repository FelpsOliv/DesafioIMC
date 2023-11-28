using FireSharp.Config;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Diagnostics.Eventing.Reader;

namespace DesafioFirebase
{
    public partial class Form2 : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "SAdFZyHodTih5cQIiplkiUtmcyt9T8hUswanIKUV",
            BasePath = "https://projetoimc-370fe-default-rtdb.firebaseio.com/"
        };

        public static string usuarioglobal = "";

        IFirebaseClient client;

        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 register = new Form1();
            register.Show();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            FuncLogin();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
        
        }
        public Data FuncLogin()
        {
            client = new FireSharp.FirebaseClient(config);
            Data result = new Data();

            FirebaseResponse response = client.Get(@"Usuario/" + textBox1.Text);
            Data objres = response.ResultAs<Data>();
            Data curobj = new Data()
            {
                User = textBox1.Text,
                Password = textBox2.Text
            };

            if (objres.Password != curobj.Password)
            {
                MessageBox.Show("Usuario não encontrado!");
                result = null;
            }
            else
            {
                MessageBox.Show("Login realizado!");
                result = objres;
                usuarioglobal = objres.User;
                Form3 infoImc = new Form3();
                infoImc.Show();
            }
            return result;
        }
    }
}