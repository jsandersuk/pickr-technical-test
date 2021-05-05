using System;

namespace PickrTechnicalTest
{
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Amazon.S3;
    using Amazon.S3.Model;

    public class S3AnnualSalaryTaxBandRepository : IAnnualSalaryTaxBandRepository
    {
        private readonly AmazonS3Client amazonS3Client;
        private readonly string annualSalaryTaxBandsS3BucketName;
        private readonly string annualSalaryTaxBandsS3Key;

        private bool isAnnualSalaryTaxBandsLoaded;
        private AnnualSalaryTaxBand[] annualSalaryTaxBands;

        public S3AnnualSalaryTaxBandRepository(
            AmazonS3Client amazonS3Client,
            string annualSalaryTaxBandsS3BucketName,
            string annualSalaryTaxBandsS3Key)
        {
            this.amazonS3Client = amazonS3Client;
            this.annualSalaryTaxBandsS3BucketName = annualSalaryTaxBandsS3BucketName;
            this.annualSalaryTaxBandsS3Key = annualSalaryTaxBandsS3Key;
        }

        public AnnualSalaryTaxBand[] GetAllBands()
        {
            if (this.isAnnualSalaryTaxBandsLoaded)
            {
                return this.annualSalaryTaxBands;
            }

            this.annualSalaryTaxBands = this.LoadAnnualSalaryTaxBandsFromS3();

            this.isAnnualSalaryTaxBandsLoaded = true;

            return this.annualSalaryTaxBands;
        }

        private AnnualSalaryTaxBand[] LoadAnnualSalaryTaxBandsFromS3()
        {
            try
            {
                var s3GetObjectRequest = new GetObjectRequest()
                {
                    BucketName = this.annualSalaryTaxBandsS3BucketName,
                    Key = this.annualSalaryTaxBandsS3Key,
                };

                string fileContents;
                using (var getObjectResponse = this.amazonS3Client.GetObjectAsync(s3GetObjectRequest).Result)
                {
                    StreamReader reader = new StreamReader(getObjectResponse.ResponseStream, Encoding.UTF8);
                    fileContents = reader.ReadToEnd();
                }

                return JsonSerializer.Deserialize<AnnualSalaryTaxBand[]>(fileContents);
            }
            catch
            {
                throw new Exception("Unable to load annual salary tax bands");
            }
        }
    }
}