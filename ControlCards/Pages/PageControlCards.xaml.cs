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
using Microsoft.Office.Interop.Excel;
using Microsoft.Vbe.Interop;

namespace ControlCards.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageControlCards.xaml
    /// </summary>
    public partial class PageControlCards : System.Windows.Controls.Page
    {
        public PageControlCards()
        {

            InitializeComponent();
            ControlCardLoad();
        }

        public void ControlCardLoad()
        {
            List<ControlCard> controls = ControlCardMalchikEntities.GetContext().ControlCard.ToList();
            dtgControlCard.ItemsSource = controls;
            txtCountRows.Text = dtgControlCard.Items.Count.ToString();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dtgControlCard.ItemsSource != null) dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.Where(x => x.Pattern.Title.ToLower().Contains(txbSearch.Text.ToLower())).ToList();
            if (txbSearch.Text.Count() == 0) dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.ToList();
            txtCountRows.Text = dtgControlCard.Items.Count.ToString();
        }

        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            if (SearchPanel.Visibility == Visibility.Hidden)
            {
                SearchMargin.Height = new GridLength(3.5, GridUnitType.Star);
                SearchPanel.Visibility = Visibility.Visible;
                MenuSearch.Header = "Убрать поиск";
            }
            else
            {
                SearchMargin.Height = new GridLength(9, GridUnitType.Star);
                SearchPanel.Visibility = Visibility.Hidden;
                MenuSearch.Header = "Поиск";
            }
        }
        private void MenuAddItem_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddEditPageControlCard(null));
        }

        private void MenuEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgControlCard.SelectedItem == null) MessageBox.Show("Выберите карту контроля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else ClassFrame.frmObj.Navigate(new AddEditPageControlCard((ControlCard)dtgControlCard.SelectedItem));
        }

        private void MenuUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            ControlCardLoad();
        }

        private void MenuDelItem_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = dtgControlCard.SelectedItems.Cast<ControlCard>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {rowsForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().ControlCard.RemoveRange(rowsForRemoving);
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.ToList();
                    txtCountRows.Text = dtgControlCard.Items.Count.ToString();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void MenuSortASC_Click(object sender, RoutedEventArgs e)
        {
            dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.OrderBy(x => x.Pattern.Title).ToList();
            txtCountRows.Text = dtgControlCard.Items.Count.ToString();
        }

        private void MenuSortDESC_Click(object sender, RoutedEventArgs e)
        {
            dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.OrderByDescending(x => x.Pattern.Title).ToList();
            txtCountRows.Text = dtgControlCard.Items.Count.ToString();
        }

        private void MenuSortFiltСlear_Click(object sender, RoutedEventArgs e)
        {
            dtgControlCard.ItemsSource = ControlCardMalchikEntities.GetContext().ControlCard.ToList();
            txtCountRows.Text = dtgControlCard.Items.Count.ToString();
        }



        private void DoubleClickMore_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dtgControlCard.SelectedItem == null) MessageBox.Show("Выберите карту контроля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ClassFrame.frmObj.Navigate(new PageProjectExecutionControlCard((ControlCard)dtgControlCard.SelectedItem));
            }
        }

        private void MenuMore_Click(object sender, RoutedEventArgs e)
        {
            if (dtgControlCard.SelectedItem == null) MessageBox.Show("Выберите карту контроля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ClassFrame.frmObj.Navigate(new PageProjectExecutionControlCard((ControlCard)dtgControlCard.SelectedItem));
            }
        }

        private void MenuPrint_Click(object sender, RoutedEventArgs e)
        {
            if (dtgControlCard.SelectedItem == null) MessageBox.Show("Выберите карту контроля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ControlCard _currentItem = (ControlCard)dtgControlCard.SelectedItem;
                var app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add();
                Worksheet worksheet = app.Worksheets.Item[1];

                worksheet.Cells[1][1] = $"Карта контроля №{_currentItem.IdControlCard}";
                Range titleRange = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[7][1]];
                titleRange.Merge();
                titleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 20;

                worksheet.Cells[2][3] = "Название детали:";
                worksheet.Cells[2][3].HorizontalAlignment = XlHAlign.xlHAlignRight;
                worksheet.Cells[3][3] = _currentItem.Pattern.Detail.Title;
                worksheet.Cells[3][3].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                Range custComRange = worksheet.Range[worksheet.Cells[3][3], worksheet.Cells[7][3]];
                custComRange.Merge();

                worksheet.Cells[2][5] = "Заводской номер:";
                worksheet.Cells[2][5].HorizontalAlignment = XlHAlign.xlHAlignRight;
                worksheet.Cells[3][5] = _currentItem.SerialNumber;
                worksheet.Cells[3][5].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                Range projectRange = worksheet.Range[worksheet.Cells[3][5], worksheet.Cells[7][5]];
                projectRange.Merge();

                worksheet.Cells[2][7] = "Серийный номер:";
                worksheet.Cells[2][7].HorizontalAlignment = XlHAlign.xlHAlignRight;
                worksheet.Cells[3][7] = _currentItem.FactoryNumber;
                worksheet.Cells[3][7].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                Range priorityRange = worksheet.Range[worksheet.Cells[3][7], worksheet.Cells[7][7]];
                priorityRange.Merge();

                worksheet.Cells[2][9] = "Дата приемки:";
                worksheet.Cells[2][9].HorizontalAlignment = XlHAlign.xlHAlignRight;
                worksheet.Cells[3][9] = _currentItem.DateOfAcceptance;
                worksheet.Cells[3][9].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                Range dateStartRange = worksheet.Range[worksheet.Cells[3][9], worksheet.Cells[7][9]];
                dateStartRange.Merge();

                worksheet.Cells[3][14] = "Сборка";
                worksheet.Cells[3][14].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                worksheet.Cells[4][14] = "ОТК";
                worksheet.Cells[4][14].HorizontalAlignment = XlHAlign.xlHAlignCenter;

                int IdSection = 1;
                int IndexRows = 15;

                foreach (Sections section in ControlCardMalchikEntities.GetContext().Sections.Where(x => x.IdPattern == _currentItem.IdPattern).ToList())
                {
                    worksheet.Cells[1][IndexRows] = $"{IdSection}. {section.Title}";
                    IndexRows++;
                    int IdPoint = 1;
                    foreach (Classes.Points point in ControlCardMalchikEntities.GetContext().Points.Where(x => x.IdSection == section.IdSections).ToList())
                    {
                        worksheet.Cells[2][IndexRows] = $"{IdSection}.{IdPoint} {point.Title}";
                        IdPoint++;
                        int i = 0;
                        foreach (Answer a in ControlCardMalchikEntities.GetContext().Answer.Where(x => x.IdControlCard == _currentItem.IdControlCard && x.IdPoint == point.IdPoints).ToList())
                        {
                            worksheet.Cells[3 + i][IndexRows] = a.Title;
                            worksheet.Cells[3 + i][IndexRows].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            worksheet.Cells[3 + i][IndexRows].VerticalAlignment = XlVAlign.xlVAlignCenter;
                            i++;
                        }
                        IndexRows++;
                    }
                    IndexRows++;
                    IdSection++;
                }
                worksheet.Columns.AutoFit();
                worksheet.Columns[1].ColumnWidth = 15;
                worksheet.Columns[2].ColumnWidth = 75;
                worksheet.Columns[2].Cells.WrapText = true;
                worksheet.Columns[2].Cells.WrapText = true;
                worksheet.Columns[3].ColumnWidth = 10;
                worksheet.Columns[4].ColumnWidth = 10;
                app.Visible = true;
            }
        }
    }
}

