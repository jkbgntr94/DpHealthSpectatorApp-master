﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlertSelectionPage : ContentPage
	{
		public AlertSelectionPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new AlertSelectionViewModel();
        }
    }
}