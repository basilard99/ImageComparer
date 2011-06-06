#region [-- IMPORTS --]

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq; 

#endregion IMPORTS

namespace Syndic.ImageComparer.Services
{

    /// <summary>
    /// Provides an implementation of the <see cref="IImageComparison"/> service.
    /// </summary>
    public class ImageComparison : IImageComparison
    {

        #region [-- IMPLEMENTED INTERFACES --]

        #region [-- IImageComparison Implementation --]

        /// <summary>
        /// Compares the specified image list to itself.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <returns>
        /// A list of images and the potential matches.
        /// </returns>
        public Dictionary<FileInfo, ReadOnlyCollection<FileInfo>> Compare(ReadOnlyCollection<FileInfo> imageList)
        {

            // Create the dictionary
            return (imageList
                .Where(f => f.Extension == ".jpg")
                .GroupBy(f => f.Length)
                .Where(g => g.Count() > 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => grouping
                    .OrderBy(fileInfo => fileInfo.Name)
                    .ToList()
                )
            ).ToDictionary(
                sortedGrouping => sortedGrouping.First(),
                sortedGrouping => new ReadOnlyCollection<FileInfo>(
                    doByteComparison(
                        sortedGrouping.First(),
                        sortedGrouping.Skip(1)
                        .ToList()
                    )
                )
            );

        }  

        #endregion IImageComparison Implementation

        #endregion IMPLEMENTED INTERFACES

        #region [-- PRIVATE METHODS --]

        /// <summary>
        /// Compares the files byte by byte.
        /// </summary>
        private List<FileInfo> doByteComparison(FileInfo source, IEnumerable<FileInfo> toList)
        {

            List<FileInfo> matches = new List<FileInfo>();

            Byte[] sourceBytes = readBytesFromSourceFile(source);


            foreach (FileInfo fileInfo in toList)
            {

                Int32 counter = 0;
                Boolean bytesMatch = true;
                FileStream comparisonStream = fileInfo.OpenRead();

                do
                {
                    bytesMatch = sourceBytes[counter++] == comparisonStream.ReadByte();
                }
                while ((bytesMatch) && (counter < sourceBytes.Length));

                comparisonStream.Close();

                if (bytesMatch) matches.Add(fileInfo);

            }

            return matches;

        }

        /// <summary>
        /// Reads the bytes from source file.
        /// </summary>
        private Byte[] readBytesFromSourceFile(FileInfo source)
        {

            Byte[] sourceBytes = new byte[source.Length];

            FileStream fileStream = source.OpenRead();
            fileStream.Read(sourceBytes, 0, (int)source.Length);
            fileStream.Close();

            return sourceBytes;

        } 

        #endregion PRIVATE METHODS
    
    }

}
