#region [-- IMPORTS --]

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input; 

#endregion IMPORTS

namespace Syndic.ImageComparer.ViewModel
{

    /// <summary>
    /// Defines the command for exporting a file comparison scan to a file.
    /// </summary>
    public class ExportToListCommand : ICommand
    {
        
        #region [-- INTERFACE IMPLEMENTATIONS --]

        #region [-- ICommand Implementation --]

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(Object parameter)
        {

            Contract.Requires(parameter != null);
            Contract.Requires(parameter is List<SourceImageFile>);

            List<SourceImageFile> fileList = (List<SourceImageFile>)parameter;
            StringBuilder sb = new StringBuilder();

            fileList.ForEach(
                sourceFile => sourceFile
                    .MatchingImages
                    .Where(match => match.ShouldDelete)
                    .ToList()
                    .ForEach(match =>
                        {
                            sb.AppendFormat(CultureInfo.CurrentUICulture, "del \"{0}\"", match.Path);
                            sb.AppendLine();
                        }
                )
            );

            StreamWriter writer = new StreamWriter("DeleteMatchingImages.bat");
            writer.Write(sb);
            writer.Close();

        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(Object parameter)
        {

            return true;

            if (parameter == null) return false;

            List<SourceImageFile> fileList = (List<SourceImageFile>)parameter;
            return (fileList.Count > 0);

        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion ICommand Implementation

        #endregion INTERFACE IMPLEMENTATIONS
    
    }

}
