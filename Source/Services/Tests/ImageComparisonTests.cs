using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using Xunit;

namespace Syndic.ImageComparer.Services.Tests
{
  
    public class ImageComparisonTests
    {

        [Fact]
        public void NoMatchesReturnsEmptyDictionary()
        {
            
            ImageComparison c = new ImageComparison();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\TestImages");

            var result = c.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => !fi.Name.Contains("Copy")).ToList()));

            Assert.True(result.Keys.Count == 0);

        }

        [Fact]
        public void SingleMatchReturnsOneResultInDictionaryList()
        {

            ImageComparison c = new ImageComparison();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\TestImages");

            var result = c.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => fi.Name.StartsWith("TestFileB")).ToList()));

            Assert.Equal("TestFileB.jpg", result.Keys.ElementAt(0).Name);
            Assert.True(result[result.Keys.ElementAt(0)].Count == 1);
            Assert.Equal("TestFileBCopy1.jpg", result[result.Keys.ElementAt(0)].First().Name);
            
        }

        [Fact]
        public void NMatchesReturnsNMinusOneResultsInDictionaryList()
        {

            ImageComparison c = new ImageComparison();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\TestImages");

            var result = c.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => fi.Name.StartsWith("TestFileC")).ToList()));

            Assert.Equal("TestFileC.jpg", result.Keys.ElementAt(0).Name);
            Assert.True(result[result.Keys.ElementAt(0)].Count == 2);
            Assert.Equal("TestFileCCopy1.jpg", result[result.Keys.ElementAt(0)][0].Name);
            Assert.Equal("TestFileCCopy2.jpg", result[result.Keys.ElementAt(0)][1].Name);

        }

        [Fact]
        public void MultipleSourceMatches()
        {

            ImageComparison c = new ImageComparison();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\TestImages");

            var result = c.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => fi.Name.StartsWith("TestFile")).ToList()));

            Assert.Equal("TestFileB.jpg", result.Keys.ElementAt(0).Name);
            Assert.True(result[result.Keys.ElementAt(0)].Count == 1);
            Assert.Equal("TestFileBCopy1.jpg", result[result.Keys.ElementAt(0)].First().Name);

            Assert.Equal("TestFileC.jpg", result.Keys.ElementAt(1).Name);
            Assert.True(result[result.Keys.ElementAt(1)].Count == 2);
            Assert.Equal("TestFileCCopy1.jpg", result[result.Keys.ElementAt(1)][0].Name);
            Assert.Equal("TestFileCCopy2.jpg", result[result.Keys.ElementAt(1)][1].Name);

        }

        [Fact]
        public void NonJpgFilesAreIgnored()
        {

            ImageComparison c = new ImageComparison();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\TestImages");

            var result = c.Compare(new ReadOnlyCollection<FileInfo>(directoryInfo.GetFiles().Where(fi => fi.Name.StartsWith("TestFile")).ToList()));

            Assert.True(result.Keys.Count(f => f.Extension.EndsWith("gif")) == 0);

        }

    }

}
