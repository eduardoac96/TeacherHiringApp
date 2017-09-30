using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace TeacherHiring
{
	public class InformacionViewModel : BaseViewModel
	{
		private Informacion _informacion { get; set; }
		public string FullName
		{
			get { return _informacion.FullName; }
			set { _informacion.FullName = value; RaisePropertyChanged(); }
		}
		public string Username
		{
			get { return _informacion.Username; }
			set { _informacion.Username = value; RaisePropertyChanged(); }
		}
		public string Password
		{
			get { return _informacion.Password; }
			set { _informacion.Password = value; RaisePropertyChanged(); }
		}
		public string LatLong
		{
			get { return _informacion.LatLong; }
			set { _informacion.LatLong = value; RaisePropertyChanged(); }
		}
		public IGeolocator GeoLocator { get; set; }
		public ICommand GetLocation { get; set; }
		public ICommand SaveInformacion { get; set; }
		public ICommand ViewMap { get; set; }
		public InformacionViewModel()
		{
			_informacion = new Informacion();
			GeoLocator = CrossGeolocator.Current;
			GetLocation = new Command(async () => await GetUserLocationAsync());
			SaveInformacion = new Command(async () => await SaveInformacionAsync());
			ViewMap = new Command(async () => await ViewMapAsync());
		}
		public async Task GetUserLocationAsync()
		{
			if (IsBusy)
				return;

			if (!GeoLocator.IsGeolocationEnabled)
			{
				await Application.Current.MainPage.DisplayAlert("Error Localizacion", "Es necesario activar el GPS", "Ok");
				IsBusy = false;
				return;
			}
			try
			{
				UserDialogs.Instance.ShowLoading("Obteniendo Ubicacion...");
				Position position = await GeoLocator.GetPositionAsync((int)TimeSpan.FromSeconds(8).TotalMilliseconds);
				UserDialogs.Instance.HideLoading();
				if (position == null)
				{
					await Application.Current.MainPage.DisplayAlert("Error Localizacion", "No se pudo obtener tu ubicacion.", "Ok");
					IsBusy = false;
					return;
				}
				LatLong = position.Latitude.ToString() + "," + position.Longitude.ToString();
			}
			catch (Exception ex)
			{
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
			finally
			{
				IsBusy = false;
			}
		}
		public async Task SaveInformacionAsync()
		{
			if (IsBusy)
				return;

			try
			{
				await GetUserLocationAsync();
				using (UserDialogs.Instance.Loading("Guardando la informacion..."))
				{
					await Task.Delay(3000);
				}
				//NavigateToPageCurrent(new ShellPage());
			}
			catch (Exception ex)
			{
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
			finally
			{
				IsBusy = false;
			}
		}
		public async Task ViewMapAsync()
		{
			if (IsBusy)
				return;

			try
			{
				Position position = await GeoLocator.GetPositionAsync((int)TimeSpan.FromSeconds(8).TotalMilliseconds);
				await NavigateTo(new MapPage(position.Latitude, position.Longitude));
			}
			catch (Exception ex)
			{
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
			finally
			{
				IsBusy = false;
			}
		}
	}
}
