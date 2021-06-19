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

namespace GestionaleTecnoimpianti.windows.lavori
{
    /// <summary>
    /// Logica di interazione per InfoLavori.xaml
    /// </summary>
    public partial class InfoLavori : Window
    {
        private readonly ClassesTecnoimpiatiDBDataContext db = new ClassesTecnoimpiatiDBDataContext();

        public InfoLavori(LAVORI lavoro)
        {
            InitializeComponent();

            CodImpianto.Content = lavoro.CodImpianto.ToString();
            Data_Lavoro.Content = lavoro.Data.Date.ToString();
            NomeTipologia.Content = lavoro.TIPOLOGIE.Nome;
            Costo.Content = lavoro.Costo.ToString();

            var Materiali = from m in lavoro.DETTAGLI_MATERIALI
                            select new { m.CodMateriale, m.MATERIALI.Nome, m.Quantità, m.Prezzo, m.Sconto, m.Nota };

            MaterialiDataGrid.ItemsSource = Materiali;

            var TurniLavorativi = from t in lavoro.TURNI_LAVORATIVI
                            select new { t.CodiceFiscale, t.DataInizioElettricista, t.OraInizio, t.OraFine, t.Nota};

            TurniLavorativiDataGrid.ItemsSource = TurniLavorativi;
        }
    }
}
