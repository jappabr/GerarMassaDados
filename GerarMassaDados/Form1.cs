using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerarMassaDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        DAO dao = new DAO();

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text += textBox1.Text;
            }
            else
            {
                richTextBox1.Text += "\n" + textBox1.Text;
            }

            richTextBox1.SaveFile("nomes.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
            {
                richTextBox2.Text += textBox2.Text;
            }
            else
            {
                richTextBox2.Text += "\n" + textBox2.Text;
            }
            richTextBox2.SaveFile("nomes2.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "")
            {
                richTextBox3.Text += textBox3.Text;
            }
            else
            {
                richTextBox3.Text += "\n" + textBox3.Text;
            }
            richTextBox3.SaveFile("nomes3.txt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile("nomes.txt");
                richTextBox2.LoadFile("nomes2.txt");
                richTextBox3.LoadFile("nomes3.txt");
            }
            catch
            {
                MessageBox.Show("Os arquivos de txt pra nome e sobrenome ainda não existem. Prossiga criando-os");
            }
            try
            {
                dao.Conecte("bdcliente", "tabela");
                dao.PreencheTabela(dataGridView1);
                MessageBox.Show("Banco de Dados conectado");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Conexão não estabelecida, feche o programa e verifique o erro", "ATENÇÃO");
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int qtde = int.Parse(textBox4.Text);
            for (int i = 0; i < qtde; i++)
            {
                Random rnd = new Random();
                //Gerar nome aleatorio
                int linha = rnd.Next(0, richTextBox1.Lines.Length);
                string nome = richTextBox1.Lines[linha];
                //Gerar sobrenome aleatorio
                int linha2 = rnd.Next(0, richTextBox2.Lines.Length);
                string sobrenome = richTextBox2.Lines[linha2];
                //Gerar sobrenome2 aleatorio
                int linha3 = rnd.Next(0, richTextBox3.Lines.Length);
                string sobrenome2 = richTextBox3.Lines[linha3];
                //nome completo
                string nomeCompleto = nome + " " + sobrenome + " " + sobrenome2;

                //Gerar endereço aleatorio 
                int linha4 = rnd.Next(0, richTextBox1.Lines.Length);
                nome = richTextBox1.Lines[linha4];
                int linha5 = rnd.Next(0, richTextBox2.Lines.Length);
                sobrenome = richTextBox2.Lines[linha5];
                string endereco = "Rua " + nome + " " + sobrenome + ", " + rnd.Next(10, 2500);

                //gerar cpf aleatório
                //formato para o CPF 999.999.999-99

                string cpf = rnd.Next(100, 1000).ToString() + "." + rnd.Next(100, 1000).ToString() +
                    "." + rnd.Next(100, 1000).ToString();

                // gerar um telefone aleatório
                // formato para o telefone 99-99999-9999

                string telefone = rnd.Next(10, 100).ToString() + "-" + rnd.Next(2000, 100000).ToString() +
                    "-" + rnd.Next(1000, 100000).ToString() ;

                //inserir no BD
                dao.Insere(nomeCompleto, endereco, telefone, cpf);
            
                string dados = nomeCompleto + " " + endereco + " " + cpf + " " + telefone;

                if (richTextBox4.Text == "")
                    richTextBox4.Text += dados;
                else
                    richTextBox4.Text += "\n" + dados;

            } // fim do for

            richTextBox4.SaveFile("nomeCompleto.txt", RichTextBoxStreamType.PlainText);

            dao.PreencheTabela(dataGridView1);
        }
    }
}
