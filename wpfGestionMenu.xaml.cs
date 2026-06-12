using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfGestionMenu : Window
    {
        private readonly string rutaMenu = @"D:\cafeteria\MENU.txt";
        private List<string[]> _platos = new List<string[]>();

        public wpfGestionMenu() { InitializeComponent(); Cargar(); }

        private void Cargar()
        {
            _platos.Clear();
            lstPlatos.Items.Clear();
            if (!File.Exists(rutaMenu)) return;
            foreach (var linea in File.ReadAllLines(rutaMenu, Encoding.UTF8))
            {
                var p = linea.Split(';');
                if (p.Length < 3) continue;
                _platos.Add(p);
                string estado = p[2].Trim() == "true" ? "✅" : "❌";
                string desc = p.Length > 3 ? p[3].Trim() : "";
                lstPlatos.Items.Add(estado + "  " + p[0].Trim() +
                                    "  —  Bs. " + p[1].Trim() + "  |  " + desc);
            }
        }

        private void Guardar()
        {
            var lineas = new List<string>();
            foreach (var p in _platos)
                lineas.Add(string.Join(";", p));
            Directory.CreateDirectory(@"D:\cafeteria");
            File.WriteAllLines(rutaMenu, lineas, Encoding.UTF8);
            Cargar();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string precio = txtPrecio.Text.Trim();
            string desc = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(precio))
            {
                MessageBox.Show("Nombre y precio son obligatorios.", "Aviso");
                return;
            }
            if (!decimal.TryParse(precio, out _))
            {
                MessageBox.Show("Precio inválido.", "Aviso");
                return;
            }
            _platos.Add(new string[] { nombre, precio, "true", desc });
            Guardar();
            txtNombre.Clear(); txtPrecio.Clear(); txtDescripcion.Clear();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstPlatos.SelectedIndex < 0) return;
            _platos.RemoveAt(lstPlatos.SelectedIndex);
            Guardar();
        }

        private void btnActivar_Click(object sender, RoutedEventArgs e)
        {
            int idx = lstPlatos.SelectedIndex;
            if (idx < 0) return;
            _platos[idx][2] = _platos[idx][2].Trim() == "true" ? "false" : "true";
            Guardar();
        }
    }
}