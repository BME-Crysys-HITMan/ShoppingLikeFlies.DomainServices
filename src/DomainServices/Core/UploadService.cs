using ShoppingLikeFiles.DomainServices.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShoppingLikeFiles.DomainServices.Core
{
    class UploadService: IUploadService
    {
        private UploadServiceOptions options;
        private BlobServiceClient blobServiceClient;
        private string blobContainerName="caff_container";
        public UploadService(UploadService options, string connectionString)
        {
            this.options = options;

            //create containerClient
            blobServiceClient = new BlobServiceClient(connectionString);
            
        }
        public async Task<string> UploadFileAsync(byte[] filecontent)
        {
            string filename =  Guid.NewGuid().ToString() + ".caff";
            string location = $"{options.DirectoryPath}/{fileName}";


            try
            {
                if (!this.options.ShouldUploadToAzure)
                {
                    while (File.Exists(location))
                    {
                        filename = Guid.NewGuid().ToString();
                        location = $"{options.DirectoryPath}/{fileName}";
                    }
                    stream.Write(bytes, 0, filecontent.Length);
                }
                else
                {
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                    BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(fileName);

                    while (blockBlobClient.exists())
                    {
                        filename = Guid.NewGuid().ToString();
                        blockBlobClient = containerClient.GetBlockBlobClient(fileName);
                    }

                    using (Stream stream = await blockBlobClient.OpenWriteAsync(true))
                    {
                        using (var fileStream = new MemoryStream(filecontent))
                        {
                            await stream.CopyToAsync(fileStream);   
                        }
                    }
                }
            }
            catch(IOException e)
            {
               Console.WriteLine("Error occured during file save: " + e.Message());
            }

            return location;
        }
        public async Task<bool> RemoveFileAsync(string fileLocation)
        {
            try
            {
                if (!this.options.ShouldUploadToAzure)
                {
                    if (File.Exists(fileLocation))
                    {
                        File.Delete(fileLocation);
                        Console.WriteLine("File deleted");
                    }
                    else
                    {
                        Console.WriteLine("File not found");
                    }
                }
                else
                {
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                    BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(fileName);
                    blockBlobClient.DeleteIfExists();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error occured during file deletion: " + e.Message());
            }

        }
    }
}
