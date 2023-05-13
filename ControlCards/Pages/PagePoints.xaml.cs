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
    /// Логика взаимодействия для PagePoints.xaml
    /// </summary>
    public partial class PagePoints : Page
    {
        public PagePoints()
        {
            InitializeComponent();
            dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }

        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            if (SearchPanel.Visibility == Visibility.Hidden)
            {
                SearchMargin.Height = new GridLength(0.8, GridUnitType.Star);
                SearchPanel.Visibility = Visibility.Visible;
                MenuSearch.Header = "Убрать поиск";
            }
            else
            {
                SearchMargin.Height = new GridLength(0, GridUnitType.Star);
                SearchPanel.Visibility = Visibility.Hidden;
                MenuSearch.Header = "Поиск";
            }
        }

        private void txbPos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dtgPoints.ItemsSource != null) dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.Where(x => x.Title.ToLower().Contains(txbPoint.Text.ToLower())).ToList();
            if (txbPoint.Text.Count() == 0) dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }
        private void MenuAddItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPagePoints(null));
        }

        private void MenuEditItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPagePoints((Points)dtgPoints.SelectedItem));
        }

        private void MenuUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }

        private void MenuDelItem_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = dtgPoints.SelectedItems.Cast<Points>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {rowsForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().Points.RemoveRange(rowsForRemoving);
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.ToList();
                    txtCountRows.Text = dtgPoints.Items.Count.ToString();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void MenuSortASC_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.OrderBy(x => x.Title).ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }

        private void MenuSortDESC_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.OrderByDescending(x => x.Title).ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }

        private void MenuSortFiltСlear_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardMalchikEntities.GetContext().Points.ToList();
            txtCountRows.Text = dtgPoints.Items.Count.ToString();
        }
    }
}