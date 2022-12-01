using ShoppingLikeFiles.DomainServices.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace ShoppingLikeFiles.DomainServices.Core
{
    class UploadService: IUploadService
    {
        private UploadServiceOptions options;
        private BlobServiceClient blobServiceClient;
        private string blobContainerName="caff_container";
        public UploadService(UploadServiceOptions options, string connectionString)
        {
            this.options = options;

            //create containerClient
            blobServiceClient = new BlobServiceClient(connectionString);
            
        }

        public async Task<string> UploadFileAsync(byte[] filecontent, string fileName)
        {
            
            try
            {
                string location = $"{options.DirectoryPath}/{fileName}";
                if (!this.options.ShouldUploadToAzure)
                {
                    using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        if(File.Exists(location))
                        {
                            throw new ArgumentException("File name already exists!");
                        }
                            
                        stream.Write(filecontent, 0, filecontent.Length);
                    }
                    return fileName;
                }
                else
                {
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                    BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(fileName);

                    if (blockBlobClient.Exists())
                    {
                        throw new ArgumentException("File name already exists!");
                    }

                    using (Stream stream = await blockBlobClient.OpenWriteAsync(true))
                    {
                        using (var fileStream = new MemoryStream(filecontent))
                        {
                            await stream.CopyToAsync(fileStream);   
                        }
                    }
                    return fileName;
                }
            }
            catch(IOException e)
            {
               Console.WriteLine("Error occured during file save: " + e.Message);
                return String.Empty;
            }
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
                        return false;
                    }
                }
                else
                {
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                    BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(fileLocation);
                    blockBlobClient.DeleteIfExists();
                }
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Error occured during file deletion: " + e.Message);
               
                return false;
            }

        }

        public string UploadFile(byte[] filecontent, string fileName)
        {
            try
            {
                string location = $"{options.DirectoryPath}/{fileName}";
                if (!this.options.ShouldUploadToAzure)
                {
                    using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        if (File.Exists(location))
                        {
                            throw new ArgumentException("File name already exists!");
                        }

                        stream.Write(filecontent, 0, filecontent.Length);
                    }
                    return fileName;
                }
                else
                {
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                    BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(fileName);

                    if (blockBlobClient.Exists())
                    {
                        throw new ArgumentException("File name already exists!");
                    }

                    using (Stream stream = blockBlobClient.OpenWrite(true))
                    {
                        using (var fileStream = new MemoryStream(filecontent))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                    return fileName;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error occured during file save: " + e.Message);
                return String.Empty;
            }
        }
    }
}
