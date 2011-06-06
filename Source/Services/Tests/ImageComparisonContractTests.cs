using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using Syndic.ImageComparer.TestUtilities;

using Xunit;

namespace Syndic.ImageComparer.Services.Tests
{

    public class ImageComparisonContractTests
    {

        [Fact]
        public void NullSourceListViolatesContract()
        {
            
            TestImplementation sut = new TestImplementation();
            XUnitHelper.ViolatesConstraint(() => sut.Compare(null), "imageList != null");

        }

        [Fact]
        public void EmptySourceListViolatesContract()
        {

            TestImplementation sut = new TestImplementation();
            XUnitHelper.ViolatesConstraint(() => sut.Compare(new ReadOnlyCollection<FileInfo>(new List<FileInfo>())), "imageList.Count > 0");

        }

        private class TestImplementation : IImageComparison
        {
            public Dictionary<FileInfo, ReadOnlyCollection<FileInfo>> Compare(ReadOnlyCollection<FileInfo> imageList)
            {
                return default(Dictionary<FileInfo, ReadOnlyCollection<FileInfo>>);
            }
        }
    }

}
