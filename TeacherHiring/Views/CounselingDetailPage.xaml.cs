using Domain.Student;
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
	public partial class CounselingDetailPage : ContentPage
	{
        private DtoRequestStatus _counseling;
		public CounselingDetailPage (DtoRequestStatus counseling)
		{
            Title = "Detalle asesoría";
			InitializeComponent ();
            BindingContext = _counseling = counseling;
            setMapLocation();
		}

        private void setMapLocation()
        {

            Pin pinLocation = new Pin
            {
                Position = new Position(_counseling.Latitude, _counseling.Longitude),
                Type = PinType.Place,
                Label = _counseling.ClassName
            };

            MyMap.Pins.Add(pinLocation);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_counseling.Latitude, _counseling.Longitude), Distance.FromMiles(1)));
        }
    }
}