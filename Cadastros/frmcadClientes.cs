using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        string foto;


        public frmcadClientes()
        {
            InitializeComponent();
        }
        private byte[] img()
        {
            byte[] image_byte = null;
            if (foto == "")
            {
                return null;
            }
            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image_byte = br.ReadBytes((int)fs.Length);
            return image_byte;

        }
        private void Limparfoto()
        {
            pbFoto.Image = Properties.Resources.download; 
            foto = "download.png";
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
            txtnome.Focus();
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
                        sql = "INSERT INTO cad_clientes(nome_clientes, documento_clientes, endereco_clientes, bairro_clientes, numero_clientes, telefone_clientes, celular_clientes, foto_clientes) VALUES (@nome_clientes, @documento_clientes, @endereco_clientes, @bairro_clientes, @numero_clientes, @telefone_clientes, @celular_clientes, @foto)";
                        cmd = new MySqlCommand(sql, con.con);
                        cmd.Parameters.AddWithValue("@nome_clientes", txtnome.Text);
                        cmd.Parameters.AddWithValue("@documento_clientes", txtdocumento.Text);
                        cmd.Parameters.AddWithValue("@endereco_clientes", txtendereco.Text);
                        cmd.Parameters.AddWithValue("@bairro_clientes", txtbairro.Text);
                        cmd.Parameters.AddWithValue("@numero_clientes", txtnumero.Text);
                        cmd.Parameters.AddWithValue("@telefone_clientes", txttelefone.Text);
                        cmd.Parameters.AddWithValue("@celular_clientes", txtcelular.Text);
                        cmd.Parameters.AddWithValue("@foto", img());

                        cmd.ExecuteNonQuery();
                        con.FecharConexao();
                        //desabilitar botões e campos
                        txtnome.Enabled = false;
                        txtnome.Clear();
                        txtdocumento.Enabled = false;
                        txtdocumento.Clear();
                        txtendereco.Enabled = false;
                        txtendereco.Clear();
                        txtbairro.Enabled = false;
                        txtbairro.Clear();
                        txtnumero.Enabled = false;
                        txtnumero.Clear();
                        txttelefone.Enabled = false;
                        txttelefone.Clear();
                        txtcelular.Enabled = false;
                        txtcelular.Clear();
                        btnAdicionar.Enabled = false;
                        btnAlterar.Enabled = false;
                        btnExcluir.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnfoto.Enabled = false;
                        btnNovo.Enabled = true;
                        Limparfoto();
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

        private void btnfoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens(*.jpg; *.png) | *.jpg; *.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString();
                pbFoto.ImageLocation = foto;

            }
        }

        private void frmcadClientes_Load(object sender, EventArgs e)
        {
            Limparfoto();
        }

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcoes.DecNumber(sender, e);
        }

        private void txtnumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcoes.DecNumber(sender, e);
        }

        private void txttelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcoes.DecNumber(sender, e);
        }

        private void txtcelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            funcoes.DecNumber(sender, e);
        }

        private void txtdocumento_Leave(object sender, EventArgs e)
        {
            try
            {
                string documento = txtdocumento.Text;


                if (documento.Length > 11 )
                {
                    txtdocumento.Text = Convert.ToInt64(documento).ToString(@"00\.000\.000/0000-00");
                    //funcoes.validarcnpj(documento.Replace(".", "").Replace("/", "").Replace("-", ""));
                }
                else
                {
                    txtdocumento.Text = Convert.ToInt64(documento).ToString(@"000\.000\.000-00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("erro na formatação" + ex);
                txtdocumento.Clear();
                txtdocumento.Focus();


            }                      
            
        }
    }
    
}
