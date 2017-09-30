using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Domain.Security;
using TeacherHiring.Views;

namespace TeacherHiring
{
	public class MasterPageViewModel : BaseViewModel
	{
		public ObservableCollection<MasterPageMenuItem> MenuItems { get; set; }

        public DtoUser User { get; set; }
		public ICommand SignOut { get; set; }

		public MasterPageViewModel()
		{
            User = App.LoggedUser;
            MenuItems = getMenuItems();
			SignOut = new Command(async () => await DeleteSession());
		}

        private ObservableCollection<MasterPageMenuItem> getMenuItems()
        {

            switch (App.LoggedUser.UserTypeID)
            {
                case 1:
                    return new ObservableCollection<MasterPageMenuItem>(new[] {
                        new MasterPageMenuItem{Id = 1, Title = "Inicio", TargetType=typeof(WelcomePage), IconPath = "home_black.png" },
                        new MasterPageMenuItem{Id = 2, Title = "Registrar asesoría", TargetType=typeof(ClassListPage), IconPath = "add_black.png" },
                        new MasterPageMenuItem{Id = 3, Title = "Confirmar asesoría", TargetType=typeof(PendingRequestsPage), IconPath = "check_black.png" },
                        new MasterPageMenuItem{Id = 4, Title = "Próximas asesorías", TargetType=typeof(ConfirmedRequestsPage), IconPath = "books_black.png" },
                    });
                case 2:
                    return new ObservableCollection<MasterPageMenuItem>(new[] {
                        new MasterPageMenuItem{Id = 1, Title = "Inicio", TargetType=typeof(WelcomePage), IconPath = "home_black.png" },
                        new MasterPageMenuItem{Id = 2, Title = "Solicitar asesoría", TargetType=typeof(RequestsPage), IconPath = "folder_black.png" },
                        new MasterPageMenuItem{Id = 3, Title = "Solicitudes realizadas", TargetType=typeof(RequestStatusPage), IconPath = "books_black.png" },
                    });
                default:
                    return new ObservableCollection<MasterPageMenuItem>();
            }
        }

		async Task DeleteSession()
		{
			var askClose = await Application.Current.MainPage.DisplayAlert(
				"Cerrar sesión",
				"¿Estás seguro que deseas cerrar sesión?",
				"Ok", "Cancelar"
			);

			if (!askClose)
				return;

            int userID = App.LoggedUser.UserID;


            Database.Model.Person person = new PersonController().GetAll().Result.Where(x => x.Id == App.LoggedUser.UserID).SingleOrDefault();
                
            if (person != null)
            {
                await new PersonController().Delete(person);
                App.Current.Properties["User"] = string.Empty;
            }

            NavigateToPageCurrent(new Login());
		}
	}
}
