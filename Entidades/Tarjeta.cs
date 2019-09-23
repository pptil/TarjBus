using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
        public class Tarjeta
        {
            #region atributos         
            private int nrotarjeta;
            private int dni;
            private int saldo;
            private string nombre;
            #endregion

            #region Constructor         
            public Tarjeta()
            {
                nrotarjeta = 0;
                dni = 0;
                saldo = 0;
                nombre = string.Empty;
            }
            #endregion

            #region propiedades/encapsulamiento         
            public int NroTarjeta { get { return nrotarjeta; } set { nrotarjeta = value; } }
            public int Dni { get { return dni; } set { dni = value; } }
            public int Saldo { get { return saldo; } set { saldo = value; } }
            public string Nombre { get { return nombre; } set { nombre = value; } }
            #endregion
        }
}
