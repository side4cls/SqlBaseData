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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            DGridClients.ItemsSource = HotelClientBaseEntities.GetContext().Клиенты.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPage((sender as Button).DataContext as Клиенты));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HotelClientBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridClients.ItemsSource = HotelClientBaseEntities.GetContext().Клиенты.ToList();
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var ClientsForRemoving = DGridClients.SelectedItems.Cast<Клиенты>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {ClientsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HotelClientBaseEntities.GetContext().Клиенты.RemoveRange(ClientsForRemoving);
                    HotelClientBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridClients.ItemsSource = HotelClientBaseEntities.GetContext().Клиенты.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}


