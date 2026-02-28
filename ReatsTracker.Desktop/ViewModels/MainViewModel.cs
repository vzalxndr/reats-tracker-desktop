using ReatsTracker.Desktop.Models;
using ReatsTracker.Desktop.Services;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
