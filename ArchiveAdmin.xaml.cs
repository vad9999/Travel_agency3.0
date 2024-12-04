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
    /// Логика взаимодействия для ArchiveAdmin.xaml
    /// </summary>
    public partial class ArchiveAdmin : Window
    {
        public event EventHandler ItemNonArchive;
        public ArchiveAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());

            var combinedData = new List<object>();

            combinedData.AddRange(TourRepository.GetAllToursArchive());
            combinedData.AddRange(HotelRepository.GetAllHotelsArchive());

            ArchiveListView.ItemsSource = combinedData;
        }

        private void UnZipButton_Click(object sender, RoutedEventArgs e)
        {
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());
            ITourRepository TourRepository = new TourRepository(new AppDbContext());

            if (ArchiveListView.SelectedItem != null)
            {
                if (ArchiveListView.SelectedItem is Tours)
                {
                    Tours selectedTour = (Tours)ArchiveListView.SelectedItem;
                    if(selectedTour.EndDate < DateOnly.FromDateTime(DateTime.Today))
                    {
                        MessageBox.Show("Измените дату окончания тура", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        selectedTour.IsArchive = false;
                        TourRepository.UpdateTour(selectedTour);
                    }
                }
                if (ArchiveListView.SelectedItem is Hotels)
                {
                    Hotels selectedHotel = (Hotels)ArchiveListView.SelectedItem;
                    selectedHotel.IsArchive = false;
                    HotelRepository.UpdateHotel(selectedHotel);
                }
                LoadData();
                ItemNonArchive?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ItemNonArchive?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void EditTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArchiveListView.SelectedItem != null)
            {
                object selectedItem = ArchiveListView.SelectedItem;
                AdminEdit adminEdit = new AdminEdit(selectedItem);
                adminEdit.ItemAdded += AddWindow_ItemAdded;
                adminEdit.ShowDialog();
            }
        }

        private void AddWindow_ItemAdded(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
