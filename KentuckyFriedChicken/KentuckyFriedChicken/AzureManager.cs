using KentuckyFriedChicken.DataModels;
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


        private IMobileServiceTable<NotFastFoodModel> notFastFoodTable;


        private AzureManager()
        {
            client = new MobileServiceClient("https://myfastfoodapp.azurewebsites.net/");
            this.notFastFoodTable = this.client.GetTable<NotFastFoodModel>();
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
        public async Task<List<NotFastFoodModel>> GetFastFoodInformation()
        {
            return await this.notFastFoodTable.ToListAsync();
        }
    }
}
