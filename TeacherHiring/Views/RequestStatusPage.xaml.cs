using Domain.Student;
using Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestStatusPage : ContentPage
    {
        public RequestStatusViewModel viewModel;
        public RequestStatusPage()
        {
            Title = "Solicitudes realizadas";
            InitializeComponent();
            BindingContext = viewModel = new RequestStatusViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DtoRequestStatus;
            if (item == null)
                return;
            item.StudentID = App.LoggedUser.UserID;

            await viewModel.MasterNavigateTo(new ClassDetailPage(new DtoTeacherSchedule
            {
                ClassID = item.ClassID,
                ClassName = item.ClassName,
                AvailableDate = item.AvailableDate,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                TeacherID = item.TeacherID,
                TeacherName = item.TeacherName,
                StudentID = item.StudentID,
                Token = App.LoggedUser.Token
            }));

            // Manually deselect item
            ListItems.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}