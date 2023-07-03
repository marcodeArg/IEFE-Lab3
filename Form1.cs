using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;


namespace CAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Vendedores objVendedores;
        Ventas objVentas;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                objVendedores = new Vendedores();
                objVentas = new Ventas();
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo cargar la base de datoss", "Error", MessageBoxButtons.OK);
                Application.Exit();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();

            int desde = Convert.ToInt32(txtDesde.Text);
            int hasta = Convert.ToInt32(txtHasta.Text);
            int ventas = 0;
            int ventasTotal = 0;
            
            DataTable tablaVentas = objVentas.Get_Tabla();


            for (int anio = desde; anio >= desde && anio <= hasta; anio++)
            {
                Series serie = chart.Series.Add(anio.ToString());

                foreach (ListViewItem item in lstVendedores.CheckedItems)
                {
                    foreach (DataRow filaVentas in tablaVentas.Rows)
                    {
                        if (filaVentas["vendedor"].ToString() == item.Tag.ToString())
                        {
                            if ((int)filaVentas["aa"] == anio)
                            {
                                ventas += (int)filaVentas["cantidad"];
                            }   
                        }
                        
                    }
                    serie.Points.AddXY(item.Text, ventas);
                    ventasTotal = ventasTotal + ventas;
                    ventas = 0;
                }
            }
            toolStripStatusLabel1.Text = ventasTotal.ToString();
        }
    }
}
