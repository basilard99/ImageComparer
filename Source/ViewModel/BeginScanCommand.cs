#region [-- IMPORTS --]

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;

using Syndic.ImageComparer.Services; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// Defines the command for beginning a file comparison scan.
    /// </summary>
    public class BeginScanCommand : ICommand
    {

        #region [-- CONSTRUCTORS --]

        /// <summary>
        /// Initializes a new instance of the <see cref="BeginScanCommand"/> class.
        /// </summary>
        /// <param name="imageComparisonService">The image comparison service to use.</param>
        public BeginScanCommand(IImageComparison imageComparisonService, IMainWindowViewModel mainWindowViewModel)
        {

            Contract.Requires(imageComparisonService != null);
            Contract.Requires(mainWindowViewModel != null);

            _imageComparison = imageComparisonService;
            _mainWindowViewModel = mainWindowViewModel;
        
        } 

        #endregion CONSTRUCTORS

        #region [-- IMPLMENTED INTERFACES --]
        
        #region [-- ICommand --]

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(Object parameter)
        {

            Contract.Requires(parameter != null);
            Contract.Requires(parameter is String);

            DirectoryInfo directoryInfo = new DirectoryInfo(parameter.ToString());
            
            var results = _imageComparison.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => fi.Extension.ToLower(CultureInfo.CurrentUICulture) == ".jpg").ToList()));
            if (results != null)
            {

                _mainWindowViewModel.DisplayList = results.Select(keyValuePair => new SourceImageFile
                    {
                        Description = keyValuePair.Key.Name, 
                        Path = keyValuePair.Key.FullName, 
                        MatchingImages = new ObservableCollection<ImageFileTracker>(keyValuePair.Value.Select(elem => new ImageFileTracker
                            {
                                Description = elem.Name, 
                                Path = elem.FullName, 
                                ShouldDelete = false
                            }
                        ))
                    }
                ).ToList();
            
            }

        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public Boolean CanExecute(Object parameter)
        {
            return _mainWindowViewModel.DisplayList.Count == 0;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion ICommand 

        #endregion IMPLMENTED INTERFACES
        
        #region [-- FIELDS --]

        private readonly IImageComparison _imageComparison;
        private readonly IMainWindowViewModel _mainWindowViewModel;

        #endregion FIELDS

    }

}
