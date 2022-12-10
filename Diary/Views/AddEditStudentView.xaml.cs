using Diary.Models;
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for AddEditStudentView.xaml
    /// </summary>
    public partial class AddEditStudentView : MetroWindow
    {
        public AddEditStudentView(Student student = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }
    }
}
