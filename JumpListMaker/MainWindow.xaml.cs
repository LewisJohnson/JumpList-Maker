using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using Microsoft.Win32;

namespace JumpListMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        public static JumpList CurrentJumpList = new JumpList(); 
        private string _applicationName = string.Empty;
        private string _applicationPath = string.Empty;

        private void AddTask(object sender, RoutedEventArgs e)
        {
            if (_applicationName != string.Empty && _applicationPath != string.Empty)
            {
                const string desc = "Open {0}.";
                var jumpTask = new JumpTask
                {
                    ApplicationPath = _applicationPath,
                    IconResourcePath = _applicationPath,
                    Title = _applicationName,
                    Description = string.Format(desc, _applicationName)
                };

                CurrentJumpList.JumpItems.Add(jumpTask);
                CurrentJumpList.JumpItemsRemovedByUser += CurrentJumpListOnJumpItemsRemovedByUser;
                JumpList.SetJumpList(Application.Current, CurrentJumpList);
                FooterTextBlock.Text = $"Task {_applicationName} has been added.";
            }
            else
            {
                FooterTextBlock.Text = "Make sure the you have select an item and named it.";
            }
        }

        private void CurrentJumpListOnJumpItemsRemovedByUser(object sender, JumpItemsRemovedEventArgs jumpItemsRemovedEventArgs)
        {
           
        }

        private void RemoveAllTask(object sender, RoutedEventArgs e)
        {
            if (CurrentJumpList.JumpItems.Count > 0 && CurrentJumpList != null)
            {
                var jumpList = JumpList.GetJumpList(Application.Current);
                jumpList?.JumpItems.Clear();
                jumpList?.Apply();
                CurrentJumpList.JumpItems.Clear();
                FooterTextBlock.Text = "All tasks have been cleared.";
            }
            else
            {
                FooterTextBlock.Text = "Looks like there's nothing in the JumpList.";
            }

        }

        private void RemoveATask(object sender, RoutedEventArgs e)
        {
            if (CurrentJumpList.JumpItems.Count > 0 && CurrentJumpList != null)
            {
                var win = new RemoveTask();
                win.Show();
            }
            else
            {
                FooterTextBlock.Text = "Looks like there's nothing in the JumpList.";
            }

        }
        
        private void SelectTask(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".*",
                Filter = "Litreally Anything (.*)|*.*",
                Title = "Select an appliction for JumpList"

            };

            var result = dlg.ShowDialog();
            if (result == true)
            {
                _applicationPath = dlg.FileName;
            }
            else
            {
                FooterTextBlock.Text = "Oops, Something went wrong there. Please try again";
            }

            AppPath.Text = _applicationPath;
            AppName.Text = _applicationName;
        }

        private void AppPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            var r = sender as TextBox;
            if (r != null) _applicationPath = r.Text;
        }

        private void AppName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var r = sender as TextBox;
            if (r != null) _applicationName = r.Text;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (JumpList.GetJumpList(Application.Current) != null)
            {
                CurrentJumpList = JumpList.GetJumpList(Application.Current);
            }

        }
    }
}


