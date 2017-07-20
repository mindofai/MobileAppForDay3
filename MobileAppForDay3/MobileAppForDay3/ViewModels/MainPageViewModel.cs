using MobileAppForDay3.Models;
using MobileAppForDay3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppForDay3.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Debt> _debts;
        public List<Debt> Debts
        {
            get { return _debts; }
            set
            {
                _debts = value;
                OnPropertyChanged();
            }
        }

        private string _nameValue;

        public string NameValue
        {
            get { return _nameValue; }
            set
            {
                _nameValue = value;
                OnPropertyChanged();
            }
        }

        private string _amountValue;

        public string AmountValue
        {
            get { return _amountValue; }
            set
            {
                _amountValue = value;
                OnPropertyChanged();
            }
        }


        private bool _isAddShow;

        public bool IsAddShow
        {
            get { return _isAddShow; }
            set
            {
                _isAddShow = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshBusy;

        public bool IsRefreshBusy
        {
            get { return _isRefreshBusy; }
            set
            {
                _isRefreshBusy = value;
                OnPropertyChanged();
            }
        }


        private bool _isPaidShow;

        public bool IsPaidShow
        {
            get { return _isPaidShow; }
            set
            {
                _isPaidShow = value;
                OnPropertyChanged();
            }
        }

        private Debt _selectedDebt;

        public Debt SelectedDebt
        {
            get { return _selectedDebt; }
            set
            {
                _selectedDebt = value;
                OnPropertyChanged();
            }
        }

        private Command _openAddCommand;
        public Command OpenAddCommand =>
            _openAddCommand ?? (_openAddCommand = new Command(OpenAdd));

        private Command _closeAddCommand;
        public Command CloseAddCommand =>
            _closeAddCommand ?? (_closeAddCommand = new Command(CloseAdd));

        private Command _addDebtCommand;
        public Command AddDebtCommand =>
            _addDebtCommand ?? (_addDebtCommand = new Command(async () => await AddDebt()));

        private Command _updateDebtCommand;
        public Command UpdateDebtCommand =>
            _updateDebtCommand ?? (_updateDebtCommand = new Command(async () => await UpdateDebt()));

        private Command _refreshDebtsCommand;

        public Command RefreshDebtsCommand =>
            _refreshDebtsCommand ?? (_refreshDebtsCommand = new Command(async () => await RefreshDebts()));

        private AzureMobileService _azureMobileService;


        #endregion

        #region Constructor
        public MainPageViewModel()
        {

            _azureMobileService = DependencyService.Get<AzureMobileService>();

            Debts = new List<Debt>();
            SelectedDebt = new Debt();
            NameValue = string.Empty;
            AmountValue = string.Empty;

            IsBusy = false;
            IsAddShow = false;
        }
        #endregion

        #region Methods
        private async Task RefreshDebts()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsRefreshBusy = true;
                var debts = await _azureMobileService.GetAllDebts();
                Debts = debts.Where(d => d.IsPaid == false).ToList();
                IsRefreshBusy = false;
            });
        }

        public async Task UpdateDebt()
        {
            IsBusy = true;

            if (!await _azureMobileService.UpdateDebt(SelectedDebt))
                return;

            await RefreshDebts();

            IsBusy = false;
            IsPaidShow = false;
        }

        public async Task AddDebt()
        {
            IsBusy = true;
            var debt = new Debt()
            {
                Name = NameValue,
                Amount = Convert.ToInt16(AmountValue),
                IsPaid = false
            };

            if (!await _azureMobileService.AddDebt(debt))
                return;

            await RefreshDebts();
            IsBusy = false;
            IsAddShow = false;
        }


        private void OpenAdd()
        {
            NameValue = string.Empty;
            AmountValue = string.Empty;

            IsAddShow = true;
        }

        public void OpenUpdate()
        {
            IsPaidShow = true;
        }

        private void CloseAdd()
        {
            IsAddShow = false;
            IsPaidShow = false;
        }

        void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
