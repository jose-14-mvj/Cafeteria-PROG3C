using System.Windows;

namespace Cafeteria
{
    public partial class wpfMenuAdmin : Window
    {
        private readonly string _usuario;

        public wpfMenuAdmin(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            lblBienvenida.Text = $"Bienvenido, {usuario}  [Administrador]";
        }

        private void btnGestionMenu_Click(object sender, RoutedEventArgs e)
            => new wpfGestionMenu().ShowDialog();

        private void btnInventario_Click(object sender, RoutedEventArgs e)
            => new wpfInventario().ShowDialog();

        private void btnProveedor_Click(object sender, RoutedEventArgs e)
            => new wpfProveedor().ShowDialog();

        private void btnReportes_Click(object sender, RoutedEventArgs e)
            => new wpfReportes().ShowDialog();

        private void btnEmpleados_Click(object sender, RoutedEventArgs e)
            => new wpfEmpleados().ShowDialog();

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
