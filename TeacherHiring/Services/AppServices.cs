using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHiring.Services
{
    public class AppServices
    {
        private string _urlAPI = string.Empty;
        public AppServices(string urlAPI)
        {
            _urlAPI = urlAPI;
        }

        private SecurityServices _securityService;
        public SecurityServices SecurityService
        {
            get
            {
                if (_securityService == null)
                {
                    _securityService = new SecurityServices(_urlAPI);
                }

                return _securityService;
            }
        }

        private TeacherServices _teacherServices;
        public TeacherServices TeacherServices
        {
            get
            {
                if (_teacherServices == null)
                {
                    _teacherServices = new TeacherServices(_urlAPI);
                }

                return _teacherServices;
            }
        }

        private StudentServices _studentService;
        public StudentServices StudentServices
        {
            get
            {
                if (_studentService == null)
                {
                    _studentService = new StudentServices(_urlAPI);
                }

                return _studentService;
            }
        }
    }
}
