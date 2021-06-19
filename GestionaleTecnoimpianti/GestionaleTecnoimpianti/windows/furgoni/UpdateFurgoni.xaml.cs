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
    /// Logica di interazione per UpdateFurgoni.xaml
    /// </summary>
    public partial class UpdateFurgoni : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly FURGONI FurgoneToUpdate;
        public UpdateFurgoni(string Targa)
        {
            InitializeComponent();

            var Furgoni = from f in db.FURGONI
                          where f.Targa == Targa
                          select f;

            FurgoneToUpdate = Furgoni.First();
            TargaFurgone.Content = FurgoneToUpdate.Targa;
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Aggiungi_KM_Click(object sender, RoutedEventArgs e)
        {
            FurgoneToUpdate.KM += int.Parse(KM.Text);

            try
            {
                db.SubmitChanges();
                MessageBox.Show("KM aggiunti correttamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Violazione DATABASE: " + ex.Message);
            }
        }
    }
}
