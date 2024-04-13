using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUD.Datos
{
    public class Conexion
    {
        //Creamos los campos para establecer la conexion

        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private static Conexion con = null;


        //creamos el metodo que va a almacenar los datos
        private Conexion()
        {
            this.Base = "BD_CRUD";
            this.Servidor = "Alan23";
            this.Usuario = "User_al";
            this.Clave = "2308";
        }

        //Este metodo nos ayuda a crear la 
        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();

            
            try
            {
                cadena.ConnectionString = "Server=" + this.Servidor +
                                          ";Database=" + this.Base +
                                          ";User Id=" + this.Usuario +
                                          ";Password=" + this.Clave;
            }
            catch (Exception ex)
            {
                cadena = null;
                throw ex;
            }
            return cadena;
        }

        //Con este metodo vamos a evaluar si la conexion de encuentra abierta o serrada...
        /* Este metodo nos ayuda a que no se sobre cargue el sistema, en cuanto no este en ejecucion 
         la base de datos se serrara la conecion */
        
        public static Conexion getInstancia()
        {

            if (con == null)
            {
                con = new Conexion();
            }
            return con;

        }

    }
}
