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
            vm.RefreshDebtsCommand.Execute(null);
        }



    }
}
