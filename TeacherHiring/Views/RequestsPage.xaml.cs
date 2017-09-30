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
    public partial class RequestsPage : ContentPage
    {
        public RequestsViewModel viewModel;
        public RequestsPage()
        {
            Title = "Materias disponibles";
            InitializeComponent();
            BindingContext = viewModel = new RequestsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DtoClassAvailable;
            if (item == null)
                return;

            var teacher = App.LoggedUser;

            var bookClass = new DtoNewClass
            {
                ClassID = item.ClassID,
                Name = item.Name,
                TeacherID = teacher.UserID,
                TeacherName = teacher.Name,
                AvailableDate = DateTime.Now,
                Time = DateTime.Now.TimeOfDay,
                Token = teacher.Token
            };

            await viewModel.MasterNavigateTo(new TeacherSchedulePage(bookClass));

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