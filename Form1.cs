using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace DesafioFirebase
{
    public partial class Form1 : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "SAdFZyHodTih5cQIiplkiUtmcyt9T8hUswanIKUV",
            BasePath = "https://projetoimc-370fe-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Conectado! Insira suas Informações.");
            }
        }
        private async void btn_register_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                User = textBox1.Text,
                Password = textBox2.Text,
            };

            SetResponse response = await client.SetTaskAsync("Usuario/" + textBox1.Text, data);
            Data result = response.ResultAs<Data>();

        }



    }
}
