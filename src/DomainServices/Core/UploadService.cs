using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Options;

namespace ShoppingLikeFiles.DomainServices.Core;

class UploadService : IUploadService
{
    private readonly UploadServiceOptions options;
    private readonly BlobServiceClient? blobServiceClient;
    private const string blobContainerName = "caff_container";
    //private readonly ILogger logger;

    public UploadService(IOptions<UploadServiceOptions> options) //ILogger<UploadService> logger,
    {
        //this.logger = logger;
        this.options = options.Value;

        //create containerClient
        if (this.options.ShouldUploadToAzure)
        {
            blobServiceClient = new BlobServiceClient(this.options.ConnectionString);
        }
    }

    public async Task<string> UploadFileAsync(byte[] filecontent, string fileName)
    {
        //logger.Verbose("Called {method}, with arguments: {fileName}", nameof(UploadFileAsync), fileName);
        try
        {
            string location = $"{options.DirectoryPath}{Path.DirectorySeparatorChar}{fileName}";
            if (!this.options.ShouldUploadToAzure)
            {
                if (File.Exists(location))
                {
                    throw new ArgumentException("File name already exists!");
                }
                using (var stream = new FileStream(location, FileMode.CreateNew, FileAccess.Write))
                {
                    if (!File.Exists(location))
                    {
                        throw new ArgumentException("File could not be created!");
                    }

                    stream.Write(filecontent, 0, filecontent.Length);
                }
                return location;
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
                    using var fileStream = new MemoryStream(filecontent);
                    await stream.CopyToAsync(fileStream);
                }
                return fileName;
            }
        }
        catch (IOException e)
        {
            //logger.Error("Error occured during file save");
            //logger.Debug(e, "Error occured during file save with filename: {fileName}", fileName);
            return String.Empty;
        }
    }

    public async Task<bool> RemoveFileAsync(string fileLocation)
    {
        //logger.Verbose("Called {method}, with arguments: {fileName}", nameof(RemoveFileAsync), fileLocation);
        await Task.Yield();
        try
        {
            if (!this.options.ShouldUploadToAzure)
            {
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                    //logger.Information("File deleted");
                }
                else
                {
                    //logger.Information("File not found");
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
            //logger.Error("Error occured during file deletion");
            //logger.Debug(e, "Error occured during file deletion with filename: {fileName}", fileLocation);
            return false;
        }
    }

    public string UploadFile(byte[] filecontent, string fileName)
    {
        //logger.Verbose("Called {method}, with arguments: {fileName}", nameof(UploadFile), fileName);
        try
        {
            string location = $"{options.DirectoryPath}{Path.DirectorySeparatorChar}{fileName}";
            if (!this.options.ShouldUploadToAzure)
            {
                if (File.Exists(location))
                {
                    throw new ArgumentException("File name already exists!");
                }
                using (var stream = new FileStream(location, FileMode.CreateNew, FileAccess.Write))
                {
                    if (!File.Exists(location))
                    {
                        throw new ArgumentException("File could not be created!");
                    }

                    stream.Write(filecontent, 0, filecontent.Length);
                }
                return location;
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
                    using var fileStream = new MemoryStream(filecontent);
                    stream.CopyTo(fileStream);
                }
                return fileName;
            }
        }
        catch (IOException e)
        {
            //logger.Error("Error occured during file save");
            //logger.Debug(e, "Error occured during file save with filename: {fileName}", fileName);
            return string.Empty;
        }
    }
}
