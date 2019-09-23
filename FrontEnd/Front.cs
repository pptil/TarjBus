using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Capsula;

namespace FrontEnd
{
    public partial class Front : Form
    {
        public Tarjeta objTarjeta = new Tarjeta();
        public CapsulaTarjeta objCapsulaTarjeta = new CapsulaTarjeta();

        public Front()
        {
           
            InitializeComponent();
            ArmarDGV();
            LlenarDGV();
        }

        private void ArmarDGV()
        {
            dgvTarjetas.ColumnCount = 4;
            dgvTarjetas.Columns[0].HeaderText = "NroTarjeta";
            dgvTarjetas.Columns[1].HeaderText = "Nombre";
            dgvTarjetas.Columns[2].HeaderText = "Dni";
            dgvTarjetas.Columns[3].HeaderText = "Saldo";
            dgvTarjetas.Columns[0].Width = 70;
            dgvTarjetas.Columns[1].Width = 70;
            dgvTarjetas.Columns[2].Width = 70;
            dgvTarjetas.Columns[3].Width = 70;
        }
        private void LlenarDGV()
        {
            dgvTarjetas.Rows.Clear();

            DataSet ds = new DataSet();
            ds = objCapsulaTarjeta.listadoTarjetas("Todos");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                   
                    dgvTarjetas.Rows.Add(dr[0].ToString(), dr[1], dr[2], dr[3]);
                }
            }
            else
            { lblMensaje.Text = "No hay Tarjetas cargadas en el sistema"; }
        }

        private void TxtBox_a_Obj()
        {
            objTarjeta.NroTarjeta = int.Parse(txtNro.Text);
            objTarjeta.Nombre = txtNombre.Text;                      
            objTarjeta.Dni = int.Parse(txtDni.Text);
            objTarjeta.Saldo = int.Parse(txtSaldo.Text);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
             int nGrabados = -1;           
            TxtBox_a_Obj(); 
 
            nGrabados = objCapsulaTarjeta.abmTarjetas("Alta", objTarjeta);

            if (nGrabados == -1)
            { lblMensaje.Text = "  No pudo grabar la Tarjeta en el sistema"; }
            else
            {
                lblMensaje.Text = "  Se grabó con éxito Tarjeta.";
                LlenarDGV();
                Limpiar();
            }
        }
        private void Limpiar()
        { txtNro.Text = string.Empty; txtNombre.Text = string.Empty; txtDni.Text = string.Empty; txtSaldo.Text = string.Empty; }

        private void Ds_a_TxtBox(DataSet ds)
        {
            txtNro.Text = ds.Tables[0].Rows[0]["NroTarjeta"].ToString();
            txtNombre.Text = ds.Tables[0].Rows[0]["Nombre"].ToString();
            txtDni.Text = ds.Tables[0].Rows[0]["Dni"].ToString();
            txtSaldo.Text = ds.Tables[0].Rows[0]["Saldo"].ToString();
            txtNro.Enabled = false;
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            int nResultado = -1;
            TxtBox_a_Obj();
            nResultado = objCapsulaTarjeta.abmTarjetas("Modificar", objTarjeta);          
            if (nResultado != -1)
            {
                MessageBox.Show("Aviso", "La Tarjeta fue Modificada con éxito");
                Limpiar();
                LlenarDGV();
                txtNro.Enabled = true;
                
            }
            else
                MessageBox.Show("Error", "Se produjo un error al intentar modificar la tarjeta"); 
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            int nResultado = -1;
            nResultado = objCapsulaTarjeta.SumRes("Sumar", int.Parse(txtAddRes.Text), objTarjeta);
            if (nResultado != -1)
            {
                MessageBox.Show("Aviso", "La Suma fue hecha con éxito");
                Limpiar();
                LlenarDGV();
                txtNro.Enabled = true;
            }
            else
                MessageBox.Show("Error", "Se produjo un error al intentar Sumar saldo a la tarjeta");
        }

        private void BtnDescontar_Click(object sender, EventArgs e)
        {
            int nResultado = -1;
            nResultado = objCapsulaTarjeta.SumRes("Restar", int.Parse(txtAddRes.Text), objTarjeta);
            if (nResultado != -1)
            {
                MessageBox.Show("Aviso", "El Descuento fue hecho con éxito");
                Limpiar();
                LlenarDGV();
                txtNro.Enabled = true;
            }
            else
                MessageBox.Show("Error", "Se produjo un error al intentar descontar saldo de la tarjeta");
        }

        private void DgvTarjetas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();

            objTarjeta.NroTarjeta = Convert.ToInt32(dgvTarjetas.CurrentRow.Cells[0].Value);

            ds = objCapsulaTarjeta.listadoTarjetas(objTarjeta.NroTarjeta.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                Ds_a_TxtBox(ds);
                btnGrabar.Visible = false;
                lblMensaje.Text = string.Empty;
            }
        }

        private void TxtDni_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
