using System.IO;
using System.Text;
using System.Windows;

namespace Cafeteria
{
    public partial class MainWindow : Window
    {
        private readonly string rutaUsuarios = @"D:\cafeteria\USUARIOS.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string correo   = txtCorreo.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Completa todos los campos.";
                return;
            }

            // Verificar credenciales en archivo
            if (!File.Exists(rutaUsuarios))
            {
                // Crear admin por defecto si no existe el archivo
                Directory.CreateDirectory(@"D:\cafeteria");
                File.WriteAllText(rutaUsuarios,
                    "admin,adminADM,Administrador\n",
                    Encoding.UTF8);
            }

            var lineas = File.ReadAllLines(rutaUsuarios, Encoding.UTF8);
            foreach (var linea in lineas)
            {
                var partes = linea.Split(',');
                if (partes.Length < 3) continue;

                if (partes[0].Trim() == correo && partes[1].Trim() == password)
                {
                    string rol = partes[2].Trim();
                    if (rol == "Administrador")
                    {
                        new wpfMenuAdmin(correo).Show();
                    }
                    else
                    {
                        new wpfMenuEmpleado(correo).Show();
                    }
                    this.Close();
                    return;
                }
            }

            lblMensaje.Text = "Usuario o contraseña incorrectos.";
        }
    }
}
