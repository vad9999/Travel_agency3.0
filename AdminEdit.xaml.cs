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
    /// <summary>
    /// Логика взаимодействия для AdminEdit.xaml
    /// </summary>
    public partial class AdminEdit : Window
    {
        public event EventHandler ItemAdded;
        private bool IsTour;
        string imagePath;
        public Tours TourToEdit { get; set; }
        public Hotels HotelToEdit { get; set; }

        public AdminEdit(object obj)
        {
            InitializeComponent();
            if(obj is Tours)
            {
                IsTour = true;
                TourToEdit = (Tours)obj;
                EditNameBox.Text = TourToEdit.Name;
                EditCountryBox.Text = TourToEdit.Country;
                DiscriptionEditBox.Text = TourToEdit.Description;
                PriceEditBox.Text = TourToEdit.Price.ToString();
            }
            else
            {
                IsTour = false;
                HotelToEdit = (Hotels)obj;
                EditNameBox.Text = HotelToEdit.Name;
                EditCountryBox.Text = HotelToEdit.Country;
                DiscriptionEditBox.Text = HotelToEdit.Description;
                PriceEditBox.Text = HotelToEdit.Price.ToString();
            }   
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());

            string Name = EditNameBox.Text;
            string Description = DiscriptionEditBox.Text;
            string Country = EditCountryBox.Text;
            string Price = PriceEditBox.Text;

            if (string.IsNullOrEmpty(Name) ||
               string.IsNullOrEmpty(Description) ||
               string.IsNullOrEmpty(Country) ||
               string.IsNullOrEmpty(Price))
            {
                MessageBox.Show("Пожалуйста заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(Price, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsTour)
            {
                TourToEdit.Name = Name;
                TourToEdit.Description = Description;
                TourToEdit.Country = Country;
                TourToEdit.Price = decimal.Parse(Price);
                if (imagePath != null)
                    TourToEdit.ImageData = File.ReadAllBytes(imagePath);
                TourRepository.UpdateTour(TourToEdit);
                ItemAdded?.Invoke(this, EventArgs.Empty);
                DialogResult = true;
            }
            else
            {
                HotelToEdit.Name = Name;
                HotelToEdit.Description = Description;
                HotelToEdit.Country = Country;
                HotelToEdit.Price = decimal.Parse(Price);
                if (imagePath != null)
                    HotelToEdit.ImageData = File.ReadAllBytes(imagePath);
                HotelRepository.UpdateHotel(HotelToEdit);
                ItemAdded?.Invoke(this, EventArgs.Empty);
                DialogResult = true;
            }
        }

        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                imagePath = openFileDialog.FileName;
        }
    }
}
