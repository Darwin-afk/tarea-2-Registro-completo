using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Registro.Entidades;
using Registro.BLL;

namespace Registro.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPersona.xaml
    /// </summary>
    public partial class rPersona : Window
    {
        public rPersona()
        {
            InitializeComponent();
        }

        private void nuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            Persona persona;
            bool paso = false;

            if (!validar())
                return;

            persona = llenaClase();

            if (Convert.ToInt32(tbID.Text) == 0)
                paso = PersonasBLL.Guardar(persona);
            else
            {
                if (existeEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                paso = PersonasBLL.Modificar(persona);
            }

            if (paso)
            {
                limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void buscar_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Persona persona = new Persona();
            int.TryParse(tbID.Text, out id);

            limpiar();

            persona = PersonasBLL.Buscar(id);

            if(persona!=null)
            {
                MessageBox.Show("Persona Encontrada");
                llenaCampo(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(tbID.Text, out id);

            limpiar();

            if (PersonasBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se puede eliminar una persona que no existe");
        }

        private bool existeEnLaBaseDeDatos()
        {

            Persona persona = PersonasBLL.Buscar(Convert.ToInt32(tbID.Text));
            return (persona != null);
        }

        private void limpiar()
        {
            tbID.Text = string.Empty;
            tbNombre.Text = string.Empty;
            tbTelefono.Text = string.Empty;
            tbCedula.Text = string.Empty;
            tbDireccion.Text = string.Empty;
            dpFecha.SelectedDate = DateTime.Now;
        }

        private void llenaCampo(Persona persona)
        {
            tbID.Text = Convert.ToString(persona.PersonaId);
            tbNombre.Text = persona.Nombre;
            tbTelefono.Text = persona.Telefono;
            tbCedula.Text = persona.Cedula;
            tbDireccion.Text = persona.Direccion;
            dpFecha.SelectedDate = persona.FechaNacimiento;
        }

        private Persona llenaClase()
        {
            Persona persona = new Persona();
            persona.PersonaId = Convert.ToInt32(tbID.Text);
            persona.Nombre = tbNombre.Text;
            persona.Telefono = tbTelefono.Text;
            persona.Cedula = tbCedula.Text;
            persona.Direccion = tbDireccion.Text;
            persona.FechaNacimiento = (DateTime)dpFecha.SelectedDate;

            return persona;
        }

        private bool validar()
        {
            bool paso = true;

            //ID
            if (string.IsNullOrWhiteSpace(tbID.Text))
                paso = false;
            else
            {
                for (int i = 0; i < tbID.Text.Length; i++)
                {
                    if (!Char.IsDigit(tbID.Text[i]) || Convert.ToInt32(tbID.Text[i]) < 0)
                        paso = false;
                }
            }

            //Nombre
            if (tbNombre.Text == string.Empty)
                paso = false;

            //Telefono
            if (string.IsNullOrWhiteSpace(tbTelefono.Text))
                paso = false;
            else
            {
                for (int i = 0; i < tbTelefono.Text.Length; i++)
                {
                    if (!Char.IsDigit(tbTelefono.Text[i]) || Convert.ToInt32(tbID.Text[i]) < 0)
                        paso = false;
                }
            }
            
            //Direccion
            if (string.IsNullOrWhiteSpace(tbDireccion.Text))
                paso = false;

            //Cedula
            if (string.IsNullOrWhiteSpace(tbCedula.Text.Replace("-", "")))
                paso = false;
            else
            {
                for (int i = 0; i < tbCedula.Text.Length; i++)
                {
                    if (!Char.IsDigit(tbCedula.Text[i]))
                        paso = false;
                }
            }
            //Fecha
            if (dpFecha.SelectedDate > DateTime.Now)
                paso = false;

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }
    }
}
