#region [-- IMPORTS --]

using System; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// A class that helps track an image.
    /// </summary>
    public class ImageFileTracker
    {

        #region [-- PROPERTIES --]

        /// <summary>
        /// Gets or sets the path to the image.
        /// </summary>
        /// <value>The <see cref="String"/> containing the path to the image.</value>
        public String Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image should be deleted or not.
        /// </summary>
        /// <value><c>true</c> if the image should be deleted; otherwise, <c>false</c>.</value>
        public Boolean ShouldDelete { get; set; }

        /// <summary>
        /// Gets or sets the image description.
        /// </summary>
        /// <value>A <see cref="String"/> containing the image description.</value>
        public String Description { get; set; } 

        #endregion PROPERTIES

    }

}
