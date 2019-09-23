using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace DatosTarjeta
{
    public class DatosRedBus : DatosConexionBD
    {
        public int Saldo(string accion, int saldo, Tarjeta tarjeta)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Sumar")
            { orden = "update Tarjetas set Saldo= " + tarjeta.Saldo + "+" + saldo + "where NroTarjeta =" + tarjeta.NroTarjeta; }
            if (accion == "Restar")
            { orden = "update Tarjetas set Saldo= " + tarjeta.Saldo + "-" + saldo + "where NroTarjeta =" + tarjeta.NroTarjeta; }

            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de Agregar o Restar Saldo", e);
            }

            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return resultado;

        }
        public int abmTarjetas(string accion, Tarjeta objtarjeta)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
            { orden = "insert into Tarjetas values (" + objtarjeta.NroTarjeta + ", '" + objtarjeta.Nombre + "'," + objtarjeta.Dni + "," + objtarjeta.Saldo + ");"; }

            if (accion == "Modificar")
            { orden = "update Tarjetas set Nombre='" + objtarjeta.Nombre + "', Dni =" + objtarjeta.Dni + ", Saldo =" + objtarjeta.Saldo +  "where NroTarjeta=" + objtarjeta.NroTarjeta + ";"; }



            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de guardar,borrar o modificar Tarjetas", e);
            }

            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return resultado;
        }

      
        public DataSet listadoTarjetas(string cual)
        {
            string orden = string.Empty;

            if (cual != "Todos")
                orden = "select * from Tarjetas where NroTarjeta = " + int.Parse(cual) + ";";
            else orden = "select * from Tarjetas;";

            SqlCommand cmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet(); SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Abrirconexion(); cmd.ExecuteNonQuery();

                da.SelectCommand = cmd; da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al listar Tarjetas", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return ds;
        }
    }
} 
    

