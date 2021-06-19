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
using System.Windows.Shapes;

namespace GestionaleTecnoimpianti.windows.turniLavorativi
{
    /// <summary>
    /// Logica di interazione per FormTurniLavorativi.xaml
    /// </summary>
    public partial class FormTurniLavorativi : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();

        public FormTurniLavorativi()
        {
            InitializeComponent();

            var Elettricisti = from e in db.ELETTRICISTI_CON_RUOLI
                               where e.DataFine == null
                               select Tuple.Create(e.CodiceFiscale, e.DataInizio);

            var Impianti = from i in db.IMPIANTI_ELETTRICI
                           select i.CodImpianto;

            var Furgoni = from i in db.FURGONI
                          select i.Targa;

            CodElettricista.ItemsSource = Elettricisti;
            CodImpianto.ItemsSource = Impianti;
            Targa.ItemsSource = Furgoni;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (CodElettricista.SelectedIndex != -1 && CodImpianto.SelectedIndex != -1
                && DataLavoro.SelectedIndex != -1 && Targa.SelectedIndex != -1
                && OraInizio.Text != "" && OraInizio.Text != "")
            {
                Tuple<string, DateTime> selectedCodElettricista = (Tuple<string, DateTime>)CodElettricista.SelectedItem;

                TURNI_LAVORATIVI NewTurnoLavorativo = new TURNI_LAVORATIVI()
                {
                    CodiceFiscale = selectedCodElettricista.Item1,
                    DataInizioElettricista = selectedCodElettricista.Item2,
                    CodImpianto = (int)CodImpianto.SelectedItem,
                    DataLavoro = (DateTime)DataLavoro.SelectedItem,
                    Targa = (string)Targa.SelectedItem,
                    OraInizio = TimeSpan.Parse(OraInizio.Text),
                    OraFine = TimeSpan.Parse(OraFine.Text),
                    Nota = Nota.Text,
                };

                db.TURNI_LAVORATIVI.InsertOnSubmit(NewTurnoLavorativo);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Inserimento avvenuto con successo");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserire tutti i campi correttamente");
            }
        }

        private void CodImpianto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CodImpianto.SelectedIndex != -1)
            {
                var Date = from l in db.LAVORI
                           where l.CodImpianto == ((int)CodImpianto.SelectedItem)
                           select l.Data;

                DataLavoro.ItemsSource = Date;
            }
        }
    }
}
