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
            string password = PasswordBox.Text;

            if (name != "Введите имя" && name.Trim() != "" && email != "Введите адрес эл. почты" && email.Trim() != "" && password != "Введите пароль" && password.Trim() != "")
            {
                if (!userRepository.CheckUser(email))
                {
                    if(DataProcessingCheck.IsChecked == true)
                    {
                        if(email.Contains('@'))
                        {
                            userRepository.AddUser(new User { Name = name, Email = email, Password = userRepository.GetHash(password), RoleId =  roleRepository.CustomerId() });
                            NameBox.Text = "Введите имя";
                            EmailBox.Text = "Введите адрес эл. почты";
                            PasswordBox.Text = "Введите пароль";
                            DataProcessingCheck.IsChecked = false;
                        }
                        else
                            MessageBox.Show("Эл. почта должна содержать @");
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
            if (NameBox.Text.Trim() == "")
                NameBox.Text = "Введите имя";
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "Введите адрес эл. почты")
                EmailBox.Text = "";
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Trim() == "")
                EmailBox.Text = "Введите адрес эл. почты";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
                PasswordBox.Text = "";
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text.Trim() == "")
                PasswordBox.Text = "Введите пароль";
        }
    }
}
