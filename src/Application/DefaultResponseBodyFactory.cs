namespace PickrTechnicalTest
{
    public class DefaultResponseBodyFactory : IResponseBodyFactory
    {
        public SimpleResponseBody MakeSimple(TaxPayableOnAnnualSalary taxPayableOnAnnualSalary)
        {
            return new SimpleResponseBody(taxPayableOnAnnualSalary.TotalPayable());
        }

        public DetailedResponseBody MakeDetailed(TaxPayableOnAnnualSalary taxPayableOnAnnualSalary)
        {
            return new DetailedResponseBody(
                taxPayableOnAnnualSalary.TotalPayable(),
                taxPayableOnAnnualSalary.TaxPayableOnAnnualSalaryTaxBands);
        }
    }
}