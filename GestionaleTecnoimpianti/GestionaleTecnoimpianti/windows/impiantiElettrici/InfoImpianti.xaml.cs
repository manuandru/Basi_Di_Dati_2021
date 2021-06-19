using GestionaleTecnoimpianti.windows.lavori;
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

namespace GestionaleTecnoimpianti.windows.impiantiElettrici
{
    /// <summary>
    /// Logica di interazione per InfoImpianti.xaml
    /// </summary>
    public partial class InfoImpianti : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();
        private readonly IMPIANTI_ELETTRICI Impianto;

        public InfoImpianti(IMPIANTI_ELETTRICI impianto)
        {
            InitializeComponent();

            this.Impianto = impianto;

            CodCliente.Content = impianto.CodCliente;
            CodImpianto.Content = impianto.CodImpianto;
            DataInizio.Content = impianto.DataInizio;
            Regione.Content = impianto.Regione;
            Citta.Content = impianto.Città;
            Via.Content = impianto.Via;
            Numero.Content = impianto.Numero;
            Note.Text = impianto.Note;

            if (impianto.DataFine != null)
            {
                DataFine.SelectedDate = impianto.DataFine;
                DataFine.IsEnabled = false;
                Aggiungi_DataFine_Button.IsEnabled = false;
            }

            var Lavori = from l in impianto.LAVORI
                         select new
                         {
                             l.Data,
                             l.Costo,
                             Tipologia = l.TIPOLOGIE.Nome
                         };

            LavoriImpiantoDataGrid.ItemsSource = Lavori;

            CostoTotale.Content = Lavori.Sum(l => l.Costo);
        }

        private void Aggiungi_DataFine_Click(object sender, RoutedEventArgs e)
        {
            if (DataFine.SelectedDate != null)
            {
                var Impianto = (from i in db.IMPIANTI_ELETTRICI
                               where i.CodImpianto == this.Impianto.CodImpianto
                               select i).First();

                Impianto.DataFine = DataFine.SelectedDate.Value;

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Data di fine impianto impostata correttamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Violazione Database: " + ex);
                }
            }
        }

        private void Lavori_DataGrid_Double_Click(object sender, MouseButtonEventArgs e)
        {
            if (LavoriImpiantoDataGrid.SelectedItem != null)
            {
                dynamic selectedLavoro = LavoriImpiantoDataGrid.SelectedItem;
                DateTime selectedData = selectedLavoro.Data;

                var data = (from l in db.LAVORI
                            where l.CodImpianto == Impianto.CodImpianto && l.Data == selectedData
                            select l).First();

                if (data != null)
                {
                    new InfoLavori(data).ShowDialog();
                }
            }
        }
    }
}
