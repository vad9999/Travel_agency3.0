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
            string password;
            User user = userRepository.GetUserByEmail(email);

            if (showpass.IsChecked == true)
                password = PasswordBox.Text;
            else
                password = pass.Password;

            if (email.Trim() != "" && password.Trim() != "")
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
                                userTour.Top = this.Top;
                                userTour.Left = this.Left;
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
                            adminTour.Left = this.Left;
                            adminTour.Top = this.Top;
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
            signUp.Left = this.Left;
            signUp.Top = this.Top;
            signUp.Show();
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