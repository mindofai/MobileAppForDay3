using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppForDay3.Services
{
    public partial class AzureManager
    {
        static AzureManager defaultInstance = new AzureManager();
        MobileServiceClient client;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://mobileappfordaythree.azurewebsites.net/");
        }

        public static AzureManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
    }
}
