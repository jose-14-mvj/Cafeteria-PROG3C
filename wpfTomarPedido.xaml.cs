using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Cafeteria
{
    // Clase auxiliar en vez de tupla (compatible con C# 7.3)
    public class ItemPedido
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }

    public class PlatoMenu
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

    public partial class wpfTomarPedido : Window
    {
        private readonly string _empleado;
        private readonly string rutaMenu = @"D:\cafeteria\MENU.txt";
        private readonly List<ItemPedido> _pedido = new List<ItemPedido>();
        private List<PlatoMenu> _platos = new List<PlatoMenu>();

        public wpfTomarPedido(string empleado)
        {
            InitializeComponent();
            _empleado = empleado;
            CargarPlatos();
        }

        private void CargarPlatos()
        {
            _platos.Clear();
            lstPlatos.Items.Clear();
            if (!File.Exists(rutaMenu)) return;

            foreach (var linea in File.ReadAllLines(rutaMenu, Encoding.UTF8))
            {
                var p = linea.Split(';');
                if (p.Length < 3) continue;
                if (p[2].Trim() == "true")
                {
                    _platos.Add(new PlatoMenu
                    {
                        Nombre = p[0].Trim(),
                        Precio = decimal.Parse(p[1].Trim())
                    });
                    lstPlatos.Items.Add(p[0].Trim() + "  —  Bs. " + p[1].Trim());
                }
            }
        }

        private void lstPlatos_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (lstPlatos.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un plato.", "Aviso");
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("Cantidad inválida.", "Aviso");
                return;
            }
            var plato = _platos[lstPlatos.SelectedIndex];
            _pedido.Add(new ItemPedido
            {
                Nombre = plato.Nombre,
                Precio = plato.Precio,
                Cantidad = cant
            });
            ActualizarListaPedido();
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (lstPedido.SelectedIndex >= 0)
            {
                _pedido.RemoveAt(lstPedido.SelectedIndex);
                ActualizarListaPedido();
            }
        }

        private void ActualizarListaPedido()
        {
            lstPedido.Items.Clear();
            decimal total = 0;
            foreach (var item in _pedido)
            {
                decimal sub = item.Precio * item.Cantidad;
                lstPedido.Items.Add(item.Cantidad + "x " + item.Nombre +
                                    "  Bs. " + sub.ToString("F2"));
                total += sub;
            }
            lblTotal.Text = "Total: Bs. " + total.ToString("F2");
        }

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            if (_pedido.Count == 0)
            {
                MessageBox.Show("El pedido está vacío.", "Aviso");
                return;
            }
            decimal total = 0;
            foreach (var item in _pedido)
                total += item.Precio * item.Cantidad;

            var wndFactura = new wpfFactura(_pedido, total, _empleado);
            wndFactura.ShowDialog();
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}