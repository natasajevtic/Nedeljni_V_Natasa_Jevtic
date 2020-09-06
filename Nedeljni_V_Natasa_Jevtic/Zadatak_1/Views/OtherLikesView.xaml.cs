using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for OtherLikesView.xaml
    /// </summary>
    public partial class OtherLikesView : Window
    {
        public OtherLikesView(vwPost post)
        {
            InitializeComponent();
            this.DataContext = new OtherLikesViewModel(this, post);
        }
    }
}
