using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using Rhino.Mocks;

using Syndic.ImageComparer.Services;
using Syndic.ImageComparer.TestUtilities;

using Xunit;

namespace Syndic.ImageComparer.ViewModel.Tests
{

    public class BeginScanCommandTests
    {

        [Fact]
        public void CreatingCommandWithNullComparisonServiceViolatesConstraint()
        {
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();
            XUnitHelper.ViolatesConstraint(() => new BeginScanCommand(null, mockMainWindowViewModel), "imageComparisonService != null");
        }

        [Fact]
        public void CreatingCommandWithNullViewModelViolatesConstraint()
        {
            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            XUnitHelper.ViolatesConstraint(() => new BeginScanCommand(mockImageComparison, null), "mainWindowViewModel != null");
        }

        [Fact]
        public void InvokingExecuteWithNullParameterViolatesConstraint()
        {

            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();

            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);
            XUnitHelper.ViolatesConstraint(() => sut.Execute(null), "parameter != null");
            
        }

        [Fact]
        public void InvokingExecuteWithNonStringParameterViolatesConstraint()
        {

            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();

            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);
            XUnitHelper.ViolatesConstraint(() => sut.Execute(10), "parameter is String");

        }

        [Fact]
        public void InvokingExecuteCallsComparisonServiceWithJpgFilesFromSuppliedPath()
        {

            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();

            mockImageComparison
                .Expect(mock => mock
                    .Compare(Arg<ReadOnlyCollection<FileInfo>>
                        .Matches(coll => 
                            (coll.Count == 7) && 
                            (coll[0].Name == "TestFileA.jpg")
                        )
                    )
                )
                .Return(null);

            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);

            sut.Execute(Path.GetFullPath(@".\TestImages"));
            
            mockImageComparison.VerifyAllExpectations();

        }

        [Fact]
        public void InvokingExecuteSetsResultValueOnViewModel()
        {
            
            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            mockImageComparison.Expect(
                mock => mock.Compare(
                    Arg<ReadOnlyCollection<FileInfo>>.Matches(coll => (coll.Count == 7) && (coll[0].Name == "TestFileA.jpg"))))
                .Return(
                    new Dictionary<FileInfo, ReadOnlyCollection<FileInfo>>
                    {
                        {
                            new FileInfo(@".\TestImages\TestFileA.jpg"),
                            new ReadOnlyCollection<FileInfo>(
                                new List<FileInfo> { new FileInfo(@".\TestImages\TestFileA.jpg") }
                            )
                        }
                    }
                );
            
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();
            
            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);

            sut.Execute(Path.GetFullPath(@".\TestImages"));

            mockMainWindowViewModel.AssertWasCalled(
                vm => vm.DisplayList = Arg<List<SourceImageFile>>.Matches(list => list.Count == 1));
            
        }

        [Fact]
        public void CanExecuteReturnsTrueIfDisplayListIsEmpty()
        {

            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();
            mockMainWindowViewModel.Expect(a => a.DisplayList).Return(new List<SourceImageFile>());

            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);
            Assert.True(sut.CanExecute(null));

        }

        [Fact]
        public void CanExecuteReturnsFalseIfDisplayListIsNotEmpty()
        {

            IImageComparison mockImageComparison = MockRepository.GenerateMock<IImageComparison>();
            IMainWindowViewModel mockMainWindowViewModel = MockRepository.GenerateMock<IMainWindowViewModel>();
            mockMainWindowViewModel.Expect(a => a.DisplayList).Return(new List<SourceImageFile> { new SourceImageFile() });

            BeginScanCommand sut = new BeginScanCommand(mockImageComparison, mockMainWindowViewModel);
            Assert.False(sut.CanExecute(null));

        }

    }

}
