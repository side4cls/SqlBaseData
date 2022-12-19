using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Клиенты _currentClient = new Клиенты();
        public AddEditPage(Клиенты selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
                _currentClient = selectedClient;
            DataContext = _currentClient;
            List<string> Клиенты = new List<string>();
            new HotelClientBaseEntities().Клиенты.ToList().ForEach(o => { Клиенты.Add(o.Имя_Клиента); });
           
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentClient.Фамилия_Клиента))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(_currentClient.Имя_Клиента))
                errors.AppendLine("Укажите Имя");
            if (string.IsNullOrWhiteSpace(_currentClient.Отчество_Клиента))
                errors.AppendLine("Укажите Отчество");
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentClient.Код_Клиента == 0)
                HotelClientBaseEntities.GetContext().Клиенты.Add(_currentClient);
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
