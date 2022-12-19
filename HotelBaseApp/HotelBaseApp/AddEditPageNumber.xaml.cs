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
    /// Логика взаимодействия для AddEditPageNumber.xaml
    /// </summary>
    public partial class AddEditPageNumber : Page
    {
        private Номера _currentNumber = new Номера();
        public AddEditPageNumber(Номера selectedNumber)
        {
            InitializeComponent();
            if (selectedNumber != null)
                _currentNumber = selectedNumber;
            DataContext = _currentNumber;
            List<string> Номера = new List<string>();
            new HotelClientBaseEntities().Номера.ToList().ForEach(o => { Номера.Add(Convert.ToString(o.Код_Номера)); });
            equipmentUp.ItemsSource = Номера;
            equipmentUp.ItemsSource = HotelClientBaseEntities.GetContext().Номера.ToList();
            PriceUp.ItemsSource = Номера;
            PriceUp.ItemsSource = HotelClientBaseEntities.GetContext().Номера.ToList();

        }
        private void BtnSaveNum_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentNumber.Название_Номера))
                errors.AppendLine("Выберите Название");
            if (string.IsNullOrWhiteSpace(_currentNumber.Комплектация_Номера))
                errors.AppendLine("Укажите Комплектацию");
            if (string.IsNullOrWhiteSpace(_currentNumber.Номер))
                errors.AppendLine("Выберите Номер");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentNumber.Цена_Номера)))
                errors.AppendLine("Укажите Цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentNumber.Код_Номера == 0)
                HotelClientBaseEntities.GetContext().Номера.Add(_currentNumber);
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
