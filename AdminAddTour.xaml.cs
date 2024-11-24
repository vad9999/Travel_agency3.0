using Microsoft.Win32;
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

namespace Travel_agency
{
    public partial class AdminAddTour : Window
    {
        string imagePath = null!;
        public event EventHandler ItemAdded;

        public AdminAddTour()
        {
            InitializeComponent();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                imagePath = openFileDialog.FileName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());

            string tourName = NameTourBox.Text;
            string tourDescription = DescriptionTourBox.Text;
            string tourPrice = PriceTourBox.Text;
            string tourCountry = CountryTourBox.Text;

            if (string.IsNullOrEmpty(tourName) ||
                string.IsNullOrEmpty(tourDescription) ||
                string.IsNullOrEmpty(tourPrice) ||
                string.IsNullOrEmpty(tourCountry) ||
                string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("Пожалуйста заполните все поля и добавьте фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(tourPrice, out decimal price))
            {
                MessageBox.Show("Цена должна быть цислом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TourRepository.DoubleName(tourName))
            {
                MessageBox.Show("Тур с таким названием уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourRepository.AddTour(new Tours { Name = tourName, Description = tourDescription, Country = tourCountry, Price = decimal.Parse(tourPrice), ImageData = File.ReadAllBytes(imagePath), IsArchive = false, Type = "Тур", StartDate = DateOnly.Parse(StartDateBox.Text), EndDate = DateOnly.Parse(EndDateBox.Text) });

            ItemAdded?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void NameTourBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTourBox.Text == "Введите название тура")
                NameTourBox.Text = "";
        }

        private void NameTourBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTourBox.Text.Trim() == "")
                NameTourBox.Text = "Введите название тура";
        }

        private void CountryTourBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountryTourBox.Text == "Введите страну тура")
                CountryTourBox.Text = "";
        }

        private void CountryTourBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountryTourBox.Text.Trim() == "")
                CountryTourBox.Text = "Введите страну тура";
        }

        private void DescriptionTourBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionTourBox.Text == "Введите описание тура")
                DescriptionTourBox.Text = "";
        }

        private void DescriptionTourBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionTourBox.Text.Trim() == "")
                DescriptionTourBox.Text = "Введите описание тура";
        }

        private void PriceTourBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PriceTourBox.Text == "Введите цену тура")
                PriceTourBox.Text = "";
        }

        private void PriceTourBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PriceTourBox.Text.Trim() == "")
                PriceTourBox.Text = "Введите цену тура";
        }

        private void StartDateBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (StartDateBox.Text == "Введите дату начала тура")
                StartDateBox.Text = "";
        }

        private void StartDateBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (StartDateBox.Text.Trim() == "")
                StartDateBox.Text = "Введите дату начала тура";
        }

        private void EndDateBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EndDateBox.Text.Trim() == "")
                EndDateBox.Text = "Введите дату окончания тура";
        }

        private void EndDateBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EndDateBox.Text == "Введите дату окончания тура")
                EndDateBox.Text = "";
        }
    }
}