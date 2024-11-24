using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Travel_agency
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IRoleRepository roleRepository = new RoleRepository(new AppDbContext());
            IUserRepository userRepository = new UserRepository(new AppDbContext());

            if(roleRepository.GetAll().Count == 0)
                roleRepository.AddRoles();
            if(userRepository.GetAllUsers().Count == 0)
                userRepository.AddAdmin();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            IUserRepository userRepository = new UserRepository(new AppDbContext());
            IRoleRepository roleRepository = new RoleRepository(new AppDbContext());

            string email = EmailBox.Text;
            string password = PasswordBox.Text;
            User user = userRepository.GetUserByEmail(email);

            if (email != "Введите эл. почту" && email.Trim() != "" && password != "Введите пароль" && password.Trim() != "")
            {
                if(user != null)
                {
                    if (user.Password == userRepository.GetHash(password))
                    {
                        if(user.RoleId == roleRepository.CustomerId())
                        {
                            if (!userRepository.GetBlockUser(email))
                            {
                                Session.CurrentUser = user;
                                UserTour userTour = new UserTour();
                                userTour.Show();
                                this.Close();
                            }
                            else
                            {
                                if (MessageBox.Show("Вы заблокированы администратором!", "", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                                    this.Close();
                            }
                        }
                        else
                        {
                            AdminTour adminTour = new AdminTour();
                            adminTour.Show();
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Неправльный пароль!");
                }
                else
                    MessageBox.Show("Пользователь с такой почтой не зарегистрирован!");
            }
            else
                MessageBox.Show("Введите почту и(или) пароль");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "Введите эл. почту")
                EmailBox.Text = "";
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Trim() == "")
                EmailBox.Text = "Введите эл. почту";
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text.Trim() == "")
                PasswordBox.Text = "Введите пароль";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
                PasswordBox.Text = "";
        }
    }
}