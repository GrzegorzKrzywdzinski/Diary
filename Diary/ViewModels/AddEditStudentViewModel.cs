using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class AddEditStudentViewModel : ViewModelBase    
    {
        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);

            ConfirmCommand = new RelayCommand(Confirm);

            if(student == null) 
            {
                Student = new StudentWrapper();
            }
            else
            {
                IsUpdate= true;
                Student = student;
            }

            InitGroups();
        }


        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set;}

        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set 
            { 
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
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

        private ObservableCollection<GroupWrapper> _groups;

        public ObservableCollection<GroupWrapper> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }




        private void Confirm(object obj)
        {
            if (IsUpdate)
                UpdateStudent();
            else
                AddStudent();

            CloseWindow(obj as Window);
        }

        private void AddStudent()
        {
            //baza danych
        }

        private void UpdateStudent()
        {
            //baza danych
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>
            {
                new GroupWrapper{ Id= 0 , Name = "-- brak --"},
                new GroupWrapper{ Id= 1 , Name = "Klasa 1"},
                new GroupWrapper{ Id= 2 , Name = "Klasa 2"},
                new GroupWrapper{ Id= 3 , Name = "Klasa 3"},
                new GroupWrapper{ Id= 4 , Name = "Klasa 4"}
            };

            Student.Group.Id = 0;
        }
    }
}
