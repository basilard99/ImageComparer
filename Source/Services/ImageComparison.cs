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

            Dictionary<FileInfo, ReadOnlyCollection<FileInfo>> results = new Dictionary<FileInfo, ReadOnlyCollection<FileInfo>>();

            var comparisonList = imageList.Where(f => f.Extension.ToLower() == ".jpg").GroupBy(f => f.Length).Where(g => g.Count() > 1);
            foreach (var grouping in comparisonList)
            {
                var images = grouping.Select(c => new ComparisonTracker { HasBeenCompared = false, Source = c }).ToArray();
                for (int index = 0; index < images.Count(); index++)
                {
                    if (images[index].HasBeenCompared) continue;
                    images[index].HasBeenCompared = true;
                    List<FileInfo> matches = doByteComparison(images[index].Source, images.Where(c => c.HasBeenCompared == false));
                    if (matches.Count() > 0)
                    {
                        results.Add(images[index].Source, new ReadOnlyCollection<FileInfo>(matches));
                    }
                }
            }

            return results;

        }  

        #endregion IImageComparison Implementation

        #endregion IMPLEMENTED INTERFACES

        #region [-- PRIVATE METHODS --]

        /// <summary>
        /// Compares the files byte by byte.
        /// </summary>
        private List<FileInfo> doByteComparison(FileInfo source, IEnumerable<ComparisonTracker> toList)
        {

            List<FileInfo> matches = new List<FileInfo>();

            Byte[] sourceBytes = readBytesFromSourceFile(source);

            foreach (ComparisonTracker tracker in toList)
            {

                Int32 counter = 0;
                Boolean bytesMatch = true;
                FileStream comparisonStream = tracker.Source.OpenRead();

                do
                {
                    bytesMatch = sourceBytes[counter++] == comparisonStream.ReadByte();
                }
                while ((bytesMatch) && (counter < sourceBytes.Length));

                comparisonStream.Close();

                if (bytesMatch)
                {
                    tracker.HasBeenCompared = true;
                    matches.Add(tracker.Source);
                }

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

        #region [-- PRIVATE FIELDS --]

        /// <summary>
        /// Private class used for tracking comparisons.
        /// </summary>
        private class ComparisonTracker
        {
            public FileInfo Source { get; set; }
            public Boolean HasBeenCompared { get; set; }
        } 

        #endregion PRIVATE FIELDS
    
    }

}
