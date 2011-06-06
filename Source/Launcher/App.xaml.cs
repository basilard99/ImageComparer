#region [-- IMPORTS --]

#endregion IMPORTS

using Syndic.ImageComparer.Services;
using Syndic.ImageComparer.ViewModel;

namespace Syndic.ImageComparer.Launcher
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            ImageComparison comparison = new ImageComparison();
            BeginScanCommand beginScanCommand = new BeginScanCommand(comparison, mainWindowViewModel);
            ExportToListCommand exportToListCommand = new ExportToListCommand();

            mainWindowViewModel.BeginScanCommand = beginScanCommand;
            mainWindowViewModel.ExportToListCommand = exportToListCommand;

            this.MainWindow = new MainWindow { DataContext = mainWindowViewModel };
            this.MainWindow.Show();

        }

    }

}
