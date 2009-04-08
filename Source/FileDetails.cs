using System;
using System.Collections.Generic;
using System.Text;

namespace IncrementalOpener
{
    public class FileDetails
    {
        private string _fileName;
        private string _path;
        private string _project;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        public string Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
            }
        }
    }
}
