# Pickr Technical Test

This repository provides my solution to the backend coding challenge posed by Pickr. In brief, the challenge requires that a C# application be deployed to AWS Lambda whose purpose is to calculate income tax based on an annual salary figure.

You can find the original specification for the challenge in `./samples/BackendCodingChallenge_v1.4.pdf`

## Invoking the Lambda function

The function is publicly available via AWS' API Gateway and can be invoked by sending an HTTP POST request to the endpoint below.

Lambda HTTP endpoint: `https://olpejjvdp1.execute-api.eu-west-1.amazonaws.com/dev/invoke`

For simplicity, I recommend using a program such as Postman to issue HTTP requests.

### POST body

The body of the POST must contain a valid JSON payload that contains one or more of the options below:

- `AnnualSalary` (decimal, required): The annual salary that you wish to calculate tax for
- `IncludeCalculationBreakdown` (boolean, optional, defaults to `false`): If included and set to true, the response will include a breakdown of the calculation

Example payload:

```json
{
   "AnnualSalary": 200000,
   "IncludeCalculationBreakdown": true
}
```

## Responses

The Lambda function returns a JSON payload which can be either "simple" or "detailed" based on the prevailing value of the `IncludeCalculationBreakdown` input parameter.

### Simple response

Example

```json
{
   "TaxPayable": 70000.00
}
```

### Detailed response

```json
{
  "TaxPayable": 70000.00,
  "TaxPayableOnAnnualSalaryTaxBands": [
    {
      "TaxBand": {
        "StartOfRange": 0,
        "EndOfRange": 12500,
        "RateMultiplier": 0
      },
      "TaxableAmount": 12500,
      "TaxAmount": 0
    },
    {
      "TaxBand": {
        "StartOfRange": 12500,
        "EndOfRange": 50000,
        "RateMultiplier": 0.2
      },
      "TaxableAmount": 37500,
      "TaxAmount": 7500.0
    },
    {
      "TaxBand": {
        "StartOfRange": 50000,
        "EndOfRange": 150000,
        "RateMultiplier": 0.4
      },
      "TaxableAmount": 100000,
      "TaxAmount": 40000.0
    },
    {
      "TaxBand": {
        "StartOfRange": 150000,
        "EndOfRange": null,
        "RateMultiplier": 0.45
      },
      "TaxableAmount": 50000,
      "TaxAmount": 22500.00
    }
  ]
}
```

## Setup

The quickest and easiest way to setup your own copy of this application is to deploy it to an AWS account.

You can deploy using the Serverless application with the following console command:

`serverless deploy`

Once deployed Serverless will output the HTTP endpoint of the API Gateway integration in a block similar to below:

```
Service Information
service: pickr-technical-test
stage: dev
region: eu-west-1
stack: pickr-technical-test-dev
resources: 11
api keys:
  None
endpoints:
  POST - https://olpejjvdp1.execute-api.eu-west-1.amazonaws.com/dev/invoke
functions:
  calculateTaxToPayOnAnnualSalary: pickr-technical-test-dev-calculateTaxToPayOnAnnualSalary
layers:
  None
```

n.b. if operating multiple AWS accounts, ensure that your `--aws-profile` attribute is set appropriately. 

### Data source

The tax bands that the application uses to calculate income tax are stored in a S3 bucket as a JSON file. This file is loaded on invocation of the function.

An example of the JSON file can be found at `./samples/annual-salary-tax-bands.json`

If you wish to setup your own environment to test this application you will need to recreate the S3 bucket, either locally or in an AWS account. You will need to perform the following:

- Create an S3 bucket
- Upload the sample file (linked above) to the S3 bucket
- Ensure you have afforded the Lambda function sufficient permissions to load the file (`GetObject` as a minimum)
    - If deploying via Serverless the IAM permissions will be setup for you but you will have to modify the ARN of the bucket in the `serverless.yml` file
- Update the environment variables within the `serverless.yml` file to point to the correct S3 bucket and file
  - `ANNUAL_SALARY_TAX_BANDS_BUCKET_NAME` - the name of the S3 bucket
  - `ANNUAL_SALARY_TAX_BANDS_S3_KEY` - the "key" (aka path) of the JSON file