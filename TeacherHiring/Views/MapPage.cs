using System;
using Xamarin.Forms.Maps;

using Xamarin.Forms;

namespace TeacherHiring
{
	public class MapPage : ContentPage
	{
		public MapPage(double Longitud, double Latitud)
		{
			Title = "Mapa";
			var map = new Map(
			MapSpan.FromCenterAndRadius(
					new Position(Latitud,Longitud), Distance.FromMiles(0.3)))
			{
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			map.MapType = MapType.Satellite;
			var stack = new StackLayout { Spacing = 0 };
			stack.Children.Add(map);
			Content = stack;
		}
	}
}

