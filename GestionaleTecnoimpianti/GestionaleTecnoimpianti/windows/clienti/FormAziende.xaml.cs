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
    /// Logica di interazione per FormAziende.xaml
    /// </summary>
    public partial class FormAziende : Window
    {
        public FormAziende()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (P_IVA.Text != "" && Denominazione.Text != "" && Telefono.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();
                var data = from c in db.CLIENTI
                           where c.PartitaIVA == P_IVA.Text
                           select c.PartitaIVA;

                if (data.Count() > 0)
                {
                    MessageBox.Show("Partita IVA già registrata");
                    return;
                }

                var data2 = from c in db.CLIENTI
                            select c.CodCliente;
                int codNewCliente = data2.Max() + 1;


                CLIENTI newCliente = new CLIENTI()
                {
                    CodCliente = codNewCliente,
                    PartitaIVA = P_IVA.Text,
                    Denominazione = Denominazione.Text,
                    Telefono = Telefono.Text
                };

                db.CLIENTI.InsertOnSubmit(newCliente);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Nuova Azienda inserita");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Non tutti i campi sono stati completati", "Errore");
            }
        }
    }
}
