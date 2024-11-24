using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public partial class AdminListUsers : Window
    {
        public AdminListUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
                IUserRepository UserRepository = new UserRepository(new AppDbContext());
                UserListView.ItemsSource = UserRepository.GetAllUsers(); 
        }

        private void BlockingTrueButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListView.SelectedItem != null)
            {
                User selectedUser = (User)UserListView.SelectedItem;
                if(selectedUser.Id != 1) 
                {
                    using (var context = new AppDbContext())
                    {
                        IUserRepository UserRepository = new UserRepository(context);
                        selectedUser.Blocking = true;
                        UserRepository.UpdateUser(selectedUser);
                    }
                }
            }
            LoadUsers();
        }

        private void BlockingFalseButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListView.SelectedItem != null)
            {
                User selectedUser = (User)UserListView.SelectedItem;
                if (selectedUser.Id != 1)
                {
                    using (var context = new AppDbContext())
                    {
                        IUserRepository UserRepository = new UserRepository(context);
                        selectedUser.Blocking = false;
                        UserRepository.UpdateUser(selectedUser);
                    }
                }
            }
            LoadUsers();
        }
    }
}
