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
    /// Логика взаимодействия для AddEditPageControlCard.xaml
    /// </summary>
    public partial class AddEditPageControlCard : Page
    {
        private ControlCard _currentItem = new ControlCard();
        public AddEditPageControlCard(ControlCard selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение карты контроля";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            CMBControlCard.SelectedValuePath = "IdPattern";
            CMBControlCard.DisplayMemberPath = "Title";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdPattern))) error.AppendLine("Укажите название шаблона.");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.SerialNumber))) error.AppendLine("Укажите серийный номер.");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.FactoryNumber))) error.AppendLine("Укажите заводской номер.");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.DateOfAcceptance))) error.AppendLine("Укажите дату приёмки.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_currentItem.IdControlCard == 0)
            {
                ControlCardMalchikEntities.GetContext().ControlCard.Add(_currentItem);
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageControlCards());
                    MessageBox.Show("Новая карта контроля успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageControlCards());
                    MessageBox.Show("Карта контроля успешно изменёна!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageControlCards());
        }
    }
}
