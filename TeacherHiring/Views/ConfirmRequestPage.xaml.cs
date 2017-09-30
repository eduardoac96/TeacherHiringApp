using Domain.Student;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmRequestPage : ContentPage
	{
        public ConfirmRequestViewModel viewModel;
		public ConfirmRequestPage (DtoRequestStatus request)
		{
            Title = "Aceptar asesoría";
			InitializeComponent ();
            BindingContext = viewModel = new ConfirmRequestViewModel(request);
            setMapLocation();
		}   

        private async void setMapLocation()
        {


            Pin pinLocation = new Pin {
                 Position = new Position(viewModel.Item.Latitude, viewModel.Item.Longitude),
                 Type = PinType.Place,
                 Label = viewModel.Item.ClassName
            };

            MyMap.Pins.Add(pinLocation);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(viewModel.Item.Latitude, viewModel.Item.Longitude), Distance.FromMiles(1)));

        }
	}
}