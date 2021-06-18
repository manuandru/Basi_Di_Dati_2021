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

namespace GestionaleTecnoimpianti.windows.preventivi
{
    /// <summary>
    /// Logica di interazione per InfoPreventivi.xaml
    /// </summary>
    public partial class InfoPreventivi : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();

        public InfoPreventivi(PREVENTIVI preventivo)
        {
            InitializeComponent();

            CodCliente.Content = preventivo.CodCliente;
            DataPreventivo.Content = preventivo.Data;
            IDElettricista.Content = (preventivo.CodiceFiscale, preventivo.DataInizioElettricista);

            var materiali = from m in db.MATERIALI_IN_PREVENTIVI
                            where m.CodPreventivo == preventivo.CodPreventivo
                            select new
                            {
                                m.CodMateriale,
                                m.MATERIALI.Nome,
                                m.MATERIALI.Descrizione,
                                m.Quantità,
                                m.MATERIALI.Prezzo,
                                m.Nota,
                                NomeFornitore = m.MATERIALI.FORNITORI.Nome
                            };

            MaterialiDataGrid.ItemsSource = materiali;

            Totale.Content = Math.Round(materiali.Sum(m => m.Quantità * m.Prezzo),2) + " €";
        }
    }
}
