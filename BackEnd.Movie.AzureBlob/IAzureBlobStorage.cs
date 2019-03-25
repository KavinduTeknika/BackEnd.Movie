using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackEnd.Movie.AzureBlob
{
    public interface IAzureBlobStorage
    {
        Task UploadTextAsync(string blobName, string content);        
        Task<string> DownloadAsTextAsync(string blobName);         
    }
}
