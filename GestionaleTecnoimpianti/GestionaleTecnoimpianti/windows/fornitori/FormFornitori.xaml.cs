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

namespace GestionaleTecnoimpianti.windows.fornitori
{
    /// <summary>
    /// Logica di interazione per FormFornitori.xaml
    /// </summary>
    public partial class FormFornitori : Window
    {
        public FormFornitori()
        {
            InitializeComponent();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (Nome.Text != "" && Telefono.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();

                FORNITORI NewFornitore = new FORNITORI()
                {
                    CodFornitore = db.FORNITORI.Count() + 1,
                    Nome = Nome.Text,
                    Telefono = Telefono.Text
                };

                db.FORNITORI.InsertOnSubmit(NewFornitore);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Fornitore correttamente inserito");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione DATABASE: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserire tutti i valori");
            }
        }
    }
}
