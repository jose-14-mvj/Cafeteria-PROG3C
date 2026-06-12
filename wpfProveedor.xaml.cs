using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfProveedor : Window
    {
        private readonly string rutaProv   = @"D:\cafeteria\PROVEEDORES.txt";
        private readonly string rutaOrdenes = @"D:\cafeteria\ORDENES.txt";

        public wpfProveedor() { InitializeComponent(); CargarProveedores(); CargarOrdenes(); }

        private void CargarProveedores()
        {
            lstProveedores.Items.Clear();
            if (!File.Exists(rutaProv)) return;
            foreach (var l in File.ReadAllLines(rutaProv, Encoding.UTF8))
                lstProveedores.Items.Add(l);
        }

        private void CargarOrdenes()
        {
            lstOrdenes.Items.Clear();
            if (!File.Exists(rutaOrdenes)) return;
            foreach (var l in File.ReadAllLines(rutaOrdenes, Encoding.UTF8))
                lstOrdenes.Items.Add(l);
        }

        private void btnGuardarProv_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProvNombre.Text)) return;
            Directory.CreateDirectory(@"D:\cafeteria");
            File.AppendAllText(rutaProv,
                $"{txtProvNombre.Text.Trim()};{txtProvTelefono.Text.Trim()}\n",
                Encoding.UTF8);
            txtProvNombre.Clear(); txtProvTelefono.Clear();
            CargarProveedores();
        }

        private void btnNuevaOrden_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrdProducto.Text) ||
                string.IsNullOrWhiteSpace(txtOrdCantidad.Text)) return;
            Directory.CreateDirectory(@"D:\cafeteria");
            string id = Guid.NewGuid().ToString("N").Substring(0,6).ToUpper();
            File.AppendAllText(rutaOrdenes,
                $"{id};{txtOrdProducto.Text.Trim()};{txtOrdCantidad.Text.Trim()};" +
                $"{txtOrdCosto.Text.Trim()};Pendiente;{DateTime.Now:dd/MM/yyyy}\n",
                Encoding.UTF8);
            txtOrdProducto.Clear(); txtOrdCantidad.Clear(); txtOrdCosto.Clear();
            CargarOrdenes();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
