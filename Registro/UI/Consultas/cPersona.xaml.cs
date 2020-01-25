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
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Registro.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cPersona.xaml
    /// </summary>
    public partial class cPersona : Window
    {
        public cPersona()
        {
            InitializeComponent();
        }

        private void bConsultar_Click(object sender, RoutedEventArgs e)
        {
            

            var listado = new List<Persona>();

            if(tbCriterio.Text.Trim().Length > 0)
            {
                switch(cbFiltrar.SelectedIndex)
                {
                    case 0:
                        listado = PersonasBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(tbCriterio.Text);
                        listado = PersonasBLL.GetList(p => p.PersonaId == id);
                        break;
                    case 2:
                        listado = PersonasBLL.GetList(p => p.Nombre.Contains(tbCriterio.Text));
                        break;
                    case 3:
                        listado = PersonasBLL.GetList(p => p.Cedula.Contains(tbCriterio.Text));
                        break;
                    case 4:
                        listado = PersonasBLL.GetList(p => p.Direccion.Contains(tbCriterio.Text));
                        break;
                }
                if(dpDesde.SelectedDate!=null && dpHasta.SelectedDate!=null)
                    listado = listado.Where(c => c.FechaNacimiento.Date >= dpDesde.SelectedDate.Value.Date && c.FechaNacimiento.Date <= dpHasta.SelectedDate.Value.Date).ToList();
            }
            else
            {
                listado = PersonasBLL.GetList(p => true);
            }

            dgConsulta.ItemsSource = listado;
        }
    }
}
