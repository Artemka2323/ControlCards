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
    /// Логика взаимодействия для AddEditPageSections.xaml
    /// </summary>
    public partial class AddEditPageSections : Page
    {
        private Sections _currentItem = new Sections();
        public AddEditPageSections(Sections selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение раздела";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            CMBPattern.SelectedValuePath = "IdPatter";
            CMBPattern.DisplayMemberPath = "Title";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Title))) error.AppendLine("Укажите название раздела.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_currentItem.IdSections == 0)
            {
                ControlCardMalchikEntities.GetContext().Sections.Add(_currentItem);
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageSections());
                    MessageBox.Show("Новый раздел успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageSections());
                    MessageBox.Show("Раздел успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageSections());
        }
    }
}