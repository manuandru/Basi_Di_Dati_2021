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
    /// Logica di interazione per FormElettricisti.xaml
    /// </summary>
    public partial class FormElettricisti : Window
    {
        public FormElettricisti()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (CF.Text != "" && Nome.Text != "" && Cognome.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();
                if (db.ELETTRICISTI.Any(el => el.CodiceFiscale == CF.Text))
                {
                    MessageBox.Show("Codice Fiscale già presente");
                }
                else
                {
                    ELETTRICISTI newElettricista = new ELETTRICISTI()
                    {
                        CodiceFiscale = CF.Text,
                        Nome = Nome.Text,
                        Cognome = Cognome.Text
                    };

                    db.ELETTRICISTI.InsertOnSubmit(newElettricista);

                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Nuovo Elettricista inserito correttamente");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Violazione Database: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Non tutti i valori sono stati inseriti");
            }
        }
    }
}
