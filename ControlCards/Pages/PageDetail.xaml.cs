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
    /// Логика взаимодействия для PageDetail.xaml
    /// </summary>
    public partial class PageDetail : Page
    {
        public PageDetail()
        {
            InitializeComponent();
            dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
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
            if (dtgDetail.ItemsSource != null) dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.Where(x => x.Title.ToLower().Contains(txbDet.Text.ToLower())).ToList();
            if (txbDet.Text.Count() == 0) dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
        }
        private void MenuAddItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPageDetail(null));
        }

        private void MenuEditItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPageDetail((Detail)dtgDetail.SelectedItem));
        }

        private void MenuUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
        }

        private void MenuDelItem_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = dtgDetail.SelectedItems.Cast<Detail>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {rowsForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().Detail.RemoveRange(rowsForRemoving);
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
                    txtCountRows.Text = dtgDetail.Items.Count.ToString();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void MenuSortASC_Click(object sender, RoutedEventArgs e)
        {
            dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.OrderBy(x => x.Title).ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
        }

        private void MenuSortDESC_Click(object sender, RoutedEventArgs e)
        {
            dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.OrderByDescending(x => x.Title).ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
        }

        private void MenuSortFiltСlear_Click(object sender, RoutedEventArgs e)
        {
            dtgDetail.ItemsSource = ControlCardMalchikEntities.GetContext().Detail.ToList();
            txtCountRows.Text = dtgDetail.Items.Count.ToString();
        }
    }
}
