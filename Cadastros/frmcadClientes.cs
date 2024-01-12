using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Vendas
{
    public partial class frmcadClientes : Form
    {
        conn con = new conn();
        string sql;
        MySqlCommand cmd;
        public frmcadClientes()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //habilita campos e botões 
            txtnome.Enabled = true;
            txtdocumento.Enabled = true;
            txtendereco.Enabled = true;
            txtbairro.Enabled = true;
            txtnumero.Enabled = true;
            txttelefone.Enabled = true;
            txtcelular.Enabled = true;
            btnNovo.Enabled = false;
            btnAdicionar.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            btnfoto.Enabled = true;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtnome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite um nome para o usuário!");
                txtnome.Clear();
                txtnome.Focus();
                return;
            }
            if (txtdocumento.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite uma senha para o usuário!");
                txtdocumento.Clear();
                txtdocumento.Focus();
                return;

            }
            else
            {       //verifica se o usuário já exite no banco de dados
                try
                {
                    con.AbrirConexao();
                    MySqlCommand cmdVerificar;
                    MySqlDataReader reader;
                    cmdVerificar = new MySqlCommand("SELECT * FROM cad_clientes WHERE nome_clientes = @usuario", con.con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    cmdVerificar.Parameters.AddWithValue("@usuario", txtnome.Text);
                    reader = cmdVerificar.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Cliente já cadastrado!");
                        txtnome.Clear();
                        txtdocumento.Clear();
                        txtendereco.Clear();
                        txtbairro.Clear();
                        txtnumero.Clear();
                        txttelefone.Clear();
                        txtcelular.Clear();
                        
                        txtnome.Focus();
                        con.FecharConexao();
                        return;
                    }
                    else
                    {

                        //insere dados na tabela
                        con.AbrirConexao();
                        sql = "INSERT INTO cad_clientes(nome_clientes, documento_clientes, endereco_clientes, bairro_clientes, numero_clientes, telefone_clientes, celular_clientes) VALUES (@nome_clientes, @documento_clientes, @endereco_clientes, @bairro_clientes, @numero_clientes, @telefone_clientes, @celular_clientes)";
                        cmd = new MySqlCommand(sql, con.con);
                        cmd.Parameters.AddWithValue("@nome_clientes", txtnome.Text);
                        cmd.Parameters.AddWithValue("@documento_clientes", txtdocumento.Text);
                        cmd.Parameters.AddWithValue("@endereco_clientes", txtendereco.Text);
                        cmd.Parameters.AddWithValue("@bairro_clientes", txtbairro.Text);
                        cmd.Parameters.AddWithValue("@numero_clientes", txtnumero.Text);
                        cmd.Parameters.AddWithValue("@telefone_clientes", txttelefone.Text);
                        cmd.Parameters.AddWithValue("@celular_clientes", txtcelular.Text);
                        //cmd.Parameters.AddWithValue("@foto_clientes", img);
                        cmd.ExecuteNonQuery();
                        con.FecharConexao();
                        //desabilitar botões e campos
                        txtnome.Enabled = false;
                        txtnome.Clear();
                        txtdocumento.Enabled = false;
                        txtdocumento.Clear();
                        txtendereco.Enabled = false;
                        txtendereco.Clear() ;
                        txtbairro.Enabled = false;
                        txtbairro.Clear() ;
                        txtnumero.Enabled = false;
                        txtnumero.Clear();
                        txttelefone.Enabled = false;
                        txttelefone.Clear() ;
                        txtcelular.Enabled = false;
                        txtcelular.Clear() ;
                        btnAdicionar.Enabled = false;
                        btnAlterar.Enabled = false;
                        btnExcluir.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnfoto.Enabled = false;
                        btnNovo.Enabled = true;
                        btnNovo.Focus();
                       
                        MessageBox.Show("Cliente cadastado com sucesso!");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao cadastrar!" + ex);
                }

            }

        }
    }
}
