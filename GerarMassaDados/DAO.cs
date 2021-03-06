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
        public string valor;
        public string produto;
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

        public void Insere(string campoNome, string campoValor, string campoProduto)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("insert into " + tabela + "(nome, valor, produto) values (@nome, @valor, @produto)", minhaConexao);

            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@valor", campoValor);
            comando.Parameters.AddWithValue("@produto", campoProduto);
            comando.ExecuteNonQuery();

            Fechar();

        }

    }
}
