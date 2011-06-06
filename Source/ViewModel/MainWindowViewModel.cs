#region [-- IMPORTS --]

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// Implements an <see cref="IMainWindoViewModel"/>
    /// </summary>
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {

        #region [-- CONSTRUCTORS --]

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.DisplayList = new List<SourceImageFile>();
        } 

        #endregion CONSTRUCTORS

        #region [-- INTERFACE IMPLEMENTATIONS --]

        #region [-- IMainWindowViewModel --]

        /// <summary>
        /// Gets or sets the command that executes when begin scan is triggered.
        /// </summary>
        /// <value>An <see cref="ICommand"/> that defines the execution.</value>
        public ICommand BeginScanCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that executes when export is triggered.
        /// </summary>
        /// <value>An <see cref="ICommand"/> that defines the execution.</value>
        public ICommand ExportToListCommand { get; set; }

        /// <summary>
        /// Gets or sets the list of duplicate images.
        /// </summary>
        /// <value>An <see cref="Dictionary{FileInfo, ReadOnlyCollection{FileInfo}}"/> that contains the data.</value>
        public List<SourceImageFile> DisplayList
        {
            get
            {
                return _displayList;
            }
            set
            {
                _displayList = value;
                this.OnPropertyChanged("DisplayList");
            }
        }

        #endregion IMainWindowViewModel

        #region [-- INotifyPropertyChanged --]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged

        #endregion INTERFACE IMPLEMENTATIONS

        #region [-- PROTECTED METHODS --]

        /// <summary>
        /// Raise the NotifyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 

        #endregion PROTECTED METHODS

        #region [-- FIELDS --]

        private List<SourceImageFile> _displayList; 

        #endregion FIELDS

    }

}
