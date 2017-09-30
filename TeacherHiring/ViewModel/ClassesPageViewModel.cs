using Acr.UserDialogs;
using Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class ClassesPageViewModel : BaseViewModel
    {
        private ObservableCollection<DtoClassAvailable> _items;
        public ObservableCollection<DtoClassAvailable> Items {
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

        public Command LoadItemsCommand { get; set; }
        

        public ClassesPageViewModel()
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
