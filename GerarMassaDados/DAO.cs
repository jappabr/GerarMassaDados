using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GerarMassaDados
{
    public class Campos{
        public int id;
        public string nome;
        public string endereco;
        public string telefone;
        public string cpf;
    }

    public class DAO
    {
        public DAO()
        {
        }

        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;

        public string usuarioBD = "root";
        public string senhaBD = "admin";
        public string servidor = "localhost";
        string bancoDados;
        string tabela;

        public void Conecte(string BancoDados, string Tabela) // Metodo para a conexão.
        {
            bancoDados = BancoDados;
            tabela = Tabela;

            minhaConexao = new MySqlConnection(
                "server=" + servidor + "; database=" + bancoDados + "; uid=" + usuarioBD + "; password=" + senhaBD);
        }
        void Abrir()
        {
            minhaConexao.Open();
        }
        void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela, minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();

        }

        public void Insere(string campoNome, string campoEndereco, string campoTelefone, string campoCPF)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("insert into " + tabela + "(nome, endereco, telefone, cpf) values (@nome, @endereco, @telefone, @cpf)", minhaConexao);

            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@endereco", campoEndereco);
            comando.Parameters.AddWithValue("@telefone", campoTelefone);
            comando.Parameters.AddWithValue("@cpf", campoCPF);
            comando.ExecuteNonQuery();

            Fechar();

        }

    }
}
