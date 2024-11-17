using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Travel_agency
{
    public partial class UserTour : Window
    {
        private int _currentPage = 1;
        private const int _itemsPerPage = 6;

        private List<object> _allItems; 
        private List<object> _filteredItems; 

        public UserTour()
        {
            InitializeComponent();
            LoadData();
            IsOnePage();
            UpdateListView();
            UpdatePaginationButtons();
            PreviousButtonn.IsEnabled = false;
        }

        private void LoadData()
        {
            ITourRepository TourRepository = new TourRepository(new AppDbContext());
            _allItems = TourRepository.DateCheckAndGetList();
            _filteredItems = new List<object>(_allItems);
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

        private void ReservationListButton_Click(object sender, RoutedEventArgs e)
        {
            UserReservations userReservations = new UserReservations();
            userReservations.ShowDialog();
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

        private void UpdatePaginationButtons()
        {
            int totalPages = GetTotalPages();
            PreviousButtonn.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = _currentPage < totalPages;
        }

        private void ReservarionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TourHotelListView.SelectedItem != null)
            {
                UserCard userCard = new UserCard();
                userCard.Card += CardWindow;
                userCard.ShowDialog();
            }
        }

        private void CardWindow(object sender, EventArgs e)
        {
            IReservationRepository ReservationRepository = new ReservationRepository(new AppDbContext());
            IUserRepository UserRepository = new UserRepository(new AppDbContext());

            if (TourHotelListView != null)
            {
                var selectedItem = TourHotelListView.SelectedItem;

                if (selectedItem is Tours)
                {
                    
                    Tours tour = (Tours)selectedItem;
                    string user = UserRepository.UserAutentification().Email;
                    var reservations = ReservationRepository.UserReservation(user);
                    bool IsReservated = false;
                    for (int i = 0; i < reservations.Count; i++)
                    {
                        if (reservations[i].TourOrHotelId == tour.Id && reservations[i].Type == tour.Type)
                        {
                            IsReservated = true;
                        }
                    }
                    if (IsReservated)
                    {
                        MessageBox.Show("Вы уже забронировали этот тур");
                    }
                    else
                    {
                        ReservationRepository.AddReservarion(new Reservation { UserEmail = user, TourOrHotelId = tour.Id, Type = tour.Type, IsConfirm = false, Name = tour.Name, ReservationDate = DateOnly.FromDateTime(DateTime.Now) });
                        MessageBox.Show("Тур забронирован!");
                    }
                }
                else
                {
                    string user = UserRepository.UserAutentification().Email;
                    var reservations = ReservationRepository.UserReservation(user);
                    Hotels hotel = (Hotels)selectedItem;
                    bool IsReservated = false;

                    for (int i = 0; i < reservations.Count; i++)
                    {
                        if (reservations[i].TourOrHotelId == hotel.Id && reservations[i].Type == hotel.Type)
                        {
                            IsReservated = true;
                        }
                    }
                    if (IsReservated)
                    {
                        MessageBox.Show("Вы уже забронировали этот отель");
                    }
                    else
                    {
                        ReservationRepository.AddReservarion(new Reservation { UserEmail = user, TourOrHotelId = hotel.Id, Type = hotel.Type, IsConfirm = false, Name = hotel.Name });
                        MessageBox.Show("Отель забронирован!");
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IUserRepository UserRepository = new UserRepository(new AppDbContext());
            User user = UserRepository.UserAutentification();
            user.isLogin = false;
            UserRepository.UpdateUser(user);
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
                else if(sortBy == "Country")
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
    }
}
