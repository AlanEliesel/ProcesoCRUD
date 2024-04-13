using ProcesoCRUD.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUD.Datos
{
    /*Ya en esta clase vamos a usar la conexion para interactuar directamente con lo datos del 
     gestor de base de datos desde el lenguaje de programacion c#*/
    public class D_Productos
    {
        //Este metodo es tipo DataTable por que nos va a retornar un contenido de datos.
        public DataTable Listado_pr(string cTexto)
        {
            //(SqlDataReader) este nos ayuda a obtener la informacion que se va a extraer de sqlserver.
            SqlDataReader resultado;
            //ahora vamos a diseñar una tabla a donde vamos a recivir toda esa informacion o donde la vamos a alojar.
            DataTable tabla = new DataTable();
            //Instanciamos la conexion.
            SqlConnection sqlcon = new SqlConnection();

            try
            {
                /*Ahora extablecemos la conexion indicando que la conexion sqlcon va a contener la conexion
                 previamente hecha osea en la clase (Conexion) la cual llamamos y usamos los metodos de esta
                que contienen los datos de la conexion el metodo (getInstancia) y el metodo (CrearConexion)*/

                sqlcon = Conexion.getInstancia().CrearConexion();

                /*Usamos (SqlCommand) para crear un proceso de trabajo creamos o instanciamos el objeto 
                 * este nos ayuda a indicar la tarea que vamos a relizar
                 * (new SqlCommad( Aqui es donde vamos a alojar el procedimiento almacenado)
                 * y luego con una coma ( , es aqui donde le indicamos la conexion) y es aqui 
                 * cuando practicamente ya enganchamos el procediminto junto a conexion ) 
                 * ya tiene toda la comunicacion para llevarnos a sqlserver y aplicarnos el procedimineto almacenado*/

                SqlCommand comando = new SqlCommand("SP_LISTADO_PR", sqlcon);

                /*Pero este procedimiento almacendao tiene que ir con unas siertas configuraciones donde vamos a 
                 * utilizar comandos como commadtype donde le indicamos que vamos a trabajar con procedimiento almacendo.*/

                comando.CommandType = CommandType.StoredProcedure;

                /*tambien como vamos a utilizar parametros hacemos uso del atributo ( Parameters ) 
                 * y añadimos atravez de este el parametro que vamos a uzar ej:
                 * ( parameters.Add( aqui ponemos el parametro ) luego con una coma le indicamos el tipo de dato 
                 * (,SqlDbType.VarChar) luego de acceder al parametro del procedimiento le indicamos que va a almacenar 
                 el parametro del metodo ( public DataTable Listado_pr(string cTexto) ) de la diguiente manera
                despues del parentesis le asignamos el valor ( .Value = cTexto ; )*/

                comando.Parameters.Add("@cTexto",SqlDbType.VarChar).Value = cTexto ;

                //luego abrimos la conexion.

                sqlcon.Open();

                //Ahora utilizamos la variable resultado para recivir la informacion que va a llegar de Sqlserver

                resultado = comando.ExecuteReader();

                /*luego vamos a usar la variable tabla para que reciva la informacion de resultado y que se almacene en la tabla*/

                tabla.Load(resultado);

                //Al final la informacion de la tabla es la que vamos a retornar de este metodo.

                return tabla;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }


        public string Guardar_pr(int nOpcion,E_Productos oPro)// oPro = objeto propiedades
        {

            string Rpsta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {

                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_GUARDAR_PR", sqlcon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Opcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@nCodigo_pr", SqlDbType.Int).Value = oPro.Codigo_pr;
                comando.Parameters.Add("@cDescripcion_pr", SqlDbType.VarChar).Value = oPro.Descripcion_pr;
                comando.Parameters.Add("@cMarca_pr", SqlDbType.VarChar).Value = oPro.Marca_pr;
                comando.Parameters.Add("@nCodigo_me", SqlDbType.Int).Value = oPro.Codigo_me;
                comando.Parameters.Add("@nCodigo_ca", SqlDbType.Int).Value = oPro.Codigo_ca;
                comando.Parameters.Add("@nStock_actual", SqlDbType.Decimal).Value = oPro.Stock_actual;
                sqlcon.Open();

                /*Aqui le estamos indicando a la variavle respuesta (Rpsta) le estamos indicando lo siguiente 
                 * usamos el metodo para lo siquiente ( .ExecuteNonQuery() ) este ejecuta una instrucción 
                 * Transact-SQL en la conexión y devuelve el número de filas afectadas.*/

                /*En base al resultado que nos devuelva el metodo ( .ExecuteNonQuery() ) hacemos una 
                 condicional si el resultado del metodo es mayor o igual a 1 ( >= 1 ) entonces nos devolvera 
                como respuesta "OK" caso contrario ( : ) nos devorvera un mensaje indicando que no se pudo 
                registrar los datos*/

                Rpsta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo registrar los datos";

            }
            catch (Exception ex)
            {
                Rpsta = ex.Message;

            }
            finally
            {
                if(sqlcon.State == ConnectionState.Open) { sqlcon.Close(); }
            }
            return Rpsta;
        }


        public string Activo_pr(int nCodigo,bool bEstado_activo)
        {
            string Rpsta = "";

            SqlConnection sqlcon = new SqlConnection();

            try
            {

                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("ACTIVO_PR", sqlcon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nCodigo_pr", SqlDbType.Int).Value = nCodigo;
                comando.Parameters.Add("@bEstado_activo", SqlDbType.Bit).Value = bEstado_activo;
                sqlcon.Open();
                Rpsta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo cambiar el estado activo del producto";

            }
            catch (Exception ex)
            {
                Rpsta = ex.Message;
            }
            finally
            {
                if(sqlcon.State == ConnectionState.Open) { sqlcon.Close(); }
            }
            return Rpsta;
        }


        public DataTable Listado_me()
        {
            SqlDataReader resultado;

            DataTable tabla = new DataTable();

            SqlConnection sqlcon = new SqlConnection();

            try
            {

                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_LISTADO_ME", sqlcon);
                comando.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }


        public DataTable Listado_ca()
        {
            SqlDataReader resultado;

            DataTable tabla = new DataTable();

            SqlConnection sqlcon = new SqlConnection();

            try
            {

                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_LISTADO_CAT", sqlcon);
                comando.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }
    }
}
