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

namespace ITTools_DeviceManager_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum LockStatus { Locked, Unlocked }
        private BitmapImage IconLock;
        private BitmapImage IconUnlock;
        private readonly Dictionary<string, string> IconPath = new Dictionary<string, string>()
        {
            {"Locked","Icons/64_lock_white.png"},
            {"Unlocked","Icons/64_unlock_orange.png"}
        };
        private List<Device> devices = new List<Device>();


        public MainWindow()
        {
            InitializeComponent();

            IconLock = new BitmapImage(new Uri(IconPath["Locked"], UriKind.Relative));
            IconUnlock = new BitmapImage(new Uri(IconPath["Unlocked"], UriKind.Relative));

            populateDataGrid();
            lockDataGridByDefault();

        }

        // populate graphs, when "Graph" tab is selected
        private void OnGraphTabSelected(object sender, RoutedEventArgs e)
        {
            (int desktopsCount, int notebooksCount) = StatsGenerator.CountDeviceTypes(devices);

            lblNumberOfDesktops.Content = desktopsCount;
            lblNumberOfLaptops.Content = notebooksCount;

        }

        private void populateDataGrid()
        {

            using (var db = new DevicesDbContext())
            {
                foreach (Device d in db.Device)
                {
                    devices.Add(d);
                }
                dgMain.ItemsSource = devices;

                lblRowCount.Content = "Rows: " + db.Device.Count();
            }

        }

        private void lockDataGridByDefault()
        {
            dgMain.IsReadOnly = true;
            imgIcon_EditLockUnlock.Source = new BitmapImage(new Uri(IconPath["Locked"], UriKind.Relative));
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
    }
}
