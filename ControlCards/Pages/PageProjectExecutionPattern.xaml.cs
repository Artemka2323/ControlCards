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
using static System.Collections.Specialized.BitVector32;

namespace ControlCards.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProjectExecutionPattern.xaml
    /// </summary>
    public partial class PageProjectExecutionPattern : Page
    {
        private Pattern _currentItem = new Pattern();

        public PageProjectExecutionPattern(Pattern item)
        {
            InitializeComponent();
            if (item != null) _currentItem = item;
            DataContext = _currentItem;
            LoadTreeView();
        }

        private void BtnToBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PagePattern());
        }

        public void LoadTreeView()
        {
            foreach (Sections section in ControlCardMalchikEntities.GetContext().Sections.Where(x => x.IdPattern == _currentItem.IdPattern).ToList())
            {
                TreeViewItem item = new TreeViewItem() { Header = section.Title };
                foreach (Points point in ControlCardMalchikEntities.GetContext().Points.Where(x => x.IdSection == section.IdSections).ToList())
                    item.Items.Add(new TreeViewItem() { Header = point.Title });
                trvTemplateStructure.Items.Add(item);
            }
        }
    }
}