using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfInventario : Window
    {
        private readonly string rutaInv = @"D:\cafeteria\INVENTARIO.txt";
        private List<string[]> _items = new List<string[]>();

        public wpfInventario() { InitializeComponent(); Cargar(); }

        private void Cargar()
        {
            _items.Clear();
            lstInventario.Items.Clear();
            if (!File.Exists(rutaInv)) return;
            foreach (var linea in File.ReadAllLines(rutaInv, Encoding.UTF8))
            {
                var p = linea.Split(';');
                if (p.Length < 5) continue;
                _items.Add(p);
                bool bajo = decimal.TryParse(p[2], out decimal s) &&
                            decimal.TryParse(p[3], out decimal m) && s <= m;
                string alerta = bajo ? "⚠️ BAJO" : "✅";
                lstInventario.Items.Add(
                    alerta + "  " + p[0].Trim() + "  [" + p[1].Trim() + "]" +
                    "  Stock:" + p[2].Trim() + "  Min:" + p[3].Trim() +
                    "  Bs." + p[4].Trim());
            }
        }

        private void Guardar()
        {
            var lineas = new List<string>();
            foreach (var p in _items)
                lineas.Add(string.Join(";", p));
            Directory.CreateDirectory(@"D:\cafeteria");
            File.WriteAllLines(rutaInv, lineas, Encoding.UTF8);
            Cargar();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProdNombre.Text) ||
                string.IsNullOrWhiteSpace(txtProdStock.Text))
            {
                MessageBox.Show("Nombre y stock son obligatorios.", "Aviso");
                return;
            }
            _items.Add(new string[]
            {
                txtProdNombre.Text.Trim(),
                txtProdCateg.Text.Trim(),
                txtProdStock.Text.Trim(),
                txtProdMinimo.Text.Trim(),
                txtProdCosto.Text.Trim()
            });
            Guardar();
            txtProdNombre.Clear(); txtProdCateg.Clear();
            txtProdStock.Clear(); txtProdMinimo.Clear(); txtProdCosto.Clear();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstInventario.SelectedIndex < 0) return;
            _items.RemoveAt(lstInventario.SelectedIndex);
            Guardar();
        }
    }
}