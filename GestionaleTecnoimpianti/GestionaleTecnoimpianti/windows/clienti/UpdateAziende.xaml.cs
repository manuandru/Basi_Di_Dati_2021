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

namespace GestionaleTecnoimpianti.windows.clienti
{
    /// <summary>
    /// Logica di interazione per UpdateAziende.xaml
    /// </summary>
    public partial class UpdateAziende : Window
    {
        private int CodToModify { get; }
        public UpdateAziende(int CodToModify)
        {
            InitializeComponent();
            this.CodToModify = CodToModify;
        }
        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            if (Denominazione.Text != "" && Telefono.Text != "")
            {
                var db = new ClassesTecnoimpiatiDBDataContext();

                var data = from c in db.CLIENTI
                           where c.CodCliente == CodToModify
                           select c;

                CLIENTI ClienteToModify = data.First();

                ClienteToModify.Denominazione = Denominazione.Text;
                ClienteToModify.Telefono = Telefono.Text;

                db.SubmitChanges();

                MessageBox.Show("Valori aggiornati correttamente");
                Close();
            }
            else
            {
                MessageBox.Show("Non tutti i campi sono stati riempiti");
            }
        }
    }
}
