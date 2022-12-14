using Diary.Commands;
using Diary.Models;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        Repository _repository = new Repository();
        public MainViewModel()
        {
                //using (var context = new ApplicationDbContext())
                //{
                //    var students = context.Students.ToList();
                //}


            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);

            RefreshDiary();

            InitGroups();
        }


        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent 
        {
            get
            {
                return _selectedStudent;
            }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        public StudentWrapper Student
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }



        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy napewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;
            //usuwnaie ucznia z bazy danych

            RefreshDiary(); 
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentView = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentView.Closed += AddEditStudentView_Closed;
            addEditStudentView.ShowDialog();
        }

        private void AddEditStudentView_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
            {
                new StudentWrapper{FirstName = "Kazimierz", LastName="Kowalski", Group = new GroupWrapper{ Id= 1} },
                new StudentWrapper{FirstName = "Marta", LastName="Nowak", Group = new GroupWrapper{ Id= 2} },
                new StudentWrapper{FirstName = "Zuzanna", LastName="Kmieć", Group = new GroupWrapper{ Id= 2} },
                new StudentWrapper{FirstName = "Adam", LastName="Paproch", Group = new GroupWrapper{ Id= 1} }
            };
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            _groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            //Groups = new ObservableCollection<GroupWrapper>
            //{
            //    new GroupWrapper{ Id= 0 , Name = "Wszystkie"},
            //    new GroupWrapper{ Id= 1 , Name = "Klasa 1"},
            //    new GroupWrapper{ Id= 2 , Name = "Klasa 2"},
            //    new GroupWrapper{ Id= 3 , Name = "Klasa 3"},
            //    new GroupWrapper{ Id= 4 , Name = "Klasa 4"}
            //};

            SelectedGroupId = 0;
        }

    }
}
