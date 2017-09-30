using Acr.UserDialogs;
using Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TeacherHiring.ViewModel
{
    public class TeacherAssignmentsViewModel:BaseViewModel
    {
        private ObservableCollection<DtoTeacherSchedule> _items;
        private DtoNewClass _selectedClass;

        public ObservableCollection<DtoTeacherSchedule> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoadItemsCommand { get; set; }

        public TeacherAssignmentsViewModel(DtoNewClass selectedClass)
        {
            _selectedClass = selectedClass;

            Items = new ObservableCollection<DtoTeacherSchedule>();
            LoadItemsCommand = new Command(async () => await GetItemsDataSource());
        }

        async Task GetItemsDataSource()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (UserDialogs.Instance.Loading("Obteniendo horarios disponibles..."))
                {
                    Items.Clear();
                    Items = new ObservableCollection<DtoTeacherSchedule>(await ApiServices.TeacherServices.GetScheduleClass(_selectedClass));
                }
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync("No se pudo obtener la lista de horarios", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
