using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for FeedView.xaml
    /// </summary>
    public partial class FeedView : UserControl
    {
        public FeedView(vwUser user)
        {
            InitializeComponent();
            this.Name = "Feed";
            this.DataContext = new FeedViewModel(this, user);
        }
    }
}