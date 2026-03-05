using ReatsTracker.Desktop.Models;
using ReatsTracker.Desktop.Services;
using ReatsTracker.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReatsTracker.Desktop.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Company> _companies;
        private Company _selectedCompany;

        public ObservableCollection<Company> Companies
        {
            get
            {
                return _companies;
            }
            set
            {
                _companies = value;
                OnPropertyChanged();
            }
        }

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _apiService = new ApiService();
            Companies = new ObservableCollection<Company>();

            _ = LoadCompaniesAsync();
        }

        private async Task LoadCompaniesAsync()
        {
            var data = await _apiService.GetCompaniesAsync();
            Companies = new ObservableCollection<Company>(data);
        }

        public async Task AddCompanyAsync(Company company)
        {
            var response = await _apiService.AddCompanyAsync(company);

            if (response != null)
            {
                Companies.Add(response);
            }
        }

        public async Task DeleteCompanyAsync()
        {
            if (SelectedCompany == null)
            {
                return;
            }

            var response = await _apiService.DeleteCompanyAsync(SelectedCompany.Id);

            if (response)
            {
                Companies.Remove(SelectedCompany);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
