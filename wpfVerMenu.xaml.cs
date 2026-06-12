using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfVerMenu : Window
    {
        private readonly string rutaMenu = @"D:\cafeteria\MENU.txt";

        public wpfVerMenu()
        {
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            if (!File.Exists(rutaMenu)) return;
            foreach (var linea in File.ReadAllLines(rutaMenu, Encoding.UTF8))
            {
                var p = linea.Split(';');
                if (p.Length < 3 || p[2].Trim() != "true") continue;
                string desc = p.Length > 3 ? $"  —  {p[3].Trim()}" : "";
                lstMenu.Items.Add($"✅  {p[0].Trim()}   Bs. {p[1].Trim()}{desc}");
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
