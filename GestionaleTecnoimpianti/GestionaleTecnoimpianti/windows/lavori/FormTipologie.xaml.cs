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
    /// Logica di interazione per FormTipologie.xaml
    /// </summary>
    public partial class FormTipologie : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();

        public FormTipologie()
        {
            InitializeComponent();
            var Tipologie = from t in db.TIPOLOGIE
                            select new { t.CodTipologia, t.Nome, t.Descrizione, t.NumeroLavori };

            TipologieDataGrid.ItemsSource = Tipologie;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Nome.Text != "" && Descrizione.Text != null)
            {
                TIPOLOGIE NewTipologia = new TIPOLOGIE()
                {
                    CodTipologia = db.TIPOLOGIE.Count() + 1,
                    Nome = Nome.Text,
                    Descrizione = Descrizione.Text,
                    NumeroLavori = 0
                };

                db.TIPOLOGIE.InsertOnSubmit(NewTipologia);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Inserimento riuscito");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Completa correttamente i campi");
            }
        }
    }
}
