﻿using KentuckyFriedChicken.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KentuckyFriedChicken
{
	public partial class AzureTable : ContentPage
    
	{
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;
        public AzureTable()
		{
			InitializeComponent();
		}
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            List<NotFastFoodModel> notFastFoodInformation = await AzureManager.AzureManagerInstance.GetFastFoodInformation();

            FastFoodList.ItemsSource = notFastFoodInformation;
        }

    }
}