using Acr.UserDialogs;
using Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleRequestPage : ContentPage
    {
        ScheduleRequestViewModel _viewModel;
        private DtoTeacherSchedule _schedule;
        public ScheduleRequestPage(DtoTeacherSchedule schedule)
        {
            _schedule = schedule;
            Title = schedule.ClassName;
            InitializeComponent();
            BindingContext = _viewModel = new ScheduleRequestViewModel(schedule);
            setMapLocation();
        }

        private void setMapLocation()
        {
           

            Pin pinLocation = new Pin
            {
                Position = new Position(_schedule.Latitude, _schedule.Longitude),
                Type = PinType.Place,
                Label = _schedule.ClassName
            };

            MyMap.Pins.Add(pinLocation);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_schedule.Latitude, _schedule.Longitude), Distance.FromMiles(1)));
        }

    }
}