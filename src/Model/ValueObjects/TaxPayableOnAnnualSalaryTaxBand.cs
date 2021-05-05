namespace PickrTechnicalTest
{
    public class TaxPayableOnAnnualSalaryTaxBand
    {
        public TaxPayableOnAnnualSalaryTaxBand(
            AnnualSalaryTaxBand taxBand,
            decimal taxableAmount,
            decimal taxAmount)
        {
            this.TaxBand = taxBand;
            this.TaxableAmount = taxableAmount;
            this.TaxAmount = taxAmount;
        }

        public AnnualSalaryTaxBand TaxBand
        {
            get;
        }

        public decimal TaxableAmount
        {
            get;
        }

        public decimal TaxAmount
        {
            get;
        }
    }
}