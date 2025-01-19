using Amazon.S3;
using Amazon.S3.Model;
using IdentityService.Business.Abstract;
using Microsoft.Extensions.Configuration;
using Amazon.Runtime;

namespace IdentityService.Business.Concrete
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        // Constructor accepting configuration for S3
        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            // Set up AWS credentials and region configuration
            var awsAccessKey = configuration["AWS:AccessKey"];
            var awsSecretKey = configuration["AWS:SecretKey"];
            var region = configuration["AWS:Region"];

            // Explicitly create AWS credentials and initialize the S3 client
            var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
            var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region); // Uses region code like "eu-north-1"

            _s3Client = new AmazonS3Client(credentials, regionEndpoint);
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> UploadFileAsync(string fileName, Stream fileStream, string contentType)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType,
            };

            // Upload the file to the S3 bucket
            var response = await _s3Client.PutObjectAsync(putRequest);

            // Return the URL to the uploaded file
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
