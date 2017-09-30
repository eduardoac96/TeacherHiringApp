using Domain.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmedRequestsPage : ContentPage
	{
        public ConfirmedRequestsViewModel viewModel;
		public ConfirmedRequestsPage ()
		{
            Title = "Próximas asesorías";
			InitializeComponent ();
            BindingContext = viewModel = new ConfirmedRequestsViewModel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DtoRequestStatus;
            if (item == null)
                return;

            var teacher = App.LoggedUser;


            await viewModel.MasterNavigateTo(new CounselingDetailPage(item));

            // Manually deselect item
            ListItems.SelectedItem = null;
        }
    }
}