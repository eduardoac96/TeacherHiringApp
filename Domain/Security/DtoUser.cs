using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class DtoUser
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int UserTypeID { get; set; }
        public string UserTypeDescription
        {
            get
            {
                string userTypeDescription = "Desconocido";

                switch (UserTypeID)
                {
                    case 1:
                        userTypeDescription = "Profesor";
                        break;
                    case 2:
                        userTypeDescription = "Alumno";
                        break;
                    default:
                        break;
                }

                return userTypeDescription;
            }
        }
        public string Token
        {
            get;set;
        }

        public DateTime TokenExpirationDate
        {
            get; set;
        }
    }
}
