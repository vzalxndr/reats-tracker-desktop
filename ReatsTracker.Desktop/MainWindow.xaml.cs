using ReatsTracker.Desktop.ViewModels;
using ReatsTracker.Desktop.Views;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReatsTracker.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private async void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddCompanyWindow();
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                var viewModel = (MainViewModel)DataContext;
                await viewModel.AddCompanyAsync(window.NewCompany);
            }
        }

        private async void DeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            await viewModel.DeleteCompanyAsync();
        }
    }
}