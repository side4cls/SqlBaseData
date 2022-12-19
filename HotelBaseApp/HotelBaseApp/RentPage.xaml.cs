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
    /// Логика взаимодействия для RentPage.xaml
    /// </summary>
    public partial class RentPage : Page
    {
        public RentPage()
        {
            InitializeComponent();
            DGridRent.ItemsSource = HotelClientBaseEntities.GetContext().Аренда.ToList();
        }

        private void ButtonRent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditRent_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageRent((sender as Button).DataContext as Аренда));
        }

        private void BtnAddRent_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageRent(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HotelClientBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridRent.ItemsSource = HotelClientBaseEntities.GetContext().Аренда.ToList();
            }
        }

        private void BtnDelRent_Click(object sender, RoutedEventArgs e)
        {
            var RentForRemoving = DGridRent.SelectedItems.Cast<Аренда>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RentForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HotelClientBaseEntities.GetContext().Аренда.RemoveRange(RentForRemoving);
                    HotelClientBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridRent.ItemsSource = HotelClientBaseEntities.GetContext().Аренда.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
