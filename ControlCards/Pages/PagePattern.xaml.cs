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
    /// Логика взаимодействия для PagePattern.xaml
    /// </summary>
    public partial class PagePattern : Page
    {
        public PagePattern()
        {
            InitializeComponent();
            PatternLoad();
        }

        public void PatternLoad()
        {
            List<Pattern> pattern = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            txtCountRows.Text = pattern.Count.ToString();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dtgPattern.ItemsSource != null) dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.Where(x => x.Title.ToLower().Contains(txbSearch.Text.ToLower()) || x.Detail.Title.ToLower().Contains(txbSearch.Text.ToLower())).ToList();
            if (txbSearch.Text.Count() == 0) dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            txtCountRows.Text = dtgPattern.Items.Count.ToString();
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
            ClassFrame.frmObj.Navigate(new AddEditPagePattern(null));
        }

        private void MenuEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgPattern.SelectedItem == null) MessageBox.Show("Выберите шаблон!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else ClassFrame.frmObj.Navigate(new AddEditPagePattern((Pattern)dtgPattern.SelectedItem));
        }

        private void MenuUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            PatternLoad();
        }

        private void MenuDelItem_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = dtgPattern.SelectedItems.Cast<Pattern>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {rowsForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardMalchikEntities.GetContext().Pattern.RemoveRange(rowsForRemoving);
                    ControlCardMalchikEntities.GetContext().SaveChanges();
                    dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
                    txtCountRows.Text = dtgPattern.Items.Count.ToString();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void MenuSortASC_Click(object sender, RoutedEventArgs e)
        {
            dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.OrderBy(x => x.Title).ToList();
            txtCountRows.Text = dtgPattern.Items.Count.ToString();
        }

        private void MenuSortDESC_Click(object sender, RoutedEventArgs e)
        {
            dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.OrderByDescending(x => x.Title).ToList();
            txtCountRows.Text = dtgPattern.Items.Count.ToString();
        }

        private void MenuSortFiltСlear_Click(object sender, RoutedEventArgs e)
        {
            dtgPattern.ItemsSource = ControlCardMalchikEntities.GetContext().Pattern.ToList();
            txtCountRows.Text = dtgPattern.Items.Count.ToString();
        }

        private void DoubleClickMore_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dtgPattern.SelectedItem == null) MessageBox.Show("Выберите шаблон!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ClassFrame.frmObj.Navigate(new PageProjectExecutionPattern((Pattern)dtgPattern.SelectedItem));
            }
        }

        private void MenuMore_Click(object sender, RoutedEventArgs e)
        {
            if (dtgPattern.SelectedItem == null) MessageBox.Show("Выберите шаблон!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ClassFrame.frmObj.Navigate(new PageProjectExecutionPattern((Pattern)dtgPattern.SelectedItem));
            }
        }


        //private void MenuPrint_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dtgProjects.SelectedItem == null) MessageBox.Show("Выберите проект!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        //    else
        //    {
        //        Projects _currentItem = (Projects)dtgProjects.SelectedItem;

        //        var app = new Microsoft.Office.Interop.Excel.Application();
        //        Workbook wb = app.Workbooks.Add();
        //        Worksheet worksheet = app.Worksheets.Item[1];

        //        worksheet.Cells[1][1] = $"Выполнение проекта №{_currentItem.idProject}";
        //        Range titleRange = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][1]];
        //        titleRange.Merge();
        //        titleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        titleRange.Font.Bold = true;
        //        titleRange.Font.Size = 20;

        //        worksheet.Cells[2][3] = "Компания заказчика:";
        //        worksheet.Cells[2][3].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][3] = _currentItem.CustomerCompanies.Name;
        //        worksheet.Cells[3][3].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range custComRange = worksheet.Range[worksheet.Cells[3][3], worksheet.Cells[7][3]];
        //        custComRange.Merge();

        //        worksheet.Cells[2][5] = "Название проекта:";
        //        worksheet.Cells[2][5].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][5] = _currentItem.Title;
        //        worksheet.Cells[3][5].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range projectRange = worksheet.Range[worksheet.Cells[3][5], worksheet.Cells[7][5]];
        //        projectRange.Merge();

        //        worksheet.Cells[2][7] = "Приоритет:";
        //        worksheet.Cells[2][7].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][7] = _currentItem.Priority;
        //        worksheet.Cells[3][7].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range priorityRange = worksheet.Range[worksheet.Cells[3][7], worksheet.Cells[7][7]];
        //        priorityRange.Merge();

        //        worksheet.Cells[2][9] = "Дата начала:";
        //        worksheet.Cells[2][9].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][9] = _currentItem.StartDate;
        //        worksheet.Cells[3][9].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range dateStartRange = worksheet.Range[worksheet.Cells[3][9], worksheet.Cells[7][9]];
        //        dateStartRange.Merge();

        //        worksheet.Cells[2][11] = "Дата конца:";
        //        worksheet.Cells[2][11].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][11] = _currentItem.StartDate;
        //        worksheet.Cells[3][11].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range dateEndRange = worksheet.Range[worksheet.Cells[3][11], worksheet.Cells[7][11]];
        //        dateEndRange.Merge();

        //        worksheet.Cells[2][13] = "Количество работников:";
        //        worksheet.Cells[2][13].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][13] = ProjectBDEntities.GetContext().PTW.Where(x => x.idProject == _currentItem.idProject).Count();
        //        worksheet.Cells[3][13].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range countWorkersRange = worksheet.Range[worksheet.Cells[3][13], worksheet.Cells[7][13]];
        //        countWorkersRange.Merge();

        //        worksheet.Cells[2][15] = "Статус:";
        //        worksheet.Cells[2][15].HorizontalAlignment = XlHAlign.xlHAlignRight;
        //        worksheet.Cells[3][15] = _currentItem.Status;
        //        worksheet.Cells[3][15].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //        Range statusRange = worksheet.Range[worksheet.Cells[3][15], worksheet.Cells[7][15]];
        //        statusRange.Merge();

        //        worksheet.Cells[1][18] = "Сотрудники, выполняющие данный проект:";
        //        Range tableRange = worksheet.Range[worksheet.Cells[1][18], worksheet.Cells[5][18]];
        //        worksheet.Cells[1][18].HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        tableRange.Merge();
        //        tableRange.Font.Size = 14;

        //        int IndexRows = 19;
        //        worksheet.Cells[1][IndexRows] = "№";
        //        worksheet.Cells[2][IndexRows] = "ФИО сотрудника";
        //        worksheet.Cells[3][IndexRows] = "Должность";
        //        worksheet.Cells[4][IndexRows] = "Задача";
        //        worksheet.Cells[5][IndexRows] = "Выполнение задачи";
        //        Range headerRange = worksheet.Range[worksheet.Cells[1][IndexRows], worksheet.Cells[5][IndexRows]];
        //        headerRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        headerRange.Font.Bold = true;
        //        headerRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //        headerRange.Borders.Weight = XlBorderWeight.xlThin;

        //        List<PTW> printItems = ProjectBDEntities.GetContext().PTW.Where(x => x.idProject == _currentItem.idProject).ToList();
        //        foreach (var item in printItems)
        //        {
        //            worksheet.Cells[1][IndexRows + 1] = IndexRows - 18;
        //            worksheet.Cells[1][IndexRows + 1].Font.Italic = true;
        //            worksheet.Cells[1][IndexRows + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //            worksheet.Cells[1][IndexRows + 1].Borders.LineStyle = XlLineStyle.xlContinuous;
        //            worksheet.Cells[1][IndexRows + 1].Borders.Weight = XlBorderWeight.xlThin;

        //            worksheet.Cells[2][IndexRows + 1] = $"{item.Workers.Surname} {item.Workers.Name} {item.Workers.Patronymic}";
        //            worksheet.Cells[3][IndexRows + 1] = item.Workers.Positions.Title;
        //            worksheet.Cells[4][IndexRows + 1] = item.Tasks.Title;
        //            worksheet.Cells[5][IndexRows + 1] = item.TaskStatuses.Name;
        //            IndexRows++;

        //            Range bodyRange = worksheet.Range[worksheet.Cells[2][IndexRows], worksheet.Cells[5][IndexRows]];
        //            bodyRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //            bodyRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //            bodyRange.Borders.Weight = XlBorderWeight.xlThin;
        //        }

        //        worksheet.Name = $"Выполнение проекта №{_currentItem.idProject}";
        //        worksheet.Columns.AutoFit();
        //        worksheet.Rows.AutoFit();
        //        worksheet.Columns[1].ColumnWidth = 5;
        //        app.Visible = true;
        //    }
        //}

        //        private void MenuFilter1_Click(object sender, RoutedEventArgs e)
        //        {
        //            List<Projects> projects = new List<Projects>();
        //            foreach (Projects p in ControlCardMalchikEntities.GetContext().Projects) if (p.Status == "Ожидается") projects.Add(p);
        //            dtgProjects.ItemsSource = projects;
        //            txtCountRows.Text = dtgProjects.Items.Count.ToString();
        //        }

        //        private void MenuFilter2_Click(object sender, RoutedEventArgs e)
        //        {
        //            List<Projects> projects = new List<Projects>();
        //            foreach (Projects p in ControlCardMalchikEntities.GetContext().Projects) if (p.Status == "Выполняется") projects.Add(p);
        //            dtgProjects.ItemsSource = projects;
        //            txtCountRows.Text = dtgProjects.Items.Count.ToString();
        //        }

        //        private void MenuFilter3_Click(object sender, RoutedEventArgs e)
        //        {
        //            List<Projects> projects = new List<Projects>();
        //            foreach (Projects p in ControlCardMalchikEntities.GetContext().Projects) if (p.Status == "Выполнен") projects.Add(p);
        //            dtgProjects.ItemsSource = projects;
        //            txtCountRows.Text = dtgProjects.Items.Count.ToString();
        //        }
    }
}
