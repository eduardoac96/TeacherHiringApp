using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class DtoWelcomePage
    {
        public string Name
        {
            get;set;
        }
        public string WelcomeMessage
        {
            get
            {
                return "Bienvenido "+  Name ;
            }
        }

        public int UserTypeID
        {
            get;set;
        }
         
    }
}
