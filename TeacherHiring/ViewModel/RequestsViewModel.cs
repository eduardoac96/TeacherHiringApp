using Acr.UserDialogs;
using Domain.Teacher;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class RequestsViewModel:BaseViewModel
    {
        private ObservableCollection<DtoClassAvailable> _items;

        public ObservableCollection<DtoClassAvailable> Items
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

        public RequestsViewModel()
        {
            Items = new ObservableCollection<DtoClassAvailable>();
            LoadItemsCommand = new Command(async () => await GetItemsDataSource());
        }

        async Task GetItemsDataSource()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (UserDialogs.Instance.Loading("Obteniendo materias disponibles..."))
                {
                    Items.Clear();
                    Items = new ObservableCollection<DtoClassAvailable>(await ApiServices.TeacherServices.GetAvailableClasses(App.LoggedUser.Token));
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("No se pudierón obtener las materias", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
