namespace PickrTechnicalTest
{
    using Amazon.Lambda.APIGatewayEvents;

    public interface IRequestBodyFactory
    {
        public RequestBody Make(APIGatewayProxyRequest request);
    }
}