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
    /// Логика взаимодействия для AddEditPageRent.xaml
    /// </summary>
    public partial class AddEditPageRent : Page
    {
        private Аренда _currentRent = new Аренда();
        public AddEditPageRent(Аренда selectedRent)
        {
            InitializeComponent();
            if (selectedRent != null)
                _currentRent = selectedRent;
            DataContext = _currentRent;
            List<string> Аренда = new List<string>();
            new HotelClientBaseEntities().Аренда.ToList().ForEach(o => { Аренда.Add(Convert.ToString(o.Код_Аренды)); });

        }

        private void BtnSaveRent_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRent.Код_Аренды)))
                errors.AppendLine("Укажите код аренды");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRent.Код_Клиента)))
                errors.AppendLine("Укажите код клиента");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRent.Код_Номера)))
                errors.AppendLine("Укажите код номера");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRent.Код_Отеля)))
                errors.AppendLine("Укажите код отеля");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentRent.Код_Аренды == 0)
                HotelClientBaseEntities.GetContext().Аренда.Add(_currentRent);
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
