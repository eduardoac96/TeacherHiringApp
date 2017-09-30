using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Teacher
{
    public class DtoNewClass
    {
        public int ScheduleID { get; set; }
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string Name { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan Time { get; set; }
        public string CurrentLatitude { get; set; }
        public string CurrentLongitude { get; set; }

        public string Token { get; set; }
    }
}
