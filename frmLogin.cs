using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Vendas
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        conn con = new conn();
        string sql;

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            //testa os campos se estão vazios
            if (txtUsuario.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite um Usuário Válido!");
                txtUsuario.Clear();
                txtUsuario.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite uma Senha Válida!");
                txtSenha.Clear();
                txtSenha.Focus();
                return;
            }
            try
            {
                //conecta ao banco de dados e verifica se corresponde os dados inseridos
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;
                cmdVerificar = new MySqlCommand("SELECT * FROM cad_usuarios WHERE nome_usuarios = @usuario AND senha_Usuarios = @senha ", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
                reader = cmdVerificar.ExecuteReader();
                if (reader.HasRows)
                {
                    frmPrincipal frmPrincipal = new frmPrincipal();
                    frmPrincipal.ShowDialog();
                    con.FecharConexao();
                    Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorreto!");
                    txtUsuario.Clear();
                    txtSenha.Clear();
                    txtUsuario.Focus();
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na consulta!" +ex);

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}

