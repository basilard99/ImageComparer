using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using Rhino.Mocks;

using Syndic.ImageComparer.TestUtilities;

using Xunit;

namespace Syndic.ImageComparer.ViewModel.Tests
{
    
    public class ExportToListCommandTests
    {

        [Fact]
        public void InvokingExecuteWithNullParameterViolatesConstraint()
        {
            ExportToListCommand sut = new ExportToListCommand();
            XUnitHelper.ViolatesConstraint(() => sut.Execute(null), "parameter != null");
        }

        [Fact]
        public void InvokingExecuteWithNonListParameterViolatesConstraint()
        {
            ExportToListCommand sut = new ExportToListCommand();
            XUnitHelper.ViolatesConstraint(() => sut.Execute(5), "parameter is List<SourceImageFile>");
        }

        [Fact]
        public void InvokingCanExecuteWithNullParameterReturnsFalse()
        {
            ExportToListCommand sut = new ExportToListCommand();
            Assert.False(sut.CanExecute(null));
        }

        [Fact]
        public void CanExecuteReturnsFalseIfDisplayListIsEmpty()
        {
            ExportToListCommand sut = new ExportToListCommand();
            Assert.False(sut.CanExecute(new List<SourceImageFile>()));
        }

        [Fact]
        public void CanExecuteReturnsTrueIfDisplayListIsNotEmpty()
        {
            ExportToListCommand sut = new ExportToListCommand();
            Assert.True(sut.CanExecute(new List<SourceImageFile> { new SourceImageFile() }));
        }

        [Fact]
        public void AppropriateFileIsCreated()
        {

            List<SourceImageFile> testData = new List<SourceImageFile>
                {
                    new SourceImageFile
                        {
                            Description = "TestSource1",
                            Path = "TestSourcePath1",
                            MatchingImages =
                                new ObservableCollection<ImageFileTracker>
                                    {
                                        new ImageFileTracker
                                            {
                                                Path = "TestTrackerPath11",
                                                Description = "TestTracker11",
                                                ShouldDelete = true
                                            },
                                        new ImageFileTracker
                                            {
                                                Path = "TestTrackerPath12",
                                                Description = "TestTracker12",
                                                ShouldDelete = false
                                            },
                                        new ImageFileTracker
                                            {
                                                Path = "TestTrackerPath13",
                                                Description = "TestTracker13",
                                                ShouldDelete = true
                                            }
                                    }
                        },
                    new SourceImageFile
                        {
                            Description = "TestSource2",
                            Path = "TestSourcePath2",
                            MatchingImages =
                                new ObservableCollection<ImageFileTracker>
                                    {
                                        new ImageFileTracker
                                            {
                                                Path = "TestTrackerPath21",
                                                Description = "TestTracker21",
                                                ShouldDelete = true
                                            }
                                    }
                        }
                };;

            ExportToListCommand sut = new ExportToListCommand();
            sut.Execute(testData);

            TextReader reader = File.OpenText("DeleteMatchingImages.bat");
            Assert.Equal("del TestTrackerPath11", reader.ReadLine());
            Assert.Equal("del TestTrackerPath13", reader.ReadLine());
            Assert.Equal("del TestTrackerPath21", reader.ReadLine());
            Assert.Equal(string.Empty, reader.ReadToEnd());
            reader.Close();

        }

    }

}
