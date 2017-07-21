using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentuckyFriedChicken
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;

        private AzureManager()
        {
            client = new MobileServiceClient("https://myfastfoodapp.azurewebsites.net/");
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }
    }
}
