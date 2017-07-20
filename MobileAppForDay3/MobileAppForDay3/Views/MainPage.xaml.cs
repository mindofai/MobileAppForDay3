using MobileAppForDay3.Models;
using MobileAppForDay3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppForDay3.Views
{
    public partial class MainPage : ContentPage
    {
        bool authenticated = false;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MainPageViewModel vm = (MainPageViewModel)BindingContext;
            vm.SelectedDebt = (Debt)e.Item;
            vm.OpenUpdate();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MainPageViewModel vm = (MainPageViewModel)BindingContext;

            if (authenticated == true)
            {
                vm.RefreshDebtsCommand.Execute(null);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (authenticated == true)
            {
                loginBtn.IsVisible = false;
                MainPageViewModel vm = (MainPageViewModel)BindingContext;
                vm.RefreshDebtsCommand.Execute(null);
            }
        }
    }
}
