namespace PickrTechnicalTest
{
    public interface IResponseBodyFactory
    {
        public SimpleResponseBody MakeSimple(TaxPayableOnAnnualSalary taxPayableOnAnnualSalary);

        public DetailedResponseBody MakeDetailed(TaxPayableOnAnnualSalary taxPayableOnAnnualSalary);
    }
}