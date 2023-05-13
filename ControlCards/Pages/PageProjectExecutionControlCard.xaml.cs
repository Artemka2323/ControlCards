using ControlCards.Classes;
using Microsoft.Office.Interop.Excel;
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
using static System.Collections.Specialized.BitVector32;

namespace ControlCards.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProjectExecutionControlCard.xaml
    /// </summary>
    public partial class PageProjectExecutionControlCard : System.Windows.Controls.Page
    {
        private ControlCard _currentItem = new ControlCard();
        public PageProjectExecutionControlCard(ControlCard item)
        {
            InitializeComponent();
            if (item != null) _currentItem = item;
            DataContext = _currentItem;
            LoadTreeView();
        }
        public void LoadTreeView()
        {
            foreach (Sections section in ControlCardMalchikEntities.GetContext().Sections.Where(x => x.IdPattern == _currentItem.IdPattern).ToList())
            {
                TreeViewItem item = new TreeViewItem() { Header = section.Title };
                foreach (Classes.Points point in ControlCardMalchikEntities.GetContext().Points.Where(x => x.IdSection == section.IdSections).ToList())
                {
                    TreeViewItem item2 = new TreeViewItem() { Header = point.Title };
                    foreach (Answer a in ControlCardMalchikEntities.GetContext().Answer.Where(x => x.IdControlCard == _currentItem.IdControlCard && x.IdPoint == point.IdPoints).ToList())
                    {
                        StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
                        TextBlock tb = new TextBlock() { Text = a.Authorizations.TypeUsers.Title, Width = 75, Margin = new Thickness(0,0,20,0), Style = Resources["StyleTxt"] as System.Windows.Style };
                        System.Windows.Controls.TextBox txb = new System.Windows.Controls.TextBox() { Text = a.Title, Width = 100, HorizontalContentAlignment = HorizontalAlignment.Center };
                        sp.Children.Add(tb);
                        sp.Children.Add(txb);
                        item2.Items.Add(sp);
                    }
                    item.Items.Add(item2);
                }
                trvTemplateStructure.Items.Add(item);
            }
        }

        private void BtnToBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageControlCards());
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
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
