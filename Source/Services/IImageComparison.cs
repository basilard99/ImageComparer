#region [-- IMPORTS --]

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO; 

#endregion IMPORTS

namespace Syndic.ImageComparer.Services
{

    /// <summary>
    /// Defines an interface for performing image comparison.
    /// </summary>
    [ContractClass(typeof(ImageComparisonContracts))]
    public interface IImageComparison
    {

        /// <summary>
        /// Compares the specified image list to itself.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <returns>A list of images and the potential matches.</returns>
        Dictionary<FileInfo, ReadOnlyCollection<FileInfo>> Compare(ReadOnlyCollection<FileInfo> imageList);

    }

    [ContractClassFor(typeof(IImageComparison))]
    internal abstract class ImageComparisonContracts : IImageComparison
    {
        [DebuggerStepThrough]
        public Dictionary<FileInfo, ReadOnlyCollection<FileInfo>> Compare(ReadOnlyCollection<FileInfo> imageList)
        {
            Contract.Requires(imageList != null);
            Contract.Requires(imageList.Count > 0);
            return default(Dictionary<FileInfo, ReadOnlyCollection<FileInfo>>);
        }
    }

}