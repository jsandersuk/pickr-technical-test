namespace PickrTechnicalTest
{
    using System;
    using System.Text.Json;
    using Amazon.Lambda.APIGatewayEvents;

    public class FromJsonRequestBodyFactory : IRequestBodyFactory
    {
        public RequestBody Make(APIGatewayProxyRequest request)
        {
            try
            {
                return JsonSerializer.Deserialize<RequestBody>(request.Body);
            }
            catch
            {
                throw new Exception("The request's payload is not valid JSON or does not conform to schema");
            }
        }
    }
}