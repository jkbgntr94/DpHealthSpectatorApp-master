using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.ViewModels;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Phase2StressPage : ContentPage
	{
		public Phase2StressPage (Queue<Activities> aktivity)
		{
			InitializeComponent ();
            this.BindingContext = new Phase2StressViewModel(aktivity);
        }

       
    }
}