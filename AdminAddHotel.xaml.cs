using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Travel_agency
{
    public partial class AdminAddHotel : Window
    {
        string imagePath = null!;
        public event EventHandler ItemAdded;

        public AdminAddHotel()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());

            string hotelName = NameHotelBox.Text;
            string hotelDescription = DiscriptionHotelBox.Text;
            string hotelPrice = PriceHotelBox.Text;
            string hotelCountry = CountryHotelBox.Text;

            if (string.IsNullOrEmpty(hotelName) ||
                string.IsNullOrEmpty(hotelDescription) ||
                string.IsNullOrEmpty(hotelPrice) ||
                string.IsNullOrEmpty(hotelCountry) ||
                string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("Пожалуйста заполните все поля и добавьте фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(hotelPrice, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(HotelRepository.DoubleName(hotelName))
            {
                MessageBox.Show("Отель с таким названием уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            HotelRepository.AddHotel(new Hotels { Name = hotelName, Description = hotelDescription, Country = hotelCountry, Price = decimal.Parse(hotelPrice), ImageData = File.ReadAllBytes(imagePath), IsArchive = false, Type = "Отель" });

            ItemAdded?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                imagePath = openFileDialog.FileName;
        }

        private void NameHotelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameHotelBox.Text == "Введите название отеля")
                NameHotelBox.Text = "";
        }

        private void NameHotelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameHotelBox.Text.Trim() == "")
                NameHotelBox.Text = "Введите название отеля";
        }

        private void CountryHotelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountryHotelBox.Text == "Введите страну отеля")
                CountryHotelBox.Text = "";
        }

        private void CountryHotelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountryHotelBox.Text.Trim() == "")
                CountryHotelBox.Text = "Введите страну отеля";
        }

        private void DiscriptionHotelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DiscriptionHotelBox.Text == "Введите описание отеля")
                DiscriptionHotelBox.Text = "";
        }

        private void DiscriptionHotelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DiscriptionHotelBox.Text.Trim() == "")
                DiscriptionHotelBox.Text = "Введите описание отеля";
        }

        private void PriceHotelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PriceHotelBox.Text == "Введите цену отеля")
                PriceHotelBox.Text = "";
        }

        private void PriceHotelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PriceHotelBox.Text.Trim() == "")
                PriceHotelBox.Text = "Введите цену отеля";
        }
    }
}
