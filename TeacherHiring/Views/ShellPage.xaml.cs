using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class ShellPage : MasterDetailPage
	{
		public ShellPage()
		{
			InitializeComponent();
			MasterPage.ListView.ItemSelected += ListView_ItemSelected;
			MasterBehavior = MasterBehavior.Popover;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BaseViewModel.Navigation = Navigation;
			BaseViewModel.MasterDetail = this;
		}
		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageMenuItem;
			if (item == null)
				return;

			var page = (Page)Activator.CreateInstance(item.TargetType);
			page.Title = item.Title;

			Detail = new NavigationPage(page);
			IsPresented = false;

			MasterPage.ListView.SelectedItem = null;
		}
	}
}
