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

namespace GestionaleTecnoimpianti.windows.materiali
{
    /// <summary>
    /// Logica di interazione per UpdateMateriali.xaml
    /// </summary>
    public partial class UpdateMateriali : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly MATERIALI MaterialeToUpdate;

        public UpdateMateriali(int CodMateriale)
        {
            InitializeComponent();

            var Materiali = from m in db.MATERIALI
                            where m.CodMateriale == CodMateriale
                            select m;

            MaterialeToUpdate = Materiali.First();
            Nome.Content = MaterialeToUpdate.Nome;
            Quantita.Text = MaterialeToUpdate.Quantità.ToString();
        }

        private void Annulla_Click(object sender, RoutedEventArgs e) => Close();

        private void Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            MaterialeToUpdate.Quantità = int.Parse(Quantita.Text);

            try
            {
                db.SubmitChanges();
                MessageBox.Show("Quantità aggiornata correttamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Violazione DATABASE: " + ex);
            }
        }
    }
}
