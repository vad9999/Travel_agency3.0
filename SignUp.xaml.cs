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
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            IUserRepository userRepository = new UserRepository(new AppDbContext());
            IRoleRepository roleRepository = new RoleRepository(new AppDbContext());

            string name = NameBox.Text;
            string email = EmailBox.Text;
            string password;

            if (showpass.IsChecked == true)
                password = PasswordBox.Text;
            else
                password = pass.Password;

            if (name.Trim() != "" && email.Trim() != "" && password.Trim() != "")
            {
                if (!userRepository.CheckUser(email))
                {
                    if(DataProcessingCheck.IsChecked == true)
                    {
                        if(email.Contains('@') && email.Contains('.'))
                        {
                            userRepository.AddUser(new User { Name = name, Email = email, Password = userRepository.GetHash(password), RoleId =  roleRepository.CustomerId() });
                            NameBox.Text = "Введите имя";
                            EmailBox.Text = "Введите адрес эл. почты";
                            PasswordBox.Text = "Введите пароль";
                            DataProcessingCheck.IsChecked = false;
                        }
                        else
                            MessageBox.Show("Эл. почта должна содержать @ и .");
                    }
                    else
                        MessageBox.Show("Дайте согласие на обработку персональных данных!");
                }
                else
                    MessageBox.Show("Пользователь с такой почтой уже зарегистрирован");
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void showpass_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Text = pass.Password;
            pass.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
        }

        private void showpass_Unchecked(object sender, RoutedEventArgs e)
        {
            pass.Password = PasswordBox.Text;
            PasswordBox.Visibility = Visibility.Collapsed;
            pass.Visibility = Visibility.Visible;
        }
    }
}
