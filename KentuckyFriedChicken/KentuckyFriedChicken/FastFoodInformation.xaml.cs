using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KentuckyFriedChicken
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FastFoodInformation : ContentPage
    {
        public FastFoodInformation()
        {

            InitializeComponent();
        }
        void click1(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("tel:092713540"));
        }
        void click2(object sender, EventArgs args)
        {   
            Device.OpenUri(new Uri("tel:095762013"));
        }
        void click3(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("tel:095282145"));
        }
        void click4(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("tel:092727715"));
        }

    }
}