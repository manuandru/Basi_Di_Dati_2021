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

namespace GestionaleTecnoimpianti.windows.elettricisti
{
    /// <summary>
    /// Logica di interazione per FormAssegnaRuoli.xaml
    /// </summary>
    public partial class FormAssegnaRuoli : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext DB = new ClassesTecnoimpiatiDBDataContext();

        public FormAssegnaRuoli()
        {
            InitializeComponent();
            SetUpElettricisti();
            SetUpRuoli();
        }

        private void SetUpElettricisti()
        {
            var ElettricistiNoRuolo = from e in DB.ELETTRICISTI
                                      where e.ELETTRICISTI_CON_RUOLI.Count() == 0 || e.ELETTRICISTI_CON_RUOLI.All(er => er.DataFine != null)
                                      select e.CodiceFiscale;
            CF.ItemsSource = ElettricistiNoRuolo;
        }

        private void SetUpRuoli()
        {
            var Ruoli = from r in DB.RUOLI
                        select new { r.CodRuolo, r.Descrizione };

            List<(int, string)> RuoliList = new List<(int, string)>();
            foreach (var r in Ruoli)
            {
                RuoliList.Add((r.CodRuolo, r.Descrizione));
            }
            Ruolo.ItemsSource = RuoliList;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Assegna_Click(object sender, RoutedEventArgs e)
        {
            if (Ruolo.SelectedIndex != -1 && CF.SelectedIndex != -1 && Date.SelectedDate != null)
            {
                ELETTRICISTI_CON_RUOLI newElettricista = new ELETTRICISTI_CON_RUOLI()
                {
                    CodiceFiscale = CF.Text,
                    CodRuolo = ((ValueTuple<int, string>)Ruolo.SelectedItem).Item1,
                    DataInizio = Date.SelectedDate.Value
                };

                DB.ELETTRICISTI_CON_RUOLI.InsertOnSubmit(newElettricista);

                try
                {
                    DB.SubmitChanges();
                    MessageBox.Show("Ruolo assegnato correttamente a Elettricista");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Non tutti i campi sono stati completati");
            }
        }

        private void Aggiungi_Ruolo(object sender, RoutedEventArgs e)
        {
            if (Descrizione.Text != "")
            {
                int CodNewRuolo = DB.RUOLI.Count() + 1;

                RUOLI NewRuolo = new RUOLI()
                {
                    CodRuolo = CodNewRuolo,
                    Descrizione = Descrizione.Text
                };

                DB.RUOLI.InsertOnSubmit(NewRuolo);

                try
                {
                    DB.SubmitChanges();
                    SetUpRuoli();
                    MessageBox.Show("Nuovo Ruolo aggiunto");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex.Message);
                }

            }
        }
    }
}
