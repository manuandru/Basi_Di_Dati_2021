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

namespace GestionaleTecnoimpianti.windows.materiali
{
    /// <summary>
    /// Logica di interazione per FormMateriali.xaml
    /// </summary>
    public partial class FormMateriali : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        public FormMateriali()
        {
            InitializeComponent();

            var fornitori = from f in db.FORNITORI
                            select new { f.CodFornitore, f.Nome };

            List<(int, string)> fornitoriList = new List<(int, string)>();
            foreach (var item in fornitori)
            {
                fornitoriList.Add((item.CodFornitore, item.Nome));
            }
            Fornitore.ItemsSource = fornitoriList;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Nome.Text != "" && Descrizione.Text != "" && Quantita.Text != "" 
                && Prezzo.Text != "" && Fornitore.SelectedIndex != -1)
            {
                MATERIALI NewMateriale = new MATERIALI()
                {
                    CodMateriale = db.MATERIALI.Count() + 1,
                    Nome = Nome.Text,
                    Descrizione = Descrizione.Text,
                    Quantità = int.Parse(Quantita.Text),
                    Prezzo = decimal.Parse(Prezzo.Text),
                    QuantitàVenduta = 0,
                    CodFornitore = ((ValueTuple<int, string>)Fornitore.SelectedItem).Item1
                };

                db.MATERIALI.InsertOnSubmit(NewMateriale);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Materiale inserito correttamente");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("VIOLAZIONE DATABASE: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserisci correttamente tutti i valori");
            }
        }
    }
}
