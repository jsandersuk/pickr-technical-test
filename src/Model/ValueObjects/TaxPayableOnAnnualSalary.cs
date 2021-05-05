namespace PickrTechnicalTest
{
    public class TaxPayableOnAnnualSalary
    {
        public TaxPayableOnAnnualSalary(TaxPayableOnAnnualSalaryTaxBand[] taxPayableOnAnnualSalaryTaxBands)
        {
            this.TaxPayableOnAnnualSalaryTaxBands = taxPayableOnAnnualSalaryTaxBands;
        }

        public TaxPayableOnAnnualSalaryTaxBand[] TaxPayableOnAnnualSalaryTaxBands
        {
            get;
        }

        public decimal TotalPayable()
        {
            decimal totalTaxPayable = 0;
            foreach (var taxPayableOnAnnualSalaryTaxBand in this.TaxPayableOnAnnualSalaryTaxBands)
            {
                totalTaxPayable += taxPayableOnAnnualSalaryTaxBand.TaxAmount;
            }

            return totalTaxPayable;
        }
    }
}