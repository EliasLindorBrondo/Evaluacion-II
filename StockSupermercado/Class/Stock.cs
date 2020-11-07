using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Class
{
    public class Stock
    {
        #region Propiedades
        public DateTime Fecha;
        public DateTime Hora;

        public string Nombre ;
        public string Apellido ;
        public string Empresa ;

        public int DNI ;

        public decimal cantidad ;
        public decimal precio ;

        public string producto;

        public decimal descuento;
        public decimal interes;

        public decimal Neto;
        public decimal IVA ;
        public decimal TOTAL ;

        

        public DataTable lista = new DataTable();
        public DataTable dato = new DataTable();
        #endregion

        #region Metodos
        public void listar()
        {
            lista.Columns.Add("Cantidad", typeof(decimal));
            lista.Columns.Add("Producto", typeof(String));
            lista.Columns.Add("Precio Unit.", typeof(decimal));
            dato.Columns.Add("Nombre",typeof(string));
            dato.Columns.Add("Apellido", typeof(string));
            dato.Columns.Add("DNI",typeof(int));
            dato.Columns.Add("Empresa", typeof(string));
            dato.Columns.Add("Descuento",typeof(decimal));
            dato.Columns.Add("Interes",typeof(decimal));
            dato.Columns.Add("Neto",typeof(decimal));
            dato.Columns.Add("Iva",typeof(decimal));
            dato.Columns.Add("TotalFinal",typeof(decimal));


        }

        public void Dato()
        {
            dato.Rows.Add();

            dato.TableName = "datos";
            dato.Rows[0]["Nombre"] = Nombre;
            dato.Rows[0]["Apellido"] = Apellido;
            dato.Rows[0]["DNI"] = DNI;
            dato.Rows[0]["Empresa"] = Empresa;
            
        }

        public void Resultado()
        {
            dato.Rows.Add();
            dato.Rows[0]["Descuento"] = descuento;
            dato.Rows[0]["Interes"] = interes;
            dato.Rows[0]["Neto"] = Neto;
            dato.Rows[0]["Iva"] = IVA;
            dato.Rows[0]["TotalFinal"] = TOTAL;
            

        }

        public void Cargar()
        {
            try
            {
                lista.TableName = "Stock";
                lista.Rows.Add();

                
                lista.Rows[lista.Rows.Count - 1]["Cantidad"] = cantidad;
                lista.Rows[lista.Rows.Count - 1]["Producto"] = producto;
                lista.Rows[lista.Rows.Count - 1]["Precio Unit."] = precio;
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            
            
        }       

        public void Guardar()
        {
            lista.WriteXml(@"listar.xml");
            dato.WriteXml(@"datos.xml");
        }

        public void CargarStock()
        {
            lista.ReadXml(@"listar.xml");
            dato.ReadXml(@"datos.xml");
        }

        public void Calculo()
        {
            IVA = Neto * 0.21m;

            TOTAL = Neto + IVA;
        }
        #endregion

        public Stock()
        {
            Fecha = DateTime.Now;
            Hora = DateTime.Now;
        }
    }
}
