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
    /// Logica di interazione per UpdateElettricista.xaml
    /// </summary>
    public partial class UpdateElettricista : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly ELETTRICISTI_CON_RUOLI ElettricistaToUpdate;

        public UpdateElettricista(string CF)
        {
            InitializeComponent();
            var data = from e in db.ELETTRICISTI_CON_RUOLI
                       where e.CodiceFiscale == CF && e.DataFine == null
                       select e;

            ElettricistaToUpdate = data.First();
            CF_Elettricista.Content = ElettricistaToUpdate.CodiceFiscale;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            if (Date.SelectedDate != null)
            {
                ElettricistaToUpdate.DataFine = Date.SelectedDate;

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Data fine correttamente aggiunta");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione DATABASE: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Aggiungere una nuova data");
            }
        }
    }
}
