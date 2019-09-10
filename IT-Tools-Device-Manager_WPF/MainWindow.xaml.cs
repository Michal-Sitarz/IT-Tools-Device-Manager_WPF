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
        public MainWindow()
        {
            InitializeComponent();
            populateDataGrid();
            lockDataGridByDefault();

        }

        private void populateDataGrid()
        {

            using (var db = new DevicesDbContext())
            {
                var devices = new List<Device>();
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
            imgIcon_EditLockUnlock.Source = new BitmapImage(new Uri("Icons/64_lock.png", UriKind.Relative));
        }

        private void btn_SwitchEditing_Click(object sender, RoutedEventArgs e)
        {
            if (dgMain.IsReadOnly)
            {
                // if locked, then unlock it
                dgMain.IsReadOnly = false;
                imgIcon_EditLockUnlock.Source = new BitmapImage(new Uri("Icons/64_unlock.png", UriKind.Relative));
            }
            else
            {
                // if unlocked, then lock it
                dgMain.IsReadOnly = true;
                imgIcon_EditLockUnlock.Source = new BitmapImage(new Uri("Icons/64_lock.png", UriKind.Relative));
            }
        }
    }
}
