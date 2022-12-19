using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddEditPageHotel.xaml
    /// </summary>
    public partial class AddEditPageHotel : Page
    {
        private Отель _currentHotel = new Отель();
        public AddEditPageHotel(Отель selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentHotel = selectedHotel;
            DataContext = _currentHotel;
            List<string> Клиенты = new List<string>();
            new HotelClientBaseEntities().Отель.ToList().ForEach(o => { Клиенты.Add(o.Название_отеля); });

        }
        private void BtnSaveHot_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Адрес_отеля))
                errors.AppendLine("Выберите адрес");
            if (string.IsNullOrWhiteSpace(_currentHotel.Название_отеля))
                errors.AppendLine("Укажите отель");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentHotel.Код_Отеля == 0)
                HotelClientBaseEntities.GetContext().Отель.Add(_currentHotel);
            try
            {
                HotelClientBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.Main.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
