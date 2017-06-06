using System;
using System.Windows.Forms;
using System.Xml;
using VigoNFE;

namespace NFTEST
{
    public partial class Form1 : Form
    {
        NFE nfe = new NFE();
        string ambiente = "2"; // HOMOLOGAÇÃO COMO PADRÃO INICIAL

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ambiente = (comboBox1.Text == "HOMOLOGAÇÃO") ? "2" : "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = nfe.status(ambiente, comboBox2.Text).OuterXml;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = nfe.protocolo(ambiente, comboBox2.Text, "51140415962533000121550010000151621612223879").OuterXml;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = nfe.cancela(ambiente, comboBox2.Text, "42110403452234000145550010000000281765232806", "99999999999999", "351000079426099").OuterXml;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Trabalho\nfe.xml");

            textBox1.Text = nfe.envia(ambiente, comboBox2.Text, doc.OuterXml).OuterXml;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = nfe.retorno(ambiente, comboBox2.Text, "151140014988063").OuterXml;
        }
    }
}
