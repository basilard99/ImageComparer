#region [-- IMPORTS --]

using System;
using System.Collections.ObjectModel; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// A class that contains the information about a source image.
    /// </summary>
    public class SourceImageFile
    {

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>A <see cref="String"/> containing the description.</value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>A <see cref="String"/> containing the path.</value>
        public String Path { get; set; }

        /// <summary>
        /// Gets or sets the matching images.
        /// </summary>
        /// <value>An <see cref="ObservableCollection{ImageFileTracker}"/> that contains the matching images.</value>
        public ObservableCollection<ImageFileTracker> MatchingImages { get; set; }

    }
}
