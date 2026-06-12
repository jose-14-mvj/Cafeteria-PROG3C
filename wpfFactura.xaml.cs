using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfFactura : Window
    {
        private readonly string rutaVentas = @"D:\cafeteria\VENTAS.txt";
        private readonly List<ItemPedido> _pedido;
        private readonly decimal _total;
        private readonly string _empleado;
        private readonly string _idVenta;

        public wpfFactura(List<ItemPedido> pedido, decimal total, string empleado)
        {
            InitializeComponent();
            _pedido = pedido;
            _total = total;
            _empleado = empleado;
            _idVenta = GenerarId();
            MostrarFactura();
        }

        private string GenerarId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        private void MostrarFactura()
        {
            lblNumero.Text = "ID: " + _idVenta;
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            foreach (var item in _pedido)
            {
                decimal sub = item.Precio * item.Cantidad;
                lstDetalle.Items.Add(item.Cantidad + "x  " + item.Nombre +
                                     "  Bs. " + sub.ToString("F2"));
            }

            decimal impuesto = Math.Round(_total * 0.13m, 2);
            decimal subtotal = _total - impuesto;

            lblSubtotal.Text = "Subtotal:  Bs. " + subtotal.ToString("F2");
            lblImpuesto.Text = "IVA (13%): Bs. " + impuesto.ToString("F2");
            lblTotalFinal.Text = "TOTAL:     Bs. " + _total.ToString("F2");
            lblEmpleado.Text = "Atendido por: " + _empleado;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(@"D:\cafeteria");
            decimal impuesto = Math.Round(_total * 0.13m, 2);

            var sb = new StringBuilder();
            foreach (var item in _pedido)
            {
                sb.AppendLine(
                    _idVenta + ";" + item.Nombre + ";" + item.Cantidad + ";" +
                    item.Precio.ToString("F2") + ";" + _total.ToString("F2") + ";" +
                    impuesto.ToString("F2") + ";" + _empleado + ";" +
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }
            File.AppendAllText(rutaVentas, sb.ToString(), Encoding.UTF8);

            MessageBox.Show(
                "Venta guardada correctamente.\nID: " + _idVenta,
                "Boleta guardada",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            this.Close();
        }
    }
}