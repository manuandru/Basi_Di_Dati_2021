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

namespace GestionaleTecnoimpianti.windows.preventivi
{
    /// <summary>
    /// Logica di interazione per FormPreventivi.xaml
    /// </summary>
    public partial class FormPreventivi : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly List<MATERIALI_IN_PREVENTIVI> Parziale_Materiali = new List<MATERIALI_IN_PREVENTIVI>();
        private readonly int codicePreventivo;

        public FormPreventivi()
        {
            InitializeComponent();

            var Clienti = (from c in db.CLIENTI
                          select new { c.CodCliente }).Select(c => c.CodCliente);

            var Elettricisti = (from e in db.ELETTRICISTI_CON_RUOLI
                               where e.DataFine == null
                               select new { e.CodiceFiscale, e.DataInizio }).Select(e => Tuple.Create(e.CodiceFiscale, e.DataInizio));

            var Materiali = (from m in db.MATERIALI
                            select new { m.CodMateriale }).Select(m => m.CodMateriale);

            CodCliente.ItemsSource = Clienti;
            ID_Elettricista.ItemsSource = Elettricisti;
            Codice_Materiale.ItemsSource = Materiali;
            codicePreventivo = db.PREVENTIVI.Count() + 1;
        }

        private void Aggiungi_Materiale(object sender, RoutedEventArgs e)
        {
            if (Codice_Materiale.SelectedIndex != -1 && Quantita_Materiale.Text != "")
            {
                if (Parziale_Materiali.Any(m => m.CodMateriale == int.Parse(Codice_Materiale.Text)))
                {
                    MessageBox.Show("Codice Materiale già presente");
                    Codice_Materiale.SelectedIndex = -1;
                    Quantita_Materiale.Text = "";
                    Nota_Materiale.Text = "";
                    return;
                }

                MATERIALI_IN_PREVENTIVI newMateriale = new MATERIALI_IN_PREVENTIVI()
                {
                    CodMateriale = (int)Codice_Materiale.SelectedItem,
                    CodPreventivo = codicePreventivo,
                    Quantità = int.Parse(Quantita_Materiale.Text),
                    Nota = Nota_Materiale.Text != "" ? Nota_Materiale.Text : null
                };

                ParzialeDataGrid.ItemsSource = null;
                Parziale_Materiali.Add(newMateriale);
                ParzialeDataGrid.ItemsSource = Parziale_Materiali;

                Codice_Materiale.SelectedIndex = -1;
                Quantita_Materiale.Text = "";
                Nota_Materiale.Text = "";
            }
            else
            {
                MessageBox.Show("Completa tutti i campi");
            }
        }

        private void Conferma(object sender, RoutedEventArgs e)
        {
            if (CodCliente.SelectedIndex != -1 && Data_Preventivo.SelectedDate != null
                && ID_Elettricista.SelectedIndex != -1 && Parziale_Materiali.Count != 0)
            {
                PREVENTIVI NewPreventivo = new PREVENTIVI()
                {
                    CodPreventivo = codicePreventivo,
                    CodCliente = int.Parse(CodCliente.Text),
                    Data = Data_Preventivo.SelectedDate.Value,
                    CodiceFiscale = ((Tuple<string, DateTime>)ID_Elettricista.SelectedItem).Item1,
                    DataInizioElettricista = ((Tuple<string, DateTime>)ID_Elettricista.SelectedItem).Item2
                };

                db.PREVENTIVI.InsertOnSubmit(NewPreventivo);

                db.MATERIALI_IN_PREVENTIVI.InsertAllOnSubmit(Parziale_Materiali);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Preventivo creato correttamente");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("VIOLAZIONE DATABASE: " + ex.Message);
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Inserisci i campi correttamente");
            }
        }
    }
}
