using Diary.Models;
using Diary.Models.Wrappers;
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for AddEditStudentView.xaml
    /// </summary>
    public partial class AddEditStudentView : MetroWindow
    {
        public AddEditStudentView(StudentWrapper student = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }
    }
}
