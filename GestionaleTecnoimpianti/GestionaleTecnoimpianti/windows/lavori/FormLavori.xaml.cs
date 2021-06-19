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

namespace GestionaleTecnoimpianti.windows.lavori
{
    /// <summary>
    /// Logica di interazione per FormLavori.xaml
    /// </summary>
    public partial class FormLavori : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly List<DETTAGLI_MATERIALI> Parziale_Materiali = new List<DETTAGLI_MATERIALI>();

        public FormLavori()
        {
            InitializeComponent();

            var Impianti = (from i in db.IMPIANTI_ELETTRICI
                            where i.DataFine == null
                            select new { i.CodImpianto }).Select(c => c.CodImpianto);

            var Tipologie = (from t in db.TIPOLOGIE
                             select new { t.CodTipologia, t.Nome }).Select(t => Tuple.Create(t.CodTipologia, t.Nome));

            var Materiali = (from m in db.MATERIALI
                             select new { m.CodMateriale, m.Nome}).Select(m => Tuple.Create(m.CodMateriale, m.Nome));

            CodImpianto.ItemsSource = Impianti;
            CodTipologia.ItemsSource = Tipologie;
            Codice_Materiale.ItemsSource = Materiali;
        }

        private void Aggiungi_Materiale(object sender, RoutedEventArgs e)
        {
            if (Codice_Materiale.SelectedIndex != -1 && Quantita_Materiale.Text != "" && Prezzo_Materiale.Text != "")
            {
                if (Parziale_Materiali.Any(m => m.CodMateriale == ((Tuple<int, string>)Codice_Materiale.SelectedItem).Item1))
                {
                    MessageBox.Show("Codice Materiale già presente");
                    Codice_Materiale.SelectedIndex = -1;
                    Quantita_Materiale.Text = "";
                    Prezzo_Materiale.Text = "";
                    Sconto_Materiale.Text = "";
                    Nota_Materiale.Text = "";
                    return;
                }

                DETTAGLI_MATERIALI newMateriale = new DETTAGLI_MATERIALI()
                {
                    CodMateriale = ((Tuple<int, string>)Codice_Materiale.SelectedItem).Item1,
                    Quantità = int.Parse(Quantita_Materiale.Text),
                    Prezzo = decimal.Parse(Prezzo_Materiale.Text),
                    Sconto = float.Parse(Sconto_Materiale.Text),
                    Nota = Nota_Materiale.Text != "" ? Nota_Materiale.Text : null
                };

                ParzialeDataGrid.ItemsSource = null;
                Parziale_Materiali.Add(newMateriale);
                ParzialeDataGrid.ItemsSource = Parziale_Materiali;

                Codice_Materiale.SelectedIndex = -1;
                Quantita_Materiale.Text = "";
                Prezzo_Materiale.Text = "";
                Sconto_Materiale.Text = "";
                Nota_Materiale.Text = "";
            }
            else
            {
                MessageBox.Show("Completa tutti i campi");
            }
        }

        private void Conferma(object sender, RoutedEventArgs e)
        {
            if (CodImpianto.SelectedIndex != -1 && Data_Lavoro.SelectedDate != null
                && CodTipologia.SelectedIndex != -1 && Parziale_Materiali.Count != 0)
            {
                decimal CostoTotale = Parziale_Materiali.Sum(m => m.Prezzo * m.Quantità);

                int selectedCodImpianto = int.Parse(CodImpianto.Text);
                DateTime selectedDate = Data_Lavoro.SelectedDate.Value;
                int selectedCodTipologia = ((Tuple<int, string>)CodTipologia.SelectedItem).Item1;

                LAVORI NewLavoro = new LAVORI()
                {
                    Data = selectedDate,
                    CodImpianto = selectedCodImpianto,
                    CodTipologia = selectedCodTipologia,
                    Costo = CostoTotale
                };

                db.LAVORI.InsertOnSubmit(NewLavoro);

                foreach (var item in Parziale_Materiali)
                {
                    item.CodImpianto = selectedCodImpianto;
                    item.DataLavoro = selectedDate;
                }

                db.DETTAGLI_MATERIALI.InsertAllOnSubmit(Parziale_Materiali);

                var tipologia = (from t in db.TIPOLOGIE
                                 where t.CodTipologia == selectedCodTipologia
                                 select t).First();

                tipologia.NumeroLavori++;

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Lavoro correttamente inserito");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("VIOLAZIONE DATABASE: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Inserisci i campi correttamente");
            }
        }

        private void Codice_Materiale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Codice_Materiale.SelectedIndex != -1)
            {
                var materiale = (from m in db.MATERIALI
                                 where m.CodMateriale == ((Tuple<int, string>)Codice_Materiale.SelectedItem).Item1
                                 select m).First();

                Prezzo_Materiale.Text = materiale.Prezzo.ToString();
            }
        }
    }
}
