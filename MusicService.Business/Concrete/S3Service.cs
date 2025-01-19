using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using MusicService.Business.Abstract;

namespace MusicService.Business.Concrete
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

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
            var response = await _s3Client.PutObjectAsync(putRequest);
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
