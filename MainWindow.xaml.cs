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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IUserRepository UserRepository = new UserRepository(new AppDbContext());

            bool admin = false;
            var users = UserRepository.GetAllUsers().ToList();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].IsAdmin)
                {
                    admin = true;
                    break;
                }
            }
            if (!admin)
            {
                UserRepository.AddUser(new User { Name = "admin", Email = "admin", Password = UserRepository.GetHash("admin"), IsAdmin = true, Blocking = false, isLogin = false });
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            IUserRepository UserRepository = new UserRepository(new AppDbContext());

            if ((EmailBox.Text != "Введите эл. почту" && EmailBox.Text != "") && (PasswordBox.Text != "Введите пароль" && PasswordBox.Text != ""))
            {
                if(UserRepository.GetUserByEmail(EmailBox.Text) != null)
                {
                    if (UserRepository.GetUserByEmail(EmailBox.Text).Password == UserRepository.GetHash(PasswordBox.Text))
                    {
                        if(EmailBox.Text == "admin" && PasswordBox.Text == "admin")
                        {
                            AdminTour adminTour = new AdminTour();
                            adminTour.Show();
                            this.Close();
                        }
                        else
                        {
                            if (!UserRepository.GetBlockUser(EmailBox.Text))
                            {
                                UserRepository.GetUserByEmail(EmailBox.Text).isLogin = true;
                                UserRepository.UpdateUser(UserRepository.GetUserByEmail(EmailBox.Text));
                                UserTour userTour = new UserTour();
                                userTour.Show();
                                this.Close();
                            }
                            else
                            {
                                if (MessageBox.Show("Вы заблокированы админимтратором!", "", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                                {
                                    this.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправльный пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с такой почтой не зарегистрирован!");
                }
            }
            else
            {
                MessageBox.Show("Введите почту или пароль");
            }
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
            if (EmailBox.Text == "")
                EmailBox.Text = "Введите эл. почту";
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "")
                PasswordBox.Text = "Введите пароль";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
                PasswordBox.Text = "";
        }
    }
}