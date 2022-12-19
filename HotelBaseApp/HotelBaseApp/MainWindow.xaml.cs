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

namespace HotelBaseApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Navigate(new Page());
            Manager.Main = Main;
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Manager.Main.GoBack();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Main_ContentRendered(object sender, EventArgs e)
        {
            if(Main.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void BtnClientСlick(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new ClientPage());
        }

        private void btnHotelClick(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new HotelPage());

        }

        private void btnNumBtnClick(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new NumberPage());

        }

        private void btnRentBtnClick(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new RentPage());
        }
    }
}
