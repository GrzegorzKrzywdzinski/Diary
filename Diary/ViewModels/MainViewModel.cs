using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
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

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent 
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

        public Student Student
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
            var addEditStudentView = new AddEditStudentView(obj as Student);
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
            Students = new ObservableCollection<Student>
            {
                new Student{FirstName = "Kazimierz", LastName="Kowalski", Group = new Group{ Id= 1} },
                new Student{FirstName = "Marta", LastName="Nowak", Group = new Group{ Id= 2} },
                new Student{FirstName = "Zuzanna", LastName="Kmieć", Group = new Group{ Id= 2} },
                new Student{FirstName = "Adam", LastName="Paproch", Group = new Group{ Id= 1} }
            };
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group{ Id= 0 , Name = "Wszystkie"},
                new Group{ Id= 1 , Name = "Klasa 1"},
                new Group{ Id= 2 , Name = "Klasa 2"},
                new Group{ Id= 3 , Name = "Klasa 3"},
                new Group{ Id= 4 , Name = "Klasa 4"}
            };

            SelectedGroupId = 0;
        }

    }
}
