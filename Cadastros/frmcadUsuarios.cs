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

namespace Sistema_de_Vendas.Cadastros
{
    public partial class frmcadUsuarios : Form
    {
        conn con = new conn();
        string sql;
        MySqlCommand cmd;

        public frmcadUsuarios()
        {
            InitializeComponent();
        }
       
        private void btnNovo_Click(object sender, EventArgs e)
        {
         //habilitar botões e campos
            txtNome.Enabled = true;
            txtSenha.Enabled = true;
            cbPermissoes.Enabled = true;
            btnAdicionar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNovo.Enabled = false;
            cbPermissoes.Text = "Administrador";
            txtNome.Focus();
        }
        //verificar se campos estão vazios
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite um nome para o usuário!");
                txtNome.Clear();
                txtNome.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite uma senha para o usuário!");
                txtSenha.Clear();
                txtSenha.Focus();
                return;
                
            }
            else
            {
                try
                {
                    con.AbrirConexao();
                    //inserir dados na tabela
                    sql = "INSERT INTO cad_usuarios(nome_usuarios, senha_Usuarios, permissoes_Usuarios) VALUES (@nome_usuarios, @senha_Usuarios, @permissoes_Usuarios)";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@nome_usuarios", txtNome.Text);
                    cmd.Parameters.AddWithValue("@senha_Usuarios", txtSenha.Text);
                    cmd.Parameters.AddWithValue("@permissoes_Usuarios", cbPermissoes.Text);
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();
                    //desabilitar botões e campos
                    txtNome.Enabled = false;
                    txtNome.Clear();
                    txtSenha.Enabled = false;
                    txtSenha.Clear();
                    cbPermissoes.Enabled = false;
                    btnAdicionar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnNovo.Enabled = true;
                    MessageBox.Show("Usuário cadastado com sucesso!");
                }
                catch (Exception ex )
                {

                    MessageBox.Show("Erro ao cadastrar!" + ex);
                }
                
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
