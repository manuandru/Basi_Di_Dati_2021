using GestionaleTecnoimpianti.windows;
using GestionaleTecnoimpianti.windows.clienti;
using GestionaleTecnoimpianti.windows.elettricisti;
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
        private ClassesTecnoimpiatiDBDataContext DB { get; }
        public MainWindow()
        {
            InitializeComponent();
            DB = GetDB();
        }

        private ClassesTecnoimpiatiDBDataContext GetDB()
            => new ClassesTecnoimpiatiDBDataContext();

        private void Elenco_Clienti_Click(object sender, RoutedEventArgs e)
        {
            var data = from c in DB.CLIENTI
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
            var data = from c in DB.CLIENTI
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
            var data = from c in DB.CLIENTI
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

        private void Ricerca_Cliente_Click(object sender, RoutedEventArgs e)
        {
            var data = from c in DB.CLIENTI
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

            if (Nome_Cliente.Text != "")
            {
                data = data.Where(elem => elem.Nome == Nome_Cliente.Text);
            }

            if (Cognome_Cliente.Text != "")
            {
                data = data.Where(elem => elem.Cognome == Cognome_Cliente.Text);
            }

            if (Telefono_Cliente.Text != "")
            {
                data = data.Where(elem => elem.Telefono == Telefono_Cliente.Text);
            }

            if (Denominazione_Cliente.Text != "")
            {
                data = data.Where(elem => elem.Denominazione == Denominazione_Cliente.Text);
            }

            ClientiDataGrid.ItemsSource = data;
        }

        private void Inserisci_Privato_Click(object sender, RoutedEventArgs e)
        {
            new FormPrivati().ShowDialog();
        }

        private void Inserisci_Azienda_Click(object sender, RoutedEventArgs e)
        {
            new FormAziende().ShowDialog();
        }

        /// <summary>
        /// Launch FORM to update the selected (CLIENTE)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clienti_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (ClientiDataGrid.SelectedItem != null)
            {
                // need to dereferencing the object 
                // otherwise we need to cast to (int,string,string...) exactly parameters
                dynamic selectedCliente = ClientiDataGrid.SelectedItem;
                int selectedCod = selectedCliente.CodCliente;

                var data = from c in DB.CLIENTI
                           where c.CodCliente == selectedCod
                           select new { c.CodCliente, c.CodiceFiscale };

                
                if (data.First().CodiceFiscale != null)
                {
                    new UpdatePrivati(selectedCod).ShowDialog();
                }
                else
                {
                    new UpdateAziende(selectedCod).ShowDialog();
                }
                
            }
        }

        private void Nuovo_Elettricista_Click(object sender, RoutedEventArgs e)
        {
            new FormElettricisti().ShowDialog();
        }

        private void Assegna_Ruolo_Click(object sender, RoutedEventArgs e)
        {
            new FormAssegnaRuoli().ShowDialog();
        }

        private void Elenco_Elettricisti_Click(object sender, RoutedEventArgs e)
        {
            var data = from el in DB.ELETTRICISTI
                       select new
                       {
                           el.CodiceFiscale,
                           el.Nome,
                           el.Cognome
                       };

            ElettricistiDataGrid.ItemsSource = data;
        }

        private void Elenco_Elettricisti_Con_Ruolo_Click(object sender, RoutedEventArgs e)
        {
            var data = from el in DB.ELETTRICISTI_CON_RUOLI
                       where el.DataFine == null
                       select new
                       {
                           el.CodiceFiscale,
                           el.ELETTRICISTI.Nome,
                           el.ELETTRICISTI.Cognome,
                           el.DataInizio,
                           el.RUOLI.Descrizione
                       };

            ElettricistiDataGrid.ItemsSource = data;
        }

        private void Elenco_Elettricisti_Senza_Ruolo_Click(object sender, RoutedEventArgs e)
        {
            var data = from el in DB.ELETTRICISTI_CON_RUOLI
                       where el.DataFine != null
                       select new
                       {
                           el.CodiceFiscale,
                           el.ELETTRICISTI.Nome,
                           el.ELETTRICISTI.Cognome,
                           el.DataInizio,
                           el.DataFine,
                           el.RUOLI.Descrizione
                       };

            ElettricistiDataGrid.ItemsSource = data;
        }

        private void Ricerca_Elettricista_Click(object sender, RoutedEventArgs e)
        {
            var data = from el in DB.ELETTRICISTI_CON_RUOLI
                       select new
                       {
                           el.CodiceFiscale,
                           el.ELETTRICISTI.Nome,
                           el.ELETTRICISTI.Cognome,
                           el.DataInizio,
                           el.DataFine,
                           el.RUOLI.Descrizione
                       };

            if (Nome_Elettricista.Text != "")
            {
                data = data.Where(elem => elem.Nome == Nome_Elettricista.Text);
            }

            if (Cognome_Elettricista.Text != "")
            {
                data = data.Where(elem => elem.Cognome == Cognome_Elettricista.Text);
            }

            if (Codice_Fiscale_Elettricista.Text != "")
            {
                data = data.Where(elem => elem.CodiceFiscale == Codice_Fiscale_Elettricista.Text);
            }

            if (Ruolo_Elettricista.Text != "")
            {
                data = data.Where(elem => elem.Descrizione == Ruolo_Elettricista.Text);
            }

            ElettricistiDataGrid.ItemsSource = data;
        }

        /// <summary>
        /// Launch the update FORM for (Elettricisti)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elettricisti_DataGrid_Double_Click(object sender, RoutedEventArgs e)
        {
            if (ElettricistiDataGrid.SelectedItem != null)
            {
                // need to dereferencing the object 
                // otherwise we need to cast to (int,string,string...) exactly parameters
                dynamic selectedElettricista = ElettricistiDataGrid.SelectedItem;
                string selectedCF = selectedElettricista.CodiceFiscale;

                var data = from c in DB.ELETTRICISTI_CON_RUOLI
                           where c.CodiceFiscale == selectedCF && c.DataFine == null
                           select new { c.CodiceFiscale, c.DataFine };

                if (data.Any() && data.First().CodiceFiscale != null)
                {
                    new UpdateElettricista(selectedCF).ShowDialog();
                }

            }
        }

        private void Ruolo_Elettricista_Click(object sender, MouseButtonEventArgs e)
        {
            var data = from r in DB.RUOLI
                       select r.Descrizione;

            Ruolo_Elettricista.SelectedItem = null;

            Ruolo_Elettricista.ItemsSource = data;
        }


    }
}
