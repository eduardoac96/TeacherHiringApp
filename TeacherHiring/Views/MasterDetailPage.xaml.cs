using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class MasterDetailMaster : ContentPage
	{
		public ListView ListView;
		MasterPageViewModel viewModel;
		public MasterDetailMaster()
		{
            InitializeComponent();
			BindingContext = viewModel = new MasterPageViewModel();
            ListView = MenuItemsListView;
            SignOutText.GestureRecognizers.Add(new TapGestureRecognizer { Command = viewModel.SignOut });
		}
	}
}
