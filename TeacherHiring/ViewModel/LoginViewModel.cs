using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Linq;
using Domain.Security;

namespace TeacherHiring
{
	public class LoginViewModel : BaseViewModel
	{
		private DtoLogin _user;
		public DtoLogin User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(); }
		}
		public ICommand LoginCommand { get; set; }
		public LoginViewModel()
		{
			LoginCommand = new Command(async () => await Login());
			User = new DtoLogin();
		}
		async Task Login()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				string username = _user.Username;
				string password = _user.Password;

                //1.Validar en SQLite si ya esta firmado el usaurio
                //2.Validar credenciales en la api
                //3.Guardar en SQLite
                Database.Model.Person person;

                DtoUser user = null;

                using (UserDialogs.Instance.Loading("Validando credenciales..."))
                {
                    user = await ApiServices.SecurityService.SignIn(new DtoLogin { Username = _user.Username, Password = _user.Password });
                    person = new PersonController().GetAll().Result.Where(x => x.Id == user.UserID).SingleOrDefault();

                    if (user == null)
                    {
                        throw new Exception("Usuario o contraseña incorrectos.");
                    }
                    else if (person == null)
                    {
                        person = new Database.Model.Person
                        {
                            Id = user.UserID,
                            ClaveUsuario = _user.Username,
                            IdTipoUsuario = user.UserTypeID,
                            Token = user.Token
                        };

                        await (new PersonController()).Insert(person);
                    }
                }

                SaveUserInformation(user);
                NavigateToPageCurrent(new ShellPage());
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
