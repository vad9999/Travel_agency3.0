using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>
    public partial class UserCard : Window
    {
        public event EventHandler Card;
        public UserCard()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            string number = NumberCardBox.Text;
            string username = NameCardBox.Text;
            string date = DateCardBox.Text;
            string cvc = CVCCardBox.Text;

            if (string.IsNullOrEmpty(number) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(date) ||
                    string.IsNullOrEmpty(cvc))
            {
                MessageBox.Show("Пожалуйста заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!long.TryParse(number, out long NUMBER))
            {
                MessageBox.Show("Номер должен быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(number.Length < 12)
            {
                MessageBox.Show("Номер должен быть 12 значным числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(cvc, out int CVC))
            {
                MessageBox.Show("CVC должен быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(cvc.Length < 3)
            {
                MessageBox.Show("CVC должен быть 3 значным числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string format = "MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(date, format, provider, DateTimeStyles.None, out DateTime parsedDate))
            {
                MessageBox.Show("Строка не соотвествует формату ММ-ГГГГ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Card?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void NumberCardBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NumberCardBox.Text == "Номер карты")
                NumberCardBox.Text = "";
        }

        private void NumberCardBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NumberCardBox.Text == "")
                NumberCardBox.Text = "Номер карты";
        }

        private void NameCardBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameCardBox.Text == "Имя пользователя карты")
                NameCardBox.Text = "";
        }

        private void NameCardBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameCardBox.Text == "")
                NameCardBox.Text = "Имя пользователя карты";
        }

        private void DateCardBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DateCardBox.Text == "Дата карты формата ММ-ГГГГ")
                DateCardBox.Text = "";
        }

        private void DateCardBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DateCardBox.Text == "")
                DateCardBox.Text = "Дата карты формата ММ-ГГГГ";
        }

        private void CVCCardBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CVCCardBox.Text == "CVC код карты")
                CVCCardBox.Text = "";
        }

        private void CVCCardBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CVCCardBox.Text == "")
                CVCCardBox.Text = "CVC код карты";
        }
    }
}
