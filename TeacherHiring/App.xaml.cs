using Domain.Security;
using Newtonsoft.Json;
using SQLite;
using System.Linq;
using TeacherHiring.Database.Model;
using Xamarin.Forms;

namespace TeacherHiring
{
	public partial class App : Application
	{
        public static DtoUser LoggedUser
        {
            get
            {
                DtoUser user = null;

                if (App.Current.Properties.ContainsKey("User"))
                {
                    user = JsonConvert.DeserializeObject<DtoUser>(App.Current.Properties["User"].ToString());
                }

                return user;
            }
        }

        public App()
		{
			InitializeComponent();
		}


		protected override void OnStart()
		{
            // Handle when your app starts

            

            if (LoggedUser == null)
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                MainPage = new ShellPage();
            }

        }

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
		static TeacherHiringDatabase database;
		public static TeacherHiringDatabase Database
		{
			get
			{
				if (database == null)
					database = new TeacherHiringDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TeacherHiring.db3"));
				return database;
			}
		}
		 
	}
}
