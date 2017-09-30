using System;
using System.Collections.Generic;
using TeacherHiring.ViewModel;
using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class WelcomePage : ContentPage
	{
        WelcomeViewModel viewmodel;
        public WelcomePage()
        {
            InitializeComponent();
            BindingContext = viewmodel = new WelcomeViewModel();
        }
	}
}
