using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileAppForDay3.Models;
using MobileAppForDay3.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureMobileService))]
namespace MobileAppForDay3.Services
{

    public class AzureMobileService
    {
        public MobileServiceClient Client { get; private set; }
        private IMobileServiceTable<Debt> debtTable;

        public void Initialize()
        {
            Client = new MobileServiceClient("http://mobileappsamplelast.azurewebsites.net");
            debtTable = Client.GetTable<Debt>();
        }

        public async Task<List<Debt>> GetAllDebts()
        {
            try
            {
                Initialize();
                return await debtTable.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Debt>();
            }
        }

        public async Task<bool> AddDebt(Debt debt)
        {
            try
            {
                await debtTable.InsertAsync(debt);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDebt(Debt debt)
        {
            try
            {
                debt.IsPaid = true;
                await debtTable.UpdateAsync(debt);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
