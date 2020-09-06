using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for RequestsView.xaml
    /// </summary>
    public partial class RequestsView : UserControl
    {
        public RequestsView(vwUser user)
        {
            InitializeComponent();
            this.Name = "Requests";
            this.DataContext = new RequestsViewModel(this, user);
        }
    }
}