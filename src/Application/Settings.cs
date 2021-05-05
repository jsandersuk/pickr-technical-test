namespace PickrTechnicalTest
{
    using System;

    public class Settings
    {
        public Settings()
        {
            this.AnnualSalaryTaxBandsS3BucketName = Environment.GetEnvironmentVariable("ANNUAL_SALARY_TAX_BANDS_BUCKET_NAME");
            this.AnnualSalaryTaxBandsS3Key = Environment.GetEnvironmentVariable("ANNUAL_SALARY_TAX_BANDS_S3_KEY");
        }

        public string AnnualSalaryTaxBandsS3BucketName
        {
            get;
        }

        public string AnnualSalaryTaxBandsS3Key
        {
            get;
        }
    }
}