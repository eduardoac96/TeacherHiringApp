using Acr.UserDialogs;
using Domain.Teacher;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class RegisterClassPageViewModel : BaseViewModel
    {
        private DtoNewClass _item;
        public DtoNewClass Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                RaisePropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }

        public RegisterClassPageViewModel(DtoNewClass item)
        {
            _item = item;
            SaveCommand = new Command(async() => await SaveClass());
        }

        async Task SaveClass()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (UserDialogs.Instance.Loading("Guardando materia..."))
                {
                    await ApiServices.TeacherServices.SaveClass(_item);
                }

                if (await UserDialogs.Instance.ConfirmAsync("El registró se realizó correctamente, ¿Deseas agregar otra materia?", "Confirmación registro", "Sí", "No"))
                {
                    await MasterNavigateTo(new ClassListPage());
                }
                else
                {
                    await MasterNavigateToMain(new ShellPage());
                }

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
