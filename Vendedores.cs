using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CAR
{
    internal class Vendedores
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;


        public Vendedores()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Vendedores";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["vendedor"];
            tabla.PrimaryKey = dc;
        }

        

        public DataTable Get_Tabla()
        {
            return tabla;
        }

        public void CargarLista(ListView lista)
        {
            foreach (DataRow filaVendedor in tabla.Rows)
            {
                ListViewItem item = lista.Items.Add(filaVendedor["nombre"].ToString());
                item.Tag = filaVendedor["vendedor"];
            }

        }

    }
}
