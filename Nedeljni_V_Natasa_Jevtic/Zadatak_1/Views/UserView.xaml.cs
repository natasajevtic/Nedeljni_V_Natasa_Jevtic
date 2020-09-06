using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public vwUser User { get; set; }

        public UserView(vwUser user)
        {
            InitializeComponent();
            User = user;
            this.DataContext = new UserViewModel(this, user);
            var menuProfile = new List<SubItem>();
            menuProfile.Add(new SubItem("View my profile", new ProfileView()));
            var item1 = new ItemMenu("Profile", menuProfile, PackIconKind.Person);

            var menuFeed = new List<SubItem>();
            menuFeed.Add(new SubItem("Search feed", new FeedView()));
            var item2 = new ItemMenu("Feed", menuFeed, PackIconKind.Feedback);

            var menuRequests = new List<SubItem>();
            menuRequests.Add(new SubItem("View and send requests", new RequestsView()));
            var item3 = new ItemMenu("Requests", menuRequests, PackIconKind.PersonQuestion);

            var item0 = new ItemMenu("", new UserControl(), PackIconKind.ViewDashboard);

            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item2, this));
            Menu.Children.Add(new UserControlMenuItem(item3, this));
        }

        public void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);

                if (screen.Name == "Profile")
                {
                    ProfileView profileView = new ProfileView();
                }
                else if (screen.Name == "Requests")
                {
                    RequestsView requestsView = new RequestsView();
                }
                else if (screen.Name == "Feed")
                {
                    FeedView feedView = new FeedView();
                }
            }
        }
    }
}