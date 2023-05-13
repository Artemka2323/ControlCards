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
    /// Логика взаимодействия для AddEditPagePattern.xaml
    /// </summary>
    public partial class AddEditPagePattern : Page
    {
        private Pattern _currentItem = new Pattern();
        public AddEditPagePattern(Pattern selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение шаблона";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
            CMBPattern.SelectedValuePath = "IdDetail";
            CMBPattern.DisplayMemberPath = "Title";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Title))) error.AppendLine("Укажите название шаблона.");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdDetail))) error.AppendLine("Укажите деталь.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_currentItem.IdPattern == 0)
            {
                ControlCardMalchikEntities.GetContext().Pattern.Add(_currentItem);
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PagePattern());
                    MessageBox.Show("Новый шаблон успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PagePattern());
                    MessageBox.Show("Шаблон успешно изменён!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PagePattern());
        }
    }
}
