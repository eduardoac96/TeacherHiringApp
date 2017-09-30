using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Student
{
    public class DtoRequestStatus
    {
        public int StudentScheduleID { get; set; }
        public int TeacherScheduleID { get; set; }
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        public int ClassID { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public DateTime AvailableDate { get; set; }

        public string ShortDate
        {
            get
            {
                return AvailableDate.ToString("dd/MM/yyyy");
            }
        }
        public string Time
        {
            get
            {
                return AvailableDate.ToString("hh:mm tt");
            }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsAccepted { get; set; }

        public string DescriptionLabel
        {
            get
            {
                return ClassName + " - " + TeacherName + " " + AvailableDate.ToString();
            }
        }

        public string PendingRequestsDescription
        {
            get
            {
                return ClassName + " - " + AvailableDate.ToString();
            }
        }
    }
}
