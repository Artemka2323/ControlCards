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
    /// Логика взаимодействия для AddEditPagePoints.xaml
    /// </summary>
    public partial class AddEditPagePoints : Page
    {
        private Points _currentItem = new Points();
        public AddEditPagePoints(Points selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение пункта";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
            CMBSection.SelectedValuePath = "IdSection";
            CMBSection.DisplayMemberPath = "Title";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Title))) error.AppendLine("Укажите название пункта.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_currentItem.IdPoints == 0)
            {
                ControlCardMalchikEntities.GetContext().Points.Add(_currentItem);
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PagePoints());
                    MessageBox.Show("Новый пункт успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PagePoints());
                    MessageBox.Show("Пункт успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PagePoints());
        }
    }
}
