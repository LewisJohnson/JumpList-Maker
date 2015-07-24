using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

namespace JumpListMaker
{
    /// <summary>
    /// Interaction logic for RemoveTask.xaml
    /// </summary>
    public partial class RemoveTask : Window
    {
        public RemoveTask()
        {
            InitializeComponent();
        }

        private void RemoveTaskListLoaded(object sender, RoutedEventArgs e)
        {
            var list = sender as ListView;
            if (MainWindow.CurrentJumpList != null)
            {
                list.ItemsSource = MainWindow.CurrentJumpList.JumpItems;
            }
        }

        private void RemoveTask_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CurrentJumpList != null)
            {
                TaskList.ItemsSource = MainWindow.CurrentJumpList.JumpItems;
            }
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItemIndexes =
                (from object o in TaskList.SelectedItems select TaskList.Items.IndexOf(o)).ToList();


            foreach (var item in selectedItemIndexes)
            {
                JumpList.GetJumpList(Application.Current).JumpItems.RemoveAt(item);
            }
        }
    }
}
