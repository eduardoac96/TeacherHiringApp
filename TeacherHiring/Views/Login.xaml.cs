using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class Login : ContentPage
	{
		LoginViewModel viewModel;
		public Login()
		{
			InitializeComponent();
			BindingContext = viewModel = new LoginViewModel();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			//BaseViewModel.Navigation = this.Navigation;
		}
	}
}
