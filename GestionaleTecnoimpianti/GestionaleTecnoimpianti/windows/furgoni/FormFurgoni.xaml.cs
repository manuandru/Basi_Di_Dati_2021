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

namespace GestionaleTecnoimpianti.windows.furgoni
{
    /// <summary>
    /// Logica di interazione per FormFurgoni.xaml
    /// </summary>
    public partial class FormFurgoni : Window
    {
        public FormFurgoni()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Targa.Text != "" && Marca.Text != "" && Posti.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();

                if (db.FURGONI.Any(f => f.Targa == Targa.Text))
                {
                    MessageBox.Show("Errore: Targa già presente");
                    return;
                }

                FURGONI NewFurgone = new FURGONI()
                {
                    Targa = Targa.Text,
                    Marca = Marca.Text,
                    Posti = byte.Parse(Posti.Text),
                    KM = 0
                };

                db.FURGONI.InsertOnSubmit(NewFurgone);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Furgone correttamente inserito");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione DATABASE: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Inserire tutti i valori");
            }
        }

    }
}
