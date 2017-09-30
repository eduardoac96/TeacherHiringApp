using Acr.UserDialogs;
using Domain.Security;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TeacherHiring.Services;
using Xamarin.Forms;

namespace TeacherHiring
{
	public class BaseViewModel : INotifyPropertyChanged
	{
       
        public BaseViewModel()
        {

        }
        public AppServices ApiServices => new AppServices(App.Current.Resources["apiUrl"].ToString());
        
		public static INavigation Navigation { get; set; }
		public static MasterDetailPage MasterDetail { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set { _isBusy = value; RaisePropertyChanged(); }
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set { _title = value; RaisePropertyChanged(); }
		}

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public async Task NavigateTo(Page pageView)
		{
			await Navigation.PushModalAsync(pageView);
		}

		public async Task MasterNavigateTo(Page pageView)
		{
			MasterDetail.IsPresented = false;
			await MasterDetail.Detail.Navigation.PushAsync(pageView);
		}

        public async Task MasterNavigateToMain(Page page)
        {
            MasterDetail.IsPresented = false;
            NavigationPage.SetHasNavigationBar(page, false);

            await MasterDetail.Detail.Navigation.PushAsync(page);
        }


        /// <summary>
        /// Ask
        /// </summary>
        /// <param name="pageView"></param>
		public void NavigateToPageCurrent(Page pageView)
		{
			Application.Current.MainPage = pageView;
		}

		public async Task NavigateGoBack()
		{
			await Navigation.PopAsync();
		}

        public async void SaveUserInformation(DtoUser user)
        {
            App.Current.Properties["User"] = JsonConvert.SerializeObject(user).ToString();
            await App.Current.SavePropertiesAsync();
        }

    }
}
