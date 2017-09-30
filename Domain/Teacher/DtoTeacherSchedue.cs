    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Teacher
{
public     class DtoTeacherSchedule
    {
        public int ScheduleID { get; set; }
        public int TeacherID { get; set; }

        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public DateTime AvailableDate { get; set; }

      
        public string Time { get
            {
                return AvailableDate.ToString("hh:mm tt");
            }
        }

        public string Description
        {
            get
            {

                return TeacherName + " - " + AvailableDate.ToString();
            }
            set { }
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Token { get; set; }
    }
}
