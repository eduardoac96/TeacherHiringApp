using Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassDetailPage : ContentPage
    {

        public DtoTeacherSchedule Schedule
        {
            get;set;
        }
        public ClassDetailPage(DtoTeacherSchedule schedule)
        {
            InitializeComponent();
            Title = "Detalle materia";
            BindingContext = Schedule = schedule;
            setMapLocation();
        }

        private  void setMapLocation()
        {
            Pin pinLocation = new Pin
            {
                Position = new Position(Schedule.Latitude, Schedule.Longitude),
                Type = PinType.Place,
                Label = Schedule.ClassName
            };

            MyMap.Pins.Add(pinLocation);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Schedule.Latitude, Schedule.Longitude), Distance.FromMiles(1)));

        }
    }
}