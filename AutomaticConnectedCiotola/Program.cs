

namespace AutomaticConnectedCiotola
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.IO;
    using System.Threading.Tasks;


    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Azure Blob storage");
            Console.WriteLine();
            ProcessAsync().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit the sample application.");
            Console.ReadLine();
        }

        private static async Task ProcessAsync()
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;
            string destinationFile = null;

            string storageConnectionString = ("DefaultEndpointsProtocol=https;AccountName=containeraccount;AccountKey=cu+ZYEpjk+r+gKM+Tr/srgXIuA7Q0t0m4VBm7OD3vgeRD1RTb0StYMZ8zto1nCY9vSFPjwKdu06HhqSvFNsm4A==;EndpointSuffix=core.windows.net");


            // Mandi il parse in una variabile null di tipo CloudStorageAccount
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    //creo un cloudBlob per avere l'endpoint dello storage
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    // Create a container called 'quickstartblobs'  
                    cloudBlobContainer = cloudBlobClient.GetContainerReference("petimages");

                    // Create a file in your local MyDocuments folder to upload to a blob.
                    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localFileName = "image.png";
                    sourceFile = Path.Combine(localPath, localFileName);

                    // Get a reference to the blob address, then upload the file to the blob.
                    // Use the value of localFileName for the blob name.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    await cloudBlockBlob.UploadFromFileAsync(sourceFile);
                    BlobContinuationToken blobContinuationToken = null;
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                }
                finally
                {

                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
            }
        }
    }
}
