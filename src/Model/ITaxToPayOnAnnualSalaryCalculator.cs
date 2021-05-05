namespace PickrTechnicalTest
{
    public interface ITaxToPayOnAnnualSalaryCalculator
    {
        public TaxPayableOnAnnualSalary Calculate(decimal annualSalary);
    }
}