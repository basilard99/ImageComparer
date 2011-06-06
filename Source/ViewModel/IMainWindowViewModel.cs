#region [-- IMPORTS --]

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// Defines the interface for a view model to the main window.
    /// </summary>
    public interface IMainWindowViewModel : IViewModel
    {

        /// <summary>
        /// Gets or sets the command that executes when begin scan is triggered.
        /// </summary>
        /// <value>An <see cref="ICommand"/> that defines the execution.</value>
        ICommand BeginScanCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that executes when export is triggered.
        /// </summary>
        /// <value>An <see cref="ICommand"/> that defines the execution.</value>
        ICommand ExportToListCommand { get; set; }

        /// <summary>
        /// Gets or sets the list of duplicate images.
        /// </summary>
        /// <value>A <see cref="List{SourceImageFile}"/> that contains the data.</value>
        List<SourceImageFile> DisplayList { get; set; }

    }

}