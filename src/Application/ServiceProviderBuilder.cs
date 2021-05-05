namespace PickrTechnicalTest
{
    using Amazon.S3;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class ServiceProviderBuilder
    {
        public static ServiceProvider Build()
        {
            var settings = new Settings();

            var services = new ServiceCollection()
                .AddSingleton<IRequestBodyFactory, FromJsonRequestBodyFactory>()
                .AddSingleton<IResponseBodyFactory, DefaultResponseBodyFactory>()
                .AddSingleton<IAnnualSalaryTaxBandRepository>(new S3AnnualSalaryTaxBandRepository(
                    new AmazonS3Client(),
                    settings.AnnualSalaryTaxBandsS3BucketName,
                    settings.AnnualSalaryTaxBandsS3Key))
                .AddSingleton<ITaxToPayOnAnnualSalaryCalculator, DefaultTaxToPayOnAnnualSalaryCalculator>()
                .AddSingleton<ApiGatewayHandler, ApiGatewayHandler>();

            return services.BuildServiceProvider();
        }
    }
}