using ControlCards.Classes;
using ControlCards.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace ControlCards
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClassFrame.frmObj = FrameWindow;
        }

        private void btnSingIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbLogin.Text == "" && tbPassword.Password == "")
                    MessageBox.Show("Поля ввода не могут быть пустыми!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (tbLogin.Text == "")
                    MessageBox.Show("Поля логина не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (tbPassword.Password == "")
                    MessageBox.Show("Поля пароля не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    var user = ControlCardMalchikEntities.GetContext().Authorizations.FirstOrDefault(x => x.Logins == tbLogin.Text && x.Passwords == tbPassword.Password);
                    if (user == null)
                        MessageBox.Show("Неправильный логин или пароль!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        stplAutorizations.Visibility= Visibility.Hidden;
                        ClassFrame.user = user;
                        stplLobby.Visibility= Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex.Message);
            }
        }

        private void btnToControlCard_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.PageControlCards());
            stplLobby.Visibility = Visibility.Hidden;
            btnToBack.Visibility = Visibility.Visible;
        }

        private void btnToPattern_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.PagePattern());
            stplLobby.Visibility = Visibility.Hidden;
            btnToBack.Visibility = Visibility.Visible;
        }

        private void btnToSections_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.PageSections());
            stplLobby.Visibility = Visibility.Hidden;
            btnToBack.Visibility = Visibility.Visible;
        }

        private void btnToPoints_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.PagePoints());
            stplLobby.Visibility = Visibility.Hidden;
            btnToBack.Visibility = Visibility.Visible;
        }

        private void btnToDetail_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.PageDetail());
            stplLobby.Visibility = Visibility.Hidden;
            btnToBack.Visibility = Visibility.Visible;
        }
        private void btnToBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(null);
            stplLobby.Visibility = Visibility.Visible;
            btnToBack.Visibility = Visibility.Hidden;
        }
    }
}