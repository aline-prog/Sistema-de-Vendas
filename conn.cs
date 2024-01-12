using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sistema_de_Vendas
{
    internal class conn
    {
        public String connec = "SERVER=localhost; DATABASE=dbsistema_vendas; UID=root; PID=; PORT=;";
        public MySqlConnection con = null;
        public void AbrirConexao()
        {
            try
            {
                con = new MySqlConnection(connec);
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro de Conexão"+ ex.Message );
            }
                   }
        public void FecharConexao()
        {
            try 
            {
                con = new MySqlConnection(connec);
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro de Conexão" + ex.Message);
            }
        }
    }
}
