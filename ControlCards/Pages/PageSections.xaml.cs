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
    /// Логика взаимодействия для PageSections.xaml
    /// </summary>
    public partial class PageSections : Page
    {
        public PageSections()
        {
            InitializeComponent();
            dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
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
            if (dtgSection.ItemsSource != null) dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.Where(x => x.Title.ToLower().Contains(txbSec.Text.ToLower())).ToList();
            if (txbSec.Text.Count() == 0) dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
        }
        private void MenuAddItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPageSections(null));
        }

        private void MenuEditItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPageSections((Sections)dtgSection.SelectedItem));
        }

        private void MenuUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
        }

        private void MenuDelItem_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = dtgSection.SelectedItems.Cast<Sections>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {rowsForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().Sections.RemoveRange(rowsForRemoving);
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
                    txtCountRows.Text = dtgSection.Items.Count.ToString();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void MenuSortASC_Click(object sender, RoutedEventArgs e)
        {
            dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.OrderBy(x => x.Title).ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
        }

        private void MenuSortDESC_Click(object sender, RoutedEventArgs e)
        {
            dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.OrderByDescending(x => x.Title).ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
        }

        private void MenuSortFiltСlear_Click(object sender, RoutedEventArgs e)
        {
            dtgSection.ItemsSource = ControlCardMalchikEntities.GetContext().Sections.ToList();
            txtCountRows.Text = dtgSection.Items.Count.ToString();
        }
    }
}