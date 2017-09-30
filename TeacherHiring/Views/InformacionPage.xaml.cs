using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class InformacionPage : ContentPage
	{
		InformacionViewModel viewModel;
		public InformacionPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new InformacionViewModel();
		}
	}
}
