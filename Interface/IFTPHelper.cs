using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPHelper.Interface
{
    public interface IFTPHelper
    {
        /// <summary>
        /// Create a direcotry
        /// </summary>
        /// <param name="directoryPath"></param>
        void CreateDirectory(string directoryPath);

        /// <summary>
        /// Delete a directory
        /// </summary>
        /// <param name="directoryPath"></param>
        void DeleteDirectory(string directoryPath);

        /// <summary>
        /// Upload a file to FTP server
        /// </summary>
        /// <param name="localFullPath"></param>
        void UploadFile(string localFullPath);

        /// <summary>
        /// if overwrite the exsiting file.
        /// </summary>
        /// <param name="localFullPath"></param>
        /// <param name="overWriteFile"></param>
        void UploadFile(string localFullPath, bool overWriteFile);

        /// <summary>
        /// Upload files as byte array
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="remoteFileName"></param>
        void UploadFile(byte[] fileBytes, string remoteFileName);
    }
}
