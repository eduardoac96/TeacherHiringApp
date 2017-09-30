using Acr.UserDialogs;
using Domain.Teacher;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherHiring.Views;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class ScheduleRequestViewModel : BaseViewModel
    {
        private DtoTeacherSchedule _schedule;
        public DtoTeacherSchedule Schedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                _schedule = value;
                RaisePropertyChanged();
            }
        }

            public ICommand RequestCommand { get; set; }
        public ScheduleRequestViewModel(DtoTeacherSchedule schedule)
        {
          _schedule = schedule;
            RequestCommand = new Command(async () => await ConfirmClass());
        }

        async Task ConfirmClass()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await ApiServices.TeacherServices.ConfirmClass(_schedule);

                if (await UserDialogs.Instance.ConfirmAsync("El registro se realizó correctamente, ¿Deseas agregar otra materia?", "Confirmación registro", "Sí", "No"))
                {
                    await MasterNavigateTo(new RequestsPage());
                }
                else
                {
                    await MasterNavigateToMain(new ShellPage());
                }
            }
            catch (Exception ex)
            {
              await  UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
