using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PickrTechnicalTest
{
    public class Handler
    {
        public APIGatewayProxyResponse CalculateTaxToPayOnAnnualSalary(APIGatewayProxyRequest request)
        {
            try
            {
                var serviceProvider = ServiceProviderBuilder.Build();

                var handler = serviceProvider.GetService<ApiGatewayHandler>();

                if (handler == null)
                {
                    throw new Exception("Unable to load API Gateway handler");
                }

                return handler.Handle(request);
            }
            catch (Exception exception)
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = exception.Message,
                    Headers = new Dictionary<string, string>() { { "Content-Type", "text/plain" } },
                };
            }
        }
    }
}