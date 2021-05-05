namespace PickrTechnicalTest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Amazon.Lambda.APIGatewayEvents;

    public class ApiGatewayHandler
    {
        private readonly IRequestBodyFactory requestBodyFactory;
        private readonly ITaxToPayOnAnnualSalaryCalculator taxToPayOnAnnualSalaryCalculator;
        private readonly IResponseBodyFactory responseBodyFactory;

        public ApiGatewayHandler(
            IRequestBodyFactory requestBodyFactory,
            ITaxToPayOnAnnualSalaryCalculator taxToPayOnAnnualSalaryCalculator,
            IResponseBodyFactory responseBodyFactory)
        {
            this.requestBodyFactory = requestBodyFactory;
            this.taxToPayOnAnnualSalaryCalculator = taxToPayOnAnnualSalaryCalculator;
            this.responseBodyFactory = responseBodyFactory;
        }

        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request)
        {
            try
            {
                var requestBody = this.requestBodyFactory.Make(request);

                var taxPayableOnAnnualSalary = this.taxToPayOnAnnualSalaryCalculator.Calculate(
                        requestBody.AnnualSalary);

                var responseBody = this.MakeResponseBody(
                    taxPayableOnAnnualSalary,
                    requestBody.IncludeCalculationBreakdown);

                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = responseBody.ToJson(),
                    Headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } },
                };
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

        private IResponseBody MakeResponseBody(
            TaxPayableOnAnnualSalary taxPayableOnAnnualSalary,
            bool includeCalculationBreakdown)
        {
            if (includeCalculationBreakdown)
            {
                return this.responseBodyFactory.MakeDetailed(taxPayableOnAnnualSalary);
            }

            return this.responseBodyFactory.MakeSimple(taxPayableOnAnnualSalary);
        }
    }
}