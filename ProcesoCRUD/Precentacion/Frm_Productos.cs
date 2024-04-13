using ProcesoCRUD.Datos;
using ProcesoCRUD.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesoCRUD.Precentacion
{
    public partial class Frm_Productos : Form
    {
        public Frm_Productos()
        {
            InitializeComponent();
        }

        #region Mis_Variable

        int nEstado_Guardado = 0;
        int vCodigo_pr = 0;

        #endregion


        #region "MIS METODOS"

        private void Limpiar_Texto()
        {

            txt_Descripcion_pr.Text = "";
            txt_Marca_pr.Text = "";
            txt_Stock_Actual.Text = "0.00";
            cmb_Medida.Text = "";
            cmb_Categoria.Text = "";
        }

        private void Estado_Texto(bool lEstado)
        {
            txt_Descripcion_pr.Enabled = lEstado;
            txt_Marca_pr.Enabled = lEstado;
            txt_Stock_Actual.Enabled = lEstado;
            cmb_Medida.Enabled = lEstado;
            cmb_Categoria.Enabled = lEstado;
        }

        private void Estado_Botones(bool lEstado)
        {

            btn_Cancelar.Visible = !lEstado;
            btn_Guardar.Visible = !lEstado;

            
            btn_Nuevo.Enabled = lEstado;
            btn_Actualizar.Enabled = lEstado;
            btn_Eliminar.Enabled = lEstado;
            btn_Reporte.Enabled = lEstado;
            btn_Salir.Enabled = lEstado;


            btn_Buscar.Enabled = lEstado;
        }

        private void Cargar_Medidas()
        {

            D_Productos Datos = new D_Productos();

            /* Usamos el metodo DataSource que significa ( recursos de datos ), Obtiene o establece el 
             * origen de datos cuyos datos se están mostrando en el control , El Valor que este devuelve
               ( Objeto que contiene los datos que se van a mostrar con el control )*/

            cmb_Medida.DataSource = Datos.Listado_me();

            /*Valor de propiedad .ValueMember (MiembroValor) : (Una String que representa un único nombre
             * de la propiedad del valor de propiedad DataSource, o una jerarquía de nombres
             * de propiedad delimitados por puntos que resuelve el nombre de una propiedad
             * del objeto final enlazado a datos. El valor predeterminado es una cadena vacía ("").)*/

            cmb_Medida.ValueMember = "codigo_me";

            /*Valor de propiedad .DisplayMember: Cadena que especifica el nombre de una propiedad de objeto contenida en la colección 
             * especificada por la propiedad DataSource. El valor predeterminado es una cadena vacía ("").*/
            cmb_Medida.DisplayMember = "descripcion_me";
        }

        private void Cargar_Categoras()
        {

            D_Productos Categorias = new D_Productos();

            cmb_Categoria.DataSource = Categorias.Listado_ca();
            cmb_Categoria.ValueMember = "codigo_cat";
            cmb_Categoria.DisplayMember = "descripcion_cat";
        }

        private void Formato_pr()
        {

            dgv_Listado_pr.Columns[0].Width = 90;
            dgv_Listado_pr.Columns[0].HeaderText = "CÓDIGO PR";
            dgv_Listado_pr.Columns[1].Width = 210;
            dgv_Listado_pr.Columns[1].HeaderText = "PRODUCTO";
            dgv_Listado_pr.Columns[2].Width = 120;
            dgv_Listado_pr.Columns[2].HeaderText = "MARCA";
            dgv_Listado_pr.Columns[3].Width = 120;
            dgv_Listado_pr.Columns[3].HeaderText = "MEDIDA";
            dgv_Listado_pr.Columns[4].Width = 120;
            dgv_Listado_pr.Columns[4].HeaderText = "CATEGORIA";
            dgv_Listado_pr.Columns[5].Width = 120;
            dgv_Listado_pr.Columns[5].HeaderText = "STOCK ACTUAL";
            dgv_Listado_pr.Columns[6].Visible = false;
            dgv_Listado_pr.Columns[7].Visible = false;

        }

        private void Listado_Pr(string cTexto)
        {
            D_Productos Datos = new D_Productos();

            dgv_Listado_pr.DataSource = Datos.Listado_pr(cTexto);

            this.Formato_pr();

        }

        private void Selecciona_Item_pr()
        {

            if (string.IsNullOrEmpty(Convert.ToString( dgv_Listado_pr.CurrentRow.Cells["codigo_pr"].Value) ) )
            {

                MessageBox.Show("No se tiene informacion para Visualizar",
                                 "Aviso de sistema",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);

            }
            else
            {

                this.vCodigo_pr = Convert.ToInt32(dgv_Listado_pr.CurrentRow.Cells["codigo_pr"].Value);
                txt_Descripcion_pr.Text = Convert.ToString(dgv_Listado_pr.CurrentRow.Cells["descripcion_pr"].Value);
                txt_Marca_pr.Text = Convert.ToString(dgv_Listado_pr.CurrentRow.Cells["marca_pr"].Value);
                cmb_Medida.Text = Convert.ToString(dgv_Listado_pr.CurrentRow.Cells["descripcion_me"].Value);
                cmb_Categoria.Text = Convert.ToString(dgv_Listado_pr.CurrentRow.Cells["descripcion_cat"].Value);
                txt_Stock_Actual.Text = Convert.ToString(dgv_Listado_pr.CurrentRow.Cells["stock_actual_pr"].Value);


            }

        }
        #endregion


        private void btn_Nuevo_Click(object sender, EventArgs e)
        {

            this.nEstado_Guardado = 1;// Nuevo Registro
            this.vCodigo_pr = 0;
            this.Limpiar_Texto();
            this.Estado_Texto(true);
            this.Estado_Botones(false);
            txt_Descripcion_pr.Select();

        }
        
        private void Frm_Productos_Load(object sender, EventArgs e)
        {
            this.Cargar_Medidas();
            this.Cargar_Categoras();

            this.Listado_Pr("%");/*Como este metodo esta a la espera de un parametro vamos
             * a de forma iniciar vamos a indicar que carge toda la informacion
             con el simbolo de porcentaje ( "%" ) este dimbolo practicamente crea un filtrado
            que le indica que debe mostrarnos todo el listado de productos*/

        }




        private void btn_Guardar_Click(object sender, EventArgs e)
        {

            if (txt_Descripcion_pr.Text == string.Empty ||
                txt_Marca_pr.Text == string.Empty||
                cmb_Medida.Text == string.Empty ||
                cmb_Categoria.Text == string.Empty ||
                txt_Stock_Actual.Text == string.Empty)//validar que todos los datos estan correctos
            {

                MessageBox.Show("Falta ingresar datos requeridos (*)",
                    "Aviso del sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            }
            else// guardar los datos
            {

                string Rpsta = "";

                E_Productos oPro = new E_Productos();

                oPro.Codigo_pr = this.vCodigo_pr;
                oPro.Descripcion_pr = txt_Descripcion_pr.Text;
                oPro.Marca_pr = txt_Marca_pr.Text;
                oPro.Codigo_me = Convert.ToInt32( cmb_Medida.SelectedValue);
                oPro.Codigo_ca = Convert.ToInt32(cmb_Categoria.SelectedValue);
                oPro.Stock_actual = Convert.ToDecimal(txt_Stock_Actual.Text);

                D_Productos Datos = new D_Productos();

                Rpsta = Datos.Guardar_pr(this.nEstado_Guardado, oPro);

                if(Rpsta == "OK")
                {

                    MessageBox.Show("Los datos han sido guardados correctamente",
                                     "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /*Le indicamos que imediatamente se realize la accion de actualizar que regrese
                      el codigo producto a un estado de 0 ( vCodigo_pr = 0 ) */

                    this.vCodigo_pr = 0;
                    
                    this.Limpiar_Texto();
                    this.Estado_Texto(false);
                    this.Estado_Botones(true);
                }

                this.Listado_Pr("%");

            }

        }

        private void dgv_Listado_pr_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Selecciona_Item_pr();

        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {

            this.nEstado_Guardado = 2; // Actualizar Registro
            this.Estado_Texto(true);
            this.Estado_Botones(false);
            txt_Descripcion_pr.Select();

        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {

            this.Listado_Pr(txt_Buscar.Text);

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Estado_Botones(true);
            this.Limpiar_Texto();
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {



        }
    }
}
