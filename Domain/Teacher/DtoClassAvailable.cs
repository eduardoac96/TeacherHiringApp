using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Teacher
{
    public class DtoClassAvailable
    {
        public int ClassID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public string AvailableDescription
        {
            get
            {
                return "Asesorías disponibles: " + Quantity.ToString();
            }
        }
    }
}
