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

namespace GestionaleTecnoimpianti.windows
{
    /// <summary>
    /// Logica di interazione per FormPrivati.xaml
    /// </summary>
    public partial class FormPrivati : Window
    {
        public FormPrivati()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();
        
        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Nome.Text != "" && Cognome.Text != ""
                && CF.Text != "" && Telefono.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();
                var data = from c in db.CLIENTI
                           where c.CodiceFiscale == CF.Text
                           select c.CodCliente;

                if (data.Count() > 0)
                {
                    MessageBox.Show("Codice Fiscale già registrato");
                    return;
                }

                var data2 = from c in db.CLIENTI
                           select c.CodCliente;
                int codNewCliente = data2.Max() + 1;

                
                CLIENTI newCliente = new CLIENTI()
                {
                    CodCliente = codNewCliente,
                    Nome = Nome.Text,
                    Cognome = Cognome.Text,
                    CodiceFiscale = CF.Text,
                    Telefono = Telefono.Text
                };

                db.CLIENTI.InsertOnSubmit(newCliente);
                db.SubmitChanges();
                MessageBox.Show("Nuovo Cliente Privato registrato");
                Close();
            }
            else
            {
                MessageBox.Show("Non tutti i campi sono stati completati", "Errore");
            }
        }
    }
}
