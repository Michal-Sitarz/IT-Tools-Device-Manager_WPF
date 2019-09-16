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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ITTools_DeviceManager_WPF.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace ITTools_DeviceManager_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DevicesDbContext db = new DevicesDbContext();
        private enum LockStatus { Locked, Unlocked }
        private BitmapImage IconLock;
        private BitmapImage IconUnlock;
        private readonly Dictionary<string, string> IconPath = new Dictionary<string, string>()
        {
            {"Locked","Icons/64_lock_white.png"},
            {"Unlocked","Icons/64_unlock_orange.png"}
        };

        //public SeriesCollection SeriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        private Stats stats;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            IconLock = new BitmapImage(new Uri(IconPath["Locked"], UriKind.Relative));
            IconUnlock = new BitmapImage(new Uri(IconPath["Unlocked"], UriKind.Relative));

            populateDataGrid();
            lockDataGridByDefault();

        }

        // populate graphs, when "Graphs" tab is selected
        private void OnGraphTabSelected(object sender, RoutedEventArgs e)
        {
            stats = StatsGenerator.CountDeviceTypes(dgMain.Items.OfType<Device>().ToList());

            lblNumberOfDesktops.Content = stats.NumberOfDesktops;
            lblNumberOfLaptops.Content = stats.NumberOfNotebooks;

            pieChart_DevicesTypes.Series[0].Values[0] = (double) stats.NumberOfDesktops;
            pieChart_DevicesTypes.Series[1].Values[0] = (double) stats.NumberOfNotebooks;
            
            PointLabel = chartPoint =>
                            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            
        }



        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        // Data Grid
        private void populateDataGrid()
        {

            try
            {
                dgMain.ItemsSource = db.Device.ToList();
                lblRowCount.Content = "Rows: " + db.Device.Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encountered when communicating with the DATABASE." +
                                "\nPlease check the database file path." +
                                "\n\n============================\n\nDetailed error message:\n\n"
                                + ex.ToString());
            }

        }

        private void lockDataGridByDefault()
        {
            dgMain.IsReadOnly = true;
            imgIcon_EditLockUnlock.Source = IconLock;
            lblLockStatus.Content = LockStatus.Locked;
        }

        private void gridIcon_EditLockUnlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dgMain.IsReadOnly)
            {
                // if locked, then unlock it
                dgMain.IsReadOnly = false;
                imgIcon_EditLockUnlock.Source = IconUnlock;
                lblLockStatus.Content = LockStatus.Unlocked;
            }
            else
            {
                // if unlocked, then lock it
                dgMain.IsReadOnly = true;
                imgIcon_EditLockUnlock.Source = IconLock;
                lblLockStatus.Content = LockStatus.Locked;
            }
        }

        private void dgMain_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            //dgMain.CommitEdit();
            try
            {
                //dgMain.CommitEdit(DataGridEditingUnit.Cell,true);
                if (e.EditAction == DataGridEditAction.Commit)
                {

                    // to save changes the row has to lose the focus, that's why there are 3 "jumps" of focus around the window
                    // by default DataGrid can update the model when the whole row loses the focus, not a single cell - this is not what was required here
                    lblGridChangeConfirmation.Focusable = true;
                    lblGridChangeConfirmation.Focus();

                    db.SaveChanges();

                    lblGridChangeConfirmation.Content = "Changed";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("UPDATING db encountered an error..." + ex.ToString());
            }


        }

        private void dgMain_GotFocus(object sender, RoutedEventArgs e)
        {
            lblGridChangeConfirmation.Content = "";
        }
    }
}
