using System;
using Class;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockSupermercado
{

    public partial class Supermercado : Form
    {
        
        #region Constructor
        public Supermercado()
        {

            InitializeComponent();
            
            Datos.listar();
            dg.DataSource = Datos.lista;
            dgdato.DataSource = Datos.dato;
        }
        #endregion

        #region Propiedades

        private decimal[] cant = new decimal[1];
        decimal[] precio = new decimal[1];
        int contador = 0;
        int contadorp = 0;

        decimal acumulado = 0m;
        decimal acumulado1 = 0m;
        decimal total = 0m;
        decimal descuento = 0m;
        decimal interes = 0m;

        Stock stock = new Stock();
        Stock Datos = new Stock();
         
        #endregion

        #region Eventos       
        private void btinicio_Click(object sender, EventArgs e)
        {
            NuevoRegistro();
            txtfecha.Text = stock.Fecha.ToString("dd/MM/yy");
            txthora.Text = stock.Hora.ToString("HH:mm");

            btcargarregis.Enabled = true;
        }

        private void btnuevoregis_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }

        private void btaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Datos.Apellido = txtapellido.Text;
                Datos.Nombre = txtnombre.Text;
                Datos.DNI = System.Convert.ToInt32(txtdni.Text);
                Datos.Empresa = txtempresa.Text;
    
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            

            if (txtapellido.Text.Trim() == "" || txtnombre.Text.Trim() == "" || txtdni.Text.Trim() == "" || txtempresa.Text.Trim() == "")
            {
                MessageBox.Show("Faltan Datos");
            }
            else if (txtdni.Text.Length < 8)
            {
                MessageBox.Show("DNI no valido");
                txtdni.Focus();
                txtdni.Text = "";
            }
            else if (txtdni.Text.Length > 8)
            {
                MessageBox.Show("DNI no valido");
                txtdni.Focus();
                txtdni.Text = "";
            }
            else
            {
                Datos.Dato();
                p2.Enabled = true;
                btcalcular.Enabled = false;
            }

        }

        private void btcargar_Click(object sender, EventArgs e)
        {
            try
            {
                
                Datos.cantidad = System.Convert.ToDecimal(txtcantidad.Text);
                Datos.producto = txtproducto.Text;
                Datos.precio = System.Convert.ToDecimal(txtpreciou.Text);

                if (Datos.cantidad > 0 && Datos.precio > 0)
                {
                    Datos.Cargar();

                    if (contador < cant.Length)
                    {
                        cant[contador] = Datos.cantidad;
                        acumulado = cant[contador];
                        contador = contador + 1;
                    }
                    else
                    {
                        Redimension();
                        cant[contador] = Datos.cantidad;
                        acumulado = cant[contador];
                        contador = contador + 1;
                    }

                    if (contadorp < precio.Length)
                    {
                        precio[contadorp] = Datos.precio;
                        acumulado1 = precio[contadorp];
                        contadorp = contadorp + 1;
                    }
                    else
                    {
                        Redimension1();
                        precio[contadorp] = Datos.precio;
                        acumulado1 = precio[contadorp];
                        contadorp = contadorp + 1;
                    }

                    acumulado = acumulado * acumulado1;
                    total = total + acumulado;

                    txtcantidad.Text = "";
                    txtproducto.Text = "";
                    txtpreciou.Text = "";

                    txtcantidad.Focus();

                    btcalcular.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Datos ingresados no validos");

                    txtcantidad.Text = "";
                    txtproducto.Text = "";
                    txtpreciou.Text = "";

                    txtcantidad.Focus();
                }
                

                
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }

        }

        private void btcalcular_Click(object sender, EventArgs e)
        {
            txttotalstock.Text = System.Convert.ToString(total);

            btcalcular.Enabled = false;
        }

        private void btok_Click(object sender, EventArgs e)
        {
            p3.Enabled = true;
            Datos.Neto = total;
        }

        private void bte_Click(object sender, EventArgs e)
        {
            txtdescuento.ReadOnly = false;
            btc.Enabled = false;
        }

        private void btc_Click(object sender, EventArgs e)
        {
            txtinteres.ReadOnly = false;
            bte.Enabled = false;
        }

        private void btfin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdescuento.ReadOnly == false)
                {
                    descuento = System.Convert.ToDecimal(txtdescuento.Text);
                    Datos.descuento = descuento;
                }

                if (txtinteres.ReadOnly == false)
                {
                    interes = System.Convert.ToDecimal(txtinteres.Text);
                    Datos.interes = interes;
                }
                Calculo();

                btfin.Enabled = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Ingresar monto");

            }
        }

        private void btguardar_Click(object sender, EventArgs e)
        {
            Datos.Guardar();
            
        }

        private void btcargarregis_Click(object sender, EventArgs e)
        {
            Datos.CargarStock();
            
            dgdato.DataSource = Datos.dato;
            dg.DataSource = Datos.lista;
        }

        #endregion

        #region Metodos
        private void NuevoRegistro()
        {         
            p1.Enabled = true;

            btinicio.Enabled = false;
            txtnombre.Focus();
        }
        private void LimpiarRegistro()
        {
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtdni.Text = "";
            txtempresa.Text = "";
            txtneto.Text = "";
            txtiva.Text = "";
            txttotalfinal.Text = "";
            txttotalstock.Text = "";
            txtdescuento.Text = "";
            txtinteres.Text = "";
            Datos.lista.Rows.Clear();
            Datos.dato.Rows.Clear();

            

            cant = new decimal[1];
            precio = new decimal[1];

            p2.Enabled = false;
            p3.Enabled = false;

            bte.Enabled = true;
            btc.Enabled = true;

            contador = 0;
            contadorp = 0;

            btfin.Enabled = true;

            txtnombre.Focus();

           

        }
        private void Redimension()
        {
            decimal[] redi = new decimal[cant.Length + 1];
            for (int i = 0; i < cant.Length; i++)
            {
                redi[i] = cant[i];
            }
            cant = redi;
        }
        private void Redimension1()
        {
            decimal[] redi = new decimal[precio.Length + 1];
            for (int i = 0; i < precio.Length; i++)
            {
                redi[i] = precio[i];
            }
            precio = redi;
        }
        private void Calculo()
        {
            txtneto.Text = System.Convert.ToString(total);

            Datos.Calculo();
            txtiva.Text = System.Convert.ToString(Datos.IVA);
            txttotalfinal.Text = System.Convert.ToString(Datos.TOTAL - descuento + interes);
            
            Datos.Resultado();
        }
        #endregion

        
    }
}
