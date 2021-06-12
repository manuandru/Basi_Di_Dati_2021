using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionaleTecnoimpianti
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ClassesTecnoimpiatiDBDataContext GetDB()
            => new ClassesTecnoimpiatiDBDataContext();

        private void Elenco_Clienti_Click(object sender, RoutedEventArgs e)
        {
            var data = from c in GetDB().CLIENTI
                       select new
                       {
                           c.CodCliente,
                           c.CodiceFiscale,
                           c.PartitaIVA,
                           c.Nome,
                           c.Cognome,
                           c.Denominazione,
                           c.Telefono
                       };

            ClientiDataGrid.ItemsSource = data;
        }

        private void Elenco_Privati_Click(object sender, RoutedEventArgs e)
        {
            var data = from c in GetDB().CLIENTI
                       where c.CodiceFiscale != null
                       select new
                       {
                           c.CodCliente,
                           c.CodiceFiscale,
                           c.Nome,
                           c.Cognome,
                           c.Telefono
                       };

            ClientiDataGrid.ItemsSource = data;
        }

        private void Elenco_Aziende_Click(object sender, RoutedEventArgs e)
        {
            var data = from c in GetDB().CLIENTI
                       where c.PartitaIVA != null
                       select new
                       {
                           c.CodCliente,
                           c.PartitaIVA,
                           c.Denominazione,
                           c.Telefono
                       };

            ClientiDataGrid.ItemsSource = data;
        }
    }
}
