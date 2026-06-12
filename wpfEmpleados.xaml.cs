using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Cafeteria
{
    public partial class wpfEmpleados : Window
    {
        private readonly string rutaUsuarios = @"D:\cafeteria\USUARIOS.txt";
        private List<string> _lineas = new List<string>();

        public wpfEmpleados() { InitializeComponent(); Cargar(); }

        private void Cargar()
        {
            _lineas.Clear();
            lstEmpleados.Items.Clear();
            if (!File.Exists(rutaUsuarios)) return;
            foreach (var l in File.ReadAllLines(rutaUsuarios, Encoding.UTF8))
            {
                _lineas.Add(l);
                var p = l.Split(',');
                if (p.Length >= 3)
                    lstEmpleados.Items.Add("[" + p[2].Trim() + "]  " + p[0].Trim());
            }
        }

        private void lstEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = lstEmpleados.SelectedIndex;
            if (idx < 0) return;
            var p = _lineas[idx].Split(',');
            txtEmpNombre.Text = p.Length > 0 ? p[0].Trim() : "";
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtEmpNombre.Text.Trim();
            string pass = txtEmpPass.Password.Trim();
            string rol = ((ComboBoxItem)cmbRol.SelectedItem)?.Content?.ToString() ?? "Empleado";

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Usuario y contraseña son obligatorios.", "Aviso");
                return;
            }
            Directory.CreateDirectory(@"D:\cafeteria");
            File.AppendAllText(rutaUsuarios,
                usuario + "," + pass + "," + rol + "\n", Encoding.UTF8);
            Cargar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int idx = lstEmpleados.SelectedIndex;
            if (idx < 0) return;
            _lineas.RemoveAt(idx);
            Directory.CreateDirectory(@"D:\cafeteria");
            File.WriteAllLines(rutaUsuarios, _lineas, Encoding.UTF8);
            Cargar();
        }
    }
}