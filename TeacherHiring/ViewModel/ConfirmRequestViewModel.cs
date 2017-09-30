using Acr.UserDialogs;
using Domain.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class ConfirmRequestViewModel : BaseViewModel
    {
        private DtoRequestStatus _request;
        public DtoRequestStatus Item
        {
            get
            {
                return _request;
            }
            set
            {
                _request = value;
                RaisePropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }

        public ConfirmRequestViewModel(DtoRequestStatus request)
        {
            _request = request;
            SaveCommand = new Command(async () => await Save());
        }


        async Task Save()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                using (UserDialogs.Instance.Loading("Procesando solicitud..."))
                {
                    await ApiServices.TeacherServices.ConfirmRequest(Item);
                }
                    

                if (await UserDialogs.Instance.ConfirmAsync("El registró se realizó correctamente, ¿Deseas confirmar otra asesoría?", "Confirmación asesoría", "Sí", "No"))
                {
                    await MasterNavigateTo(new PendingRequestsPage());
                }
                else
                {
                    await MasterNavigateToMain(new ShellPage());
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
