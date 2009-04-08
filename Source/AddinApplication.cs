using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using System.Windows.Forms;
using EnvDTE;
using System.IO;

namespace IncrementalOpener
{
    public class AddinApplication
    {
        public void Run(DTE2 _applicationObject)
        {
            List<FileDetails> fileDetails = new List<FileDetails>();

            /*
             * Create a list of all projects and files in the solution, so
             * that we can display them later on to the user. This is done
             * prior to showing the dialog.
             */

            Projects projects = _applicationObject.Solution.Projects;
            int projCount = _applicationObject.Solution.Projects.Count;
            
            for (int i = 1; i <= projects.Count; i++ )
            {
                Project project = projects.Item(i);

                CreateFileList(project.Name, project.ProjectItems, ref fileDetails);
            }

            /*
             * formIncrementalOpen is the dialog in which the user filters
             * the list of files.
             */
            formIncrementalOpen openDlg = new formIncrementalOpen();

            FileDetails resultFile = openDlg.Show(fileDetails);

            if (resultFile != null)
            {
                string fullPath = Path.Combine(resultFile.Path, resultFile.FileName);
                Window win = _applicationObject.OpenFile(EnvDTE.Constants.vsViewKindCode, fullPath);
                win.Visible = true;
                win.SetFocus();
            }
            
        }

        /// <summary>
        /// Generate a list of all files included in the Visual Studio solutions.
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectItems"></param>
        /// <param name="fileDetails"></param>
        private void CreateFileList(string projectName, ProjectItems projectItems, ref List<FileDetails> fileDetails)
        {
            foreach (ProjectItem projItem in projectItems)
            {
                if (projItem.ProjectItems != null && projItem.ProjectItems.Count > 0)
                {
                    // This is a sub project...
                    CreateFileList(projectName, projItem.ProjectItems, ref fileDetails);
                }

                if (projItem.Kind == Constants.vsProjectItemKindPhysicalFile)
                {
                    string entirePathAndFile = projItem.Properties.Item("FullPath").Value as string;

                    string fileName = Path.GetFileName(entirePathAndFile);
                    string path = Path.GetDirectoryName(entirePathAndFile);

                    FileDetails details = new FileDetails();

                    details.FileName = fileName;
                    details.Path = path;
                    details.Project = projectName;

                    fileDetails.Add(details);
                }
            }
            
        }

    }
}
