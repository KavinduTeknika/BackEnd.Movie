using System;

namespace BackEnd.Movie.AzureBlob
{
    public class AzureBlobSetings
    {
        public AzureBlobSetings(string storageAccount, 
                                       string storageKey,
                                       string containerName,string developmentConnectionString)
        {
            
            DevelopmentConnectionString = developmentConnectionString;                          
            StorageAccount = storageAccount;
            StorageKey = storageKey;
            ContainerName = containerName;
        }

        public string StorageAccount { get; set; }
        public string StorageKey { get; set; }
        public string ContainerName { get; set; }       
        public string DevelopmentConnectionString { get; set; }

    }
}
