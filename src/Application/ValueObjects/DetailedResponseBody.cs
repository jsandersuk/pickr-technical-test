namespace PickrTechnicalTest
{
    using System.Text.Json;

    public class DetailedResponseBody : IResponseBody
    {
        public DetailedResponseBody(
            decimal taxPayable,
            TaxPayableOnAnnualSalaryTaxBand[] taxPayableOnAnnualSalaryTaxBands)
        {
            this.TaxPayable = taxPayable;
            this.TaxPayableOnAnnualSalaryTaxBands = taxPayableOnAnnualSalaryTaxBands;
        }

        public decimal TaxPayable { get; }

        public TaxPayableOnAnnualSalaryTaxBand[] TaxPayableOnAnnualSalaryTaxBands { get; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}