#region [-- IMPORTS --]

#endregion IMPORTS

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace Syndic.ImageComparer.Launcher
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        #region [-- CONSTRUCTORS --]

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        private void Image_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
			this.Close();
        }

        /// <summary>
        /// Launch a dialog to browse for a folder.
        /// </summary>
        private void browseForFolderButton_Click(Object sender, RoutedEventArgs e)
        {

            FolderBrowserDialog browserDialog = new FolderBrowserDialog
                {
                    Description = "Browse to image folder",
                    RootFolder = Environment.SpecialFolder.MyDocuments,
                    ShowNewFolderButton = false
                };

            if (browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImageFolderPathTextBox.Text = browserDialog.SelectedPath;
            }

        } 

        #endregion CONSTRUCTORS

    }

}
