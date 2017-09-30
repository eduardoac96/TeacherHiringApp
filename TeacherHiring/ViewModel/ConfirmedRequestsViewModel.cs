using Acr.UserDialogs;
using Domain.Student;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class ConfirmedRequestsViewModel:BaseViewModel
    {
        private ObservableCollection<DtoRequestStatus> _items;

        public ObservableCollection<DtoRequestStatus> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoadItemsCommand { get; set; }

        public ConfirmedRequestsViewModel()
        {
            Items = new ObservableCollection<DtoRequestStatus>();
            LoadItemsCommand = new Command(async () => await GetItemsDataSource());
        }

        async Task GetItemsDataSource()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (UserDialogs.Instance.Loading("Obteniendo asesorias confirmadas..."))
                {
                    Items.Clear();
                    Items = new ObservableCollection<DtoRequestStatus>(await ApiServices.TeacherServices.GetConfirmedRequests(App.LoggedUser.UserID));
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("No se pudierón obtener las asesorías", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
