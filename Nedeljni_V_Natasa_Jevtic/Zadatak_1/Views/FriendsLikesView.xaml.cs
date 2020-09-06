using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for FriendsLikesView.xaml
    /// </summary>
    public partial class FriendsLikesView : Window
    {
        public FriendsLikesView(vwFriendPost post)
        {
            InitializeComponent();
            this.DataContext = new FriendsLikesViewModel(this, post);
        }
    }
}
