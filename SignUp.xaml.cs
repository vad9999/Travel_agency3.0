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
using System.Windows.Shapes;

namespace Travel_agency
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            IUserRepository UserRepository = new UserRepository(new AppDbContext());
            if (NameBox.Text != "Введите имя" && NameBox.Text != "")
            {
                if (EmailBox.Text != "Введите адрес эл. почты" && EmailBox.Text != "")
                {
                    if (PasswordBox.Text != "Введите пароль" && PasswordBox.Text != "")
                    {
                        bool email = false;
                        var users = UserRepository.GetAllUsers().ToList();
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (users[i].Email == UserRepository.GetHash(EmailBox.Text))
                            {
                                email = true;
                                MessageBox.Show("Пользователь с такой почтой уже зарегистрирован");
                                break;
                            }
                        }
                        if (!email)
                        {
                            if(DataProcessingCheck.IsChecked == true)
                            {
                                if(EmailBox.Text.Contains('@'))
                                {
                                    UserRepository.AddUser(new User { Name = NameBox.Text, Email = EmailBox.Text, Password = UserRepository.GetHash(PasswordBox.Text), IsAdmin = false, Blocking = false, isLogin = false });
                                    NameBox.Text = "Введите имя";
                                    EmailBox.Text = "Введите адрес эл. почты";
                                    PasswordBox.Text = "Введите пароль";
                                    DataProcessingCheck.IsChecked = false;
                                }
                                else
                                {
                                    MessageBox.Show("Эл. почта должна содержать @");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Дайте согласие на обработку персональных данных!");
                            }
                        }
                    }
                }
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Введите имя")
                NameBox.Text = "";
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
                NameBox.Text = "Введите имя";
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "Введите адрес эл. почты")
                EmailBox.Text = "";
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "")
                EmailBox.Text = "Введите адрес эл. почты";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
                PasswordBox.Text = "";
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "")
                PasswordBox.Text = "Введите пароль";
        }
    }
}
