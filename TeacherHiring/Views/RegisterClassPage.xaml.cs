using Acr.UserDialogs;
using Plugin.Geolocator;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterClassPage : ContentPage
    {
        RegisterClassPageViewModel _viewModel;

        public RegisterClassPage()
        {
            InitializeComponent();
        }
        public RegisterClassPage(RegisterClassPageViewModel viewModel)
        {
            Title = "Registrar asesoría";
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            getGeolocation();
        }

        private async void getGeolocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (!CrossGeolocator.Current.IsGeolocationAvailable)
            {
                await UserDialogs.Instance.AlertAsync("El servicio de GPS no está disponible en su dispositivo.", "GPS", "Ok");
            }
            else if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                await UserDialogs.Instance.AlertAsync("El servicio de GPS no está habilitado en su dispositivo.", "GPS", "Ok");
            }

            else
            {
                if (!CrossGeolocator.Current.IsListening)
                    await locator.StartListeningAsync(5, 3000);

                var position = await locator.GetPositionAsync();

                _viewModel.Item.CurrentLatitude = position.Latitude.ToString();
                _viewModel.Item.CurrentLongitude = position.Longitude.ToString();

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(_viewModel.Item.CurrentLatitude), double.Parse(_viewModel.Item.CurrentLongitude)), Distance.FromMiles(1)));
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var locator = e.Position;

            _viewModel.Item.CurrentLatitude = locator.Latitude.ToString();
            _viewModel.Item.CurrentLongitude = locator.Longitude.ToString();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(_viewModel.Item.CurrentLatitude), double.Parse(_viewModel.Item.CurrentLongitude)), Distance.FromMiles(1)));
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            await CrossGeolocator.Current.StopListeningAsync();
        }
    }
}