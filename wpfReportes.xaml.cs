using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class wpfReportes : Window
    {
        private readonly string rutaVentas = @"D:\cafeteria\VENTAS.txt";
        private readonly string _filtroEmpleado;

        // Constructor para admin (ver todo)
        public wpfReportes() : this(null) { }

        // Constructor para empleado (solo sus ventas)
        public wpfReportes(string empleado)
        {
            InitializeComponent();
            _filtroEmpleado = empleado;
            Cargar();
        }

        private void Cargar()
        {
            if (!File.Exists(rutaVentas)) return;

            decimal totalAcum = 0;
            int countVentas = 0;

            foreach (var linea in File.ReadAllLines(rutaVentas, Encoding.UTF8))
            {
                var p = linea.Split(';');
                if (p.Length < 8) continue;
                // formato: ID;Plato;Cant;Precio;Total;IVA;Empleado;Fecha
                if (_filtroEmpleado != null && p[6].Trim() != _filtroEmpleado) continue;

                lstVentas.Items.Add(
                    $"[{p[7].Trim()}]  ID:{p[0].Trim()}  {p[2].Trim()}x {p[1].Trim()}" +
                    $"  Bs.{p[3].Trim()}  Total:Bs.{p[4].Trim()}  Emp:{p[6].Trim()}");

                if (decimal.TryParse(p[4].Trim(), out decimal t)) totalAcum += t;
                countVentas++;
            }

            lblTotalVentas.Text =
                $"Total ventas: {countVentas}   |   Recaudado: Bs. {totalAcum:F2}";
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
