using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddPostView.xaml
    /// </summary>
    public partial class AddPostView : Window
    {
        public AddPostView(vwUser user)
        {
            InitializeComponent();
            this.DataContext = new AddPostViewModel(this, user);
        }
    }
}
