using FluentFTP;
using System.Collections.Generic;
using System.IO;

namespace IgcRestApi.Services
{
    public interface IFtpService
    {
        public List<FtpListItem> GetFileList();

        public Stream DownloadFile(string remoteFileName);
    }
}