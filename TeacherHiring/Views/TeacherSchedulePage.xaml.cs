using Domain.Teacher;
using TeacherHiring.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherHiring.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherSchedulePage : ContentPage
    {
        public TeacherAssignmentsViewModel viewModel;

        public TeacherSchedulePage(DtoNewClass selectedClass)
        {
            InitializeComponent();
            Title = "Horarios disponibles";
            BindingContext = viewModel = new TeacherAssignmentsViewModel(selectedClass);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DtoTeacherSchedule;
            if (item == null)
                return;
            item.StudentID = App.LoggedUser.UserID;

            await viewModel.MasterNavigateTo(new ScheduleRequestPage(item));

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