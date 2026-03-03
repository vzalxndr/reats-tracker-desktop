using ReatsTracker.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReatsTracker.Desktop.Views
{
    /// <summary>
    /// Interaction logic for AddCompanyWindow.xaml
    /// </summary>
    public partial class AddCompanyWindow : Window
    {

        public Company NewCompany { get; private set; }

        public AddCompanyWindow()
        {
            InitializeComponent();
        }

        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            var company = new Company();
            company.Name = CompanyNameTextBox.Text;
            company.Website = CompanyUrlTextBox.Text;

            NewCompany = company;

            DialogResult = true;
        }

        private void CancelAddCompany_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
