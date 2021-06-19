using GestionaleTecnoimpianti.windows;
using GestionaleTecnoimpianti.windows.clienti;
using GestionaleTecnoimpianti.windows.elettricisti;
using GestionaleTecnoimpianti.windows.fornitori;
using GestionaleTecnoimpianti.windows.furgoni;
using GestionaleTecnoimpianti.windows.impiantiElettrici;
using GestionaleTecnoimpianti.windows.lavori;
using GestionaleTecnoimpianti.windows.materiali;
using GestionaleTecnoimpianti.windows.preventivi;
using GestionaleTecnoimpianti.windows.turniLavorativi;
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

        private void Inserisci_Furgone_Click(object sender, RoutedEventArgs e)
        {
            new FormFurgoni().ShowDialog();
        }

        private void Elenco_Furgone_Click(object sender, RoutedEventArgs e)
        {
            var furgoni = from f in DB.FURGONI
                          select new { f.Targa, f.Marca, f.Posti, f.KM };

            FurgoniDataGrid.ItemsSource = furgoni;
        }

        /// <summary>
        /// Launch the update FORM for (Furgoni)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Furgoni_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (FurgoniDataGrid.SelectedItem != null)
            {
                dynamic selectedFurgone = FurgoniDataGrid.SelectedItem;
                string selectedTarga = selectedFurgone.Targa;

                var data = from f in DB.FURGONI
                           where f.Targa == selectedTarga
                           select new { f.Targa };

                if (data.Any() && data.First().Targa != null)
                {
                    new UpdateFurgoni(selectedTarga).ShowDialog();
                }

            }
        }

        private void Nuovo_Materiale_Click(object sender, RoutedEventArgs e)
        {
            new FormMateriali().ShowDialog();
        }

        private void Elenco_Materiali_Click(object sender, RoutedEventArgs e)
        {
            var materiali = from m in DB.MATERIALI
                            select new { m.CodMateriale, m.Nome, m.Descrizione, m.Quantità, m.Prezzo, m.QuantitàVenduta, Nome_Fornitore = m.FORNITORI.Nome };

            MaterialiDataGrid.ItemsSource = materiali;
        }

        private void Materiale_Costosto_Click(object sender, RoutedEventArgs e)
        {
            var materiali = from m in DB.MATERIALI
                            select new { m.CodMateriale, m.Nome, m.Descrizione, m.Quantità, m.Prezzo, m.QuantitàVenduta, Nome_Fornitore = m.FORNITORI.Nome };

            int maxQuantita = materiali.Max(m => m.QuantitàVenduta);
            MaterialiDataGrid.ItemsSource = materiali.Where(m => m.QuantitàVenduta == maxQuantita);
        }

        /// <summary>
        /// Launch the update FORM for (Materiali)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Materiali_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (MaterialiDataGrid.SelectedItem != null)
            {
                dynamic selectedMateriale = MaterialiDataGrid.SelectedItem;
                int selectedCod = selectedMateriale.CodMateriale;

                var data = from m in DB.MATERIALI
                           where m.CodMateriale == selectedCod
                           select new { m.CodMateriale };

                if (data.Any())
                {
                    new UpdateMateriali(selectedCod).ShowDialog();
                }

            }
        }

        private void Nuovo_Fornitore_Click(object sender, RoutedEventArgs e)
        {
            new FormFornitori().ShowDialog();
        }

        private void Elenco_Fornitori_Click(object sender, RoutedEventArgs e)
        {
            var forntori = from f in DB.FORNITORI
                           select new { f.CodFornitore, f.Nome, f.Telefono };

            FornitoriDataGrid.ItemsSource = forntori;
        }

        private void Nuovo_Preventivo_Click(object sender, RoutedEventArgs e)
        {
            new FormPreventivi().ShowDialog();
        }

        private void Elenco_Preventivi_Click(object sender, RoutedEventArgs e)
        {
            var preventivi = from p in DB.PREVENTIVI
                             select new { p.CodPreventivo, p.CodCliente, p.Data };

            PreventivoDataGrid.ItemsSource = preventivi;
        }

        private void Ricerca_Preventivo_Click(object sender, RoutedEventArgs e)
        {
            var preventivi = from p in DB.PREVENTIVI
                             select new { p.CodPreventivo, p.CodCliente, p.Data };

            if (Codice_Cliente_Preventivo.Text != "")
            {
                preventivi = preventivi.Where(elem => elem.CodCliente == int.Parse(Codice_Cliente_Preventivo.Text));
            }

            if (Data_Preventivo.SelectedDate != null)
            {
                preventivi = preventivi.Where(elem => elem.Data == Data_Preventivo.SelectedDate);
            }

            PreventivoDataGrid.ItemsSource = preventivi;
        }

        /// <summary>
        /// Launch the information FORM for (Preventivi)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Preventivi_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (PreventivoDataGrid.SelectedItem != null)
            {
                // need to dereferencing the object 
                // otherwise we need to cast to (int,string,string...) exactly parameters
                dynamic selectedPreventivo = PreventivoDataGrid.SelectedItem;
                int selectedCodPreventivo = selectedPreventivo.CodPreventivo;

                var data = (from p in DB.PREVENTIVI
                            where p.CodPreventivo == selectedCodPreventivo
                            select p).First();

                if (data != null)
                {
                    new InfoPreventivi(data).ShowDialog();
                }
            }
        }

        private void Nuovo_ImpiantoElettrico_Click(object sender, RoutedEventArgs e)
        {
            new FormImpiantiElettrici().ShowDialog();
        }

        private void Elenco_ImpiantiElettrici_Click(object sender, RoutedEventArgs e)
        {
            var impiantiElettrici = from i in DB.IMPIANTI_ELETTRICI
                                    select new { i.CodImpianto, i.CodCliente, i.DataInizio, i.DataFine };

            ImpiantoElettricoDataGrid.ItemsSource = impiantiElettrici;
        }

        private void Ricerca_ImpiantiElettrico_Click(object sender, RoutedEventArgs e)
        {
            var impiantoElettrico = from i in DB.IMPIANTI_ELETTRICI
                                    select new { i.CodImpianto, i.CodCliente, i.DataInizio, i.DataFine };

            if (Codice_Cliente_Impianto.Text != "")
            {
                impiantoElettrico = impiantoElettrico.Where(elem => elem.CodCliente == int.Parse(Codice_Cliente_Impianto.Text));
            }

            if (DataInizio_Impianto.SelectedDate != null)
            {
                impiantoElettrico = impiantoElettrico.Where(elem => elem.DataInizio == DataInizio_Impianto.SelectedDate);
            }

            if (ImpiantoFinito_Checkbox.IsChecked.Value)
            {
                impiantoElettrico = impiantoElettrico.Where(elem => elem.DataFine != null);
            }

            ImpiantoElettricoDataGrid.ItemsSource = impiantoElettrico;
        }

        /// <summary>
        /// Launch the information FORM for (ImpiantoElettrico)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpiantoElettrico_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (ImpiantoElettricoDataGrid.SelectedItem != null)
            {
                dynamic selectedImpiantoElettrico = ImpiantoElettricoDataGrid.SelectedItem;
                int selectedCodImpianto = selectedImpiantoElettrico.CodImpianto;

                var data = (from i in DB.IMPIANTI_ELETTRICI
                            where i.CodImpianto == selectedCodImpianto
                            select i).First();

                if (data != null)
                {
                    new InfoImpianti(data).ShowDialog();
                }
            }
        }

        private void Nuovo_Lavoro_Click(object sender, RoutedEventArgs e)
        {
            new FormLavori().ShowDialog();
        }

        private void Elenco_Lavori_Click(object sender, RoutedEventArgs e)
        {
            var Lavori = from l in DB.LAVORI
                         orderby l.Data descending
                         select new { l.CodImpianto, l.Data, l.Costo, Tipologia = l.TIPOLOGIE.Nome };

            LavoriDataGrid.ItemsSource = Lavori;
        }

        /// <summary>
        /// Launch the information FORM for (Lavori)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lavori_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (LavoriDataGrid.SelectedItem != null)
            {
                dynamic selectedLavoro = LavoriDataGrid.SelectedItem;
                int SelectedCodImpianto = selectedLavoro.CodImpianto;
                DateTime SelectedData = selectedLavoro.Data;

                var data = (from l in DB.LAVORI
                            where l.CodImpianto == SelectedCodImpianto && l.Data == SelectedData
                            select l).First();

                if (data != null)
                {
                    new InfoLavori(data).ShowDialog();
                }
            }
        }

        private void Mostra_TipologiaLavori_Click(object sender, RoutedEventArgs e)
        {
            var Tipologie = from t in DB.TIPOLOGIE
                             select t;

            int maxTipologieCount = Tipologie.Max(t => t.NumeroLavori);

            var TopTipologia = (from t in Tipologie
                                where t.NumeroLavori == maxTipologieCount
                                select new { t.Nome, t.NumeroLavori, t.Descrizione }).First();

            MessageBox.Show("La tipologia di Lavoro più eseguita è "
                + TopTipologia.Nome + ", con "
                + TopTipologia.NumeroLavori + " Lavori");

        }

        private void Inserisci_Tipologia_Click(object sender, RoutedEventArgs e)
        {
            new FormTipologie().ShowDialog();
        }

        private void Inserisci_TurnoLavorativo_Click(object sender, RoutedEventArgs e)
        {
            new FormTurniLavorativi().ShowDialog();
        }

        private void Elenco_TurniLavorativi_Click(object sender, RoutedEventArgs e)
        {
            var TurniLavorativi = from t in DB.TURNI_LAVORATIVI
                                  orderby t.DataLavoro descending
                                  select new { t.CodImpianto, t.DataLavoro, t.OraInizio, t.OraFine, t.CodiceFiscale, 
                                      t.DataInizioElettricista, TargaMezzo = t.Targa, t.Nota };

            TurniLavorativiDataGrid.ItemsSource = TurniLavorativi;
        }

        /// <summary>
        /// Launch the information FORM for (TurniLavorativi)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TurniLavorativi_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (TurniLavorativiDataGrid.SelectedItem != null)
            {
                dynamic selectedTurnoLavorativo = TurniLavorativiDataGrid.SelectedItem;
                int selectedCodImpianto = selectedTurnoLavorativo.CodImpianto;
                DateTime selectedDataLavoro = selectedTurnoLavorativo.DataLavoro;
                string selectedCodiceFiscale = selectedTurnoLavorativo.CodiceFiscale;
                DateTime selectedDataInizio = selectedTurnoLavorativo.DataInizioElettricista;

                var data = (from t in DB.TURNI_LAVORATIVI
                            where t.LAVORI.CodImpianto == selectedCodImpianto
                            && t.LAVORI.Data == selectedDataLavoro
                            && t.CodiceFiscale == selectedCodiceFiscale
                            && t.DataInizioElettricista == selectedDataInizio
                            select t).First();

                if (data != null)
                {
                    new InfoTurniLavorativi(data).ShowDialog();
                }
            }
        }
    }
}
