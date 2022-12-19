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
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        public HotelPage()
        {
            InitializeComponent();
            DGridHotels.ItemsSource = HotelClientBaseEntities.GetContext().Отель.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddHot_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageHotel(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HotelClientBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = HotelClientBaseEntities.GetContext().Отель.ToList();
            }
        }

        private void BtnDelHot_Click(object sender, RoutedEventArgs e)
        {
            var HotelsForRemoving = DGridHotels.SelectedItems.Cast<Отель>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {HotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HotelClientBaseEntities.GetContext().Отель.RemoveRange(HotelsForRemoving);
                    HotelClientBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridHotels.ItemsSource = HotelClientBaseEntities.GetContext().Отель.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEditHot_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageHotel((sender as Button).DataContext as Отель));
        }
    }
}

