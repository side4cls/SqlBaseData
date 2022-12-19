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
    /// Логика взаимодействия для NumberPage.xaml
    /// </summary>
    public partial class NumberPage : Page
    {
        public NumberPage()
        {
            InitializeComponent();
            DGridNumber.ItemsSource = HotelClientBaseEntities.GetContext().Номера.ToList();
        }

        private void BtnAddNum_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageNumber(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HotelClientBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridNumber.ItemsSource = HotelClientBaseEntities.GetContext().Номера.ToList();
            }
        }

        private void BtnDelNum_Click(object sender, RoutedEventArgs e)
        {
            var NumbersForRemoving = DGridNumber.SelectedItems.Cast<Номера>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {NumbersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HotelClientBaseEntities.GetContext().Номера.RemoveRange(NumbersForRemoving);
                    HotelClientBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridNumber.ItemsSource = HotelClientBaseEntities.GetContext().Номера.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEditNum_Click(object sender, RoutedEventArgs e)
        {
            Manager.Main.Navigate(new AddEditPageNumber((sender as Button).DataContext as Номера));
        }
    }
}
