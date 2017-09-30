
using Domain.Student;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PendingRequestsPage : ContentPage
	{
        public PendingRequestsViewModel viewModel;
		public PendingRequestsPage ()
		{
            Title = "Confirmar asesorias";
			InitializeComponent();
            BindingContext = viewModel = new PendingRequestsViewModel();
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
            

            await viewModel.MasterNavigateTo(new ConfirmRequestPage(item));

            // Manually deselect item
            ListItems.SelectedItem = null;
        }
    }
}