using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Travel_agency
{
    public partial class AdminTour : Window
    {
        private int _currentPage = 1;
        private const int _itemsPerPage = 6;
        private List<object> _allItems;
        private List<object> _filteredItems;

        public AdminTour()
        {
            InitializeComponent();
            LoadData();
            IsOnePage();
            UpdateListView();
            UpdatePaginationButtons();
            PreviousButtonn.IsEnabled = false;
        }

        private void IsOnePage()
        {
            if (GetTotalPages() <= 1)
            {
                PreviousButtonn.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else
            {
                NextButton.IsEnabled = true;
            }
        }
        private void LoadData()
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());

            _allItems = TourRepository.DateCheckAndGetList();
            _filteredItems = new List<object>(_allItems);
        }

        private void UpdateListView()
        {
            TourHotelListView.ItemsSource = GetPagedFilteredData();
        }
        private List<object> GetPagedFilteredData()
        {
            string searchText = SearchBox.Text.ToLower();
            _filteredItems = _allItems.Where(item =>
            {
                if (item is Tours tour)
                {
                    return tour.Name.ToLower().Contains(searchText);
                }
                else if (item is Hotels hotel)
                {
                    return hotel.Name.ToLower().Contains(searchText);
                }
                return false;
            }).ToList();

            ApplySort();

            return _filteredItems.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        }

        private int GetTotalPages()
        {
            return (int)Math.Ceiling((double)_filteredItems.Count / _itemsPerPage);
        }

        private void UpdatePaginationButtons()
        {
            int totalPages = GetTotalPages();
            PreviousButtonn.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = _currentPage < totalPages;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentPage = 1;
            UpdateListView();
            UpdatePaginationButtons();
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
            UpdateListView();
        }

        private void ApplySort()
        {
            var selectedSort = SortBox.SelectedItem as ComboBoxItem;
            if (selectedSort != null)
            {
                string sortBy = selectedSort.Tag.ToString();

                if (sortBy == "Name")
                {
                    _filteredItems = _filteredItems.OrderBy(item =>
                        item is Tours tour ? tour.Name :
                        item is Hotels hotel ? hotel.Name : "").ToList();
                }
                else if (sortBy == "Price")
                {
                    _filteredItems = _filteredItems.OrderBy(item =>
                        item is Tours tour ? tour.Price :
                        item is Hotels hotel ? hotel.Price : 0).ToList();

                }
                else if (sortBy == "Country")
                {
                    _filteredItems = _filteredItems.OrderBy(item =>
                        item is Tours tour ? tour.Country :
                        item is Hotels hotel ? hotel.Country : "").ToList();
                }
                else if (sortBy == "Description")
                {
                    _filteredItems = _filteredItems.OrderBy(item =>
                        item is Tours tour ? tour.Description :
                        item is Hotels hotel ? hotel.Description : "").ToList();
                }
            }
        }

        private void AllUsersButton_Click(object sender, RoutedEventArgs e)
        {
            AdminListUsers adminListUsers = new AdminListUsers();
            adminListUsers.Left = this.Left;
            adminListUsers.Top = this.Top;
            adminListUsers.ShowDialog();
        }

        private void AddTourButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAddTour adminAddTour = new AdminAddTour();
            adminAddTour.Left = this.Left;
            adminAddTour.Top = this.Top;
            adminAddTour.ItemAdded += AddWindow_ItemAdded;
            adminAddTour.ShowDialog();
        }

        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAddHotel adminAddHotel = new AdminAddHotel();
            adminAddHotel.Top = this.Top;
            adminAddHotel.Left = this.Left;
            adminAddHotel.ItemAdded += AddWindow_ItemAdded;
            adminAddHotel.ShowDialog();
        }

        private void ReservationListButton_Click(object sender, RoutedEventArgs e)
        {
            AdminReservationList adminReservationList = new AdminReservationList();
            adminReservationList.Left = this.Left;
            adminReservationList.Top = this.Top;
            adminReservationList.ShowDialog();
        }

        private void AddWindow_ItemAdded(object sender, EventArgs e)
        {
            LoadData();
            UpdateListView();
            IsOnePage();
            UpdatePaginationButtons();
            if (_currentPage == GetTotalPages())
                NextButton.IsEnabled = false;
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            ArchiveAdmin archiveAdmin = new ArchiveAdmin();
            archiveAdmin.Top = this.Top;
            archiveAdmin.Left = this.Left;
            archiveAdmin.ItemNonArchive += AddWindow_ItemAdded;
            archiveAdmin.ShowDialog();
        }

        private void ZipButton_Click(object sender, RoutedEventArgs e)
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());
            if (TourHotelListView.SelectedItem != null)
            {
                if (TourHotelListView.SelectedItem is Tours)
                {
                    Tours selectedTour = (Tours)TourHotelListView.SelectedItem;
                    selectedTour.IsArchive = true;
                    TourRepository.UpdateTour(selectedTour);
                }
                if (TourHotelListView.SelectedItem is Hotels)
                {
                    Hotels selectedHotel = (Hotels)TourHotelListView.SelectedItem;
                    selectedHotel.IsArchive = true;
                    HotelRepository.UpdateHotel(selectedHotel);     
                }
                if (TourRepository.DateCheckAndGetList().Count == 6)
                    _currentPage = 1;
                LoadData();
                UpdateListView();
                IsOnePage();
                UpdatePaginationButtons();
            }
        }

        private void PreviousButtonn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateListView();
                UpdatePaginationButtons();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < GetTotalPages())
            {
                _currentPage++;
                UpdateListView();
                UpdatePaginationButtons();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Left = this.Left;
            main.Top = this.Top;
            main.Show();
            this.Close();
        }
    }
}
