using System.Windows;

namespace Cafeteria
{
    public partial class wpfMenuEmpleado : Window
    {
        private readonly string _usuario;

        public wpfMenuEmpleado(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            lblBienvenida.Text = $"Bienvenid@, {usuario}  [Empleado]";
        }

        private void btnTomar_Click(object sender, RoutedEventArgs e)
            => new wpfTomarPedido(_usuario).ShowDialog();

        private void btnMenu_Click(object sender, RoutedEventArgs e)
            => new wpfVerMenu().ShowDialog();

        private void btnMisVentas_Click(object sender, RoutedEventArgs e)
            => new wpfReportes(_usuario).ShowDialog();

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
