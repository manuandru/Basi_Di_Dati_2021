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

namespace GestionaleTecnoimpianti.windows.impiantiElettrici
{
    /// <summary>
    /// Logica di interazione per FormImpiantiElettrici.xaml
    /// </summary>
    public partial class FormImpiantiElettrici : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();

        public FormImpiantiElettrici()
        {
            InitializeComponent();

            var codClienti = from c in db.CLIENTI
                             select new { c.CodCliente };

            Codice_Cliente.ItemsSource = codClienti;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Codice_Cliente.SelectedIndex != -1 && Data.SelectedDate != null
                && Regione.Text != "" && Citta.Text != ""
                && Via.Text != "" && Numero.Text != "")
            {
                dynamic Cliente = Codice_Cliente.SelectedItem;

                IMPIANTI_ELETTRICI NewImpianto = new IMPIANTI_ELETTRICI()
                {
                    CodImpianto = db.IMPIANTI_ELETTRICI.Count() + 1,
                    CodCliente = Cliente.CodCliente,
                    DataInizio = Data.SelectedDate.Value,
                    Regione = Regione.Text,
                    Città = Citta.Text,
                    Via = Via.Text,
                    Numero = int.Parse(Numero.Text),
                    Note = Nota.Text != "" ? Nota.Text : null
                };

                db.IMPIANTI_ELETTRICI.InsertOnSubmit(NewImpianto);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Valori inseriti correttamente");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Inserisci correttamente tutti i campi");
            }
        }
    }
}
