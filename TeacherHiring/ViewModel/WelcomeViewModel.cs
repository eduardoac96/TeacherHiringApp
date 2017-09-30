using Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring.ViewModel
{
    public class WelcomeViewModel : BaseViewModel
    {
        private DtoWelcomePage _welcomeModel;
        public DtoWelcomePage WelcomeModel
        {
            get
            {
                return _welcomeModel;
            }
            set
            {
                _welcomeModel = value;
                RaisePropertyChanged();
            }
        }
        public WelcomeViewModel()
        {
            WelcomeModel = new DtoWelcomePage {
                 Name = App.LoggedUser.Name,
                 UserTypeID = App.LoggedUser.UserTypeID
            };
        }
    }
}
