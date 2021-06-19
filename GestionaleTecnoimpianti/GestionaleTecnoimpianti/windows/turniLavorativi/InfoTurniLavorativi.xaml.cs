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

namespace GestionaleTecnoimpianti.windows.turniLavorativi
{
    /// <summary>
    /// Logica di interazione per InfoTurniLavorativi.xaml
    /// </summary>
    public partial class InfoTurniLavorativi : Window
    {
        public InfoTurniLavorativi(TURNI_LAVORATIVI turno)
        {
            InitializeComponent();

            CodElettricista.Content = turno.CodiceFiscale;
            CodImpianto.Content = turno.CodImpianto;
            DataLavoro.Content = turno.DataLavoro;
            Targa.Content = turno.Targa;
            OraInizio.Content = turno.OraInizio;
            OraFine.Content = turno.OraFine;
            OreLavorate.Content = turno.OraFine - turno.OraInizio;
            Nota.Text = turno.Nota;
        }
    }
}
