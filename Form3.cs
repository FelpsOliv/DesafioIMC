using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DesafioFirebase
{
    public partial class Form3 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "SAdFZyHodTih5cQIiplkiUtmcyt9T8hUswanIKUV",
            BasePath = "https://projetoimc-370fe-default-rtdb.firebaseio.com/"
        };

        string usuarioglobal = "";
        string usuarioPeso = "";
        string usuarioAltura = "";

        IFirebaseClient client;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            using (Form2 form = new Form2())
            {
                usuarioglobal = Form2.usuarioglobal;
            }

            var resultTabela = tabela();
            textBox1.Text = resultTabela != null ? resultTabela.Peso.ToString() : "0";
            textBox2.Text = resultTabela != null ? resultTabela.Altura.ToString() : "0";
            label4.Text = resultTabela != null ? resultTabela.imc.ToString("F1") : "0";
        }

        private Info tabela()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get(@"Informacao/" + usuarioglobal);
            if (response.Body == "null")
            {
                return null;
            }
            else
            {
                Info objres = response.ResultAs<Info>();
                return objres;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            var info = new Info
            {
                Peso = Convert.ToDouble(textBox1.Text),
                Altura = Convert.ToDouble(textBox2.Text),
            };

            SetResponse responseImc = await client.SetTaskAsync("Informacao/" + usuarioglobal, info);
            Info resultImc = responseImc.ResultAs<Info>();
            usuarioPeso = textBox1.Text;
            usuarioAltura = textBox2.Text;

            MessageBox.Show("Informação inserida " + resultImc.User);


        }

        private async void button2_Click(object sender, EventArgs e)
        {

            client = new FireSharp.FirebaseClient(config);

            var info = new Info
            {
                Peso = Convert.ToDouble(usuarioPeso),
                Altura = Convert.ToDouble(usuarioAltura),
                imc = Convert.ToDouble(textBox1.Text) / (Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox2.Text))
            };

            label4.Text = info.imc.ToString("F1");

            SetResponse responseImc = await client.SetTaskAsync("Informacao/" + usuarioglobal, info);
            Info resultImc = responseImc.ResultAs<Info>();

            if(resultImc.imc <= 18.5)
            {
                label5.Text = "Abaixo do peso, vá comer.";
            }
            else if(resultImc.imc >= 18.6 &&  resultImc.imc <= 24.9)
            {
                label5.Text = "Peso ideal, parabéns.";
            } 
            else if(resultImc.imc >= 25.0 && resultImc.imc <= 29.9)
            {
                label5.Text = "Levemente acima do peso.";
            }
            else if (resultImc.imc >= 30.0 && resultImc.imc <= 34.9)
            {
                label5.Text = "Obesidade grau 1.";
            }
            else if(resultImc.imc >= 35.0 && resultImc.imc <= 39.9)
            {
                label5.Text = "Obesidade grau 2.";
            }
            else if(resultImc.imc > 40)
            {
                label5.Text = "Obesidade grau 3.";
            }

        }


    }
}
