using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlissXamarinApp.Interfaces;
using Xamarin.Forms;

namespace BlissXamarinApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool IsNotBusy => !_isBusy;

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        public bool IsNotEmpty => !_isEmpty;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            try
            {
                var viewModelType = typeof(TViewModel);
                var viewModelTypeName = viewModelType.Name;
                var viewModelWordLength = "ViewModel".Length;
                var viewTypeName = $"BlissXamarinApp.Views.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
                var viewType = Type.GetType(viewTypeName);

                var page = Activator.CreateInstance(viewType) as Page;

                if (viewModelType.GetTypeInfo().DeclaredConstructors.Any(c => c.GetParameters().Any(p => p.ParameterType == typeof(IBlissApiService))))
                {
                    var argsList = args.ToList();
                    var blissXamarinApiService = DependencyService.Get<IBlissApiService>();
                    argsList.Insert(0, blissXamarinApiService);
                    args = argsList.ToArray();
                }
                var viewModel = Activator.CreateInstance(viewModelType, args);

                if (page != null)
                {
                    page.BindingContext = viewModel;
                }
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "Error display page", "OK");
            }
        }

        public virtual Task LoadAsync()
        {
            return Task.FromResult(0);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
