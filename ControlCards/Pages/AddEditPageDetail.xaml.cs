using ControlCards.Classes;
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

namespace ControlCards.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageDetail.xaml
    /// </summary>
    public partial class AddEditPageDetail : Page
    {
        private Detail _currentItem = new Detail();
        public AddEditPageDetail(Detail selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение детали";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Title))) error.AppendLine("Укажите название детали.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_currentItem.IdDetail == 0)
            {
                ControlCardMalchikEntities.GetContext().Detail.Add(_currentItem);
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageDetail());
                    MessageBox.Show("Новая деталь успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageDetail());
                    MessageBox.Show("Деталь успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageDetail());
        }
    }
}
