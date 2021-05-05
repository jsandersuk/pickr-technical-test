namespace PickrTechnicalTest
{
    using System.Collections.Generic;

    public class DefaultTaxToPayOnAnnualSalaryCalculator : ITaxToPayOnAnnualSalaryCalculator
    {
        private readonly IAnnualSalaryTaxBandRepository annualSalaryTaxBandRepository;

        public DefaultTaxToPayOnAnnualSalaryCalculator(IAnnualSalaryTaxBandRepository annualSalaryTaxBandRepository)
        {
            this.annualSalaryTaxBandRepository = annualSalaryTaxBandRepository;
        }

        public TaxPayableOnAnnualSalary Calculate(decimal annualSalary)
        {
            var annualSalaryTaxBands = this.annualSalaryTaxBandRepository.GetAllBands();

            var taxPayableOnAnnualSalaryTaxBands = new List<TaxPayableOnAnnualSalaryTaxBand>();

            foreach (var annualSalaryTaxBand in annualSalaryTaxBands)
            {
                taxPayableOnAnnualSalaryTaxBands.Add(CalculateTaxPayableForAnnualSalaryTaxBand(
                    annualSalary,
                    annualSalaryTaxBand));
            }

            return new TaxPayableOnAnnualSalary(taxPayableOnAnnualSalaryTaxBands.ToArray());
        }

        private static TaxPayableOnAnnualSalaryTaxBand CalculateTaxPayableForAnnualSalaryTaxBand(
            decimal annualSalary,
            AnnualSalaryTaxBand annualSalaryTaxBand)
        {
            var taxableAmount = CalculateTaxableAmountWithinAnnualSalaryTaxBand(
                annualSalary,
                annualSalaryTaxBand);

            if (taxableAmount.HasValue == false)
            {
                return new TaxPayableOnAnnualSalaryTaxBand(annualSalaryTaxBand, 0m, 0m);
            }

            var taxPayable = (decimal)taxableAmount * annualSalaryTaxBand.RateMultiplier;

            return new TaxPayableOnAnnualSalaryTaxBand(
                annualSalaryTaxBand,
                (decimal)taxableAmount,
                taxPayable);
        }

        private static decimal? CalculateTaxableAmountWithinAnnualSalaryTaxBand(
            decimal annualSalary,
            AnnualSalaryTaxBand annualSalaryTaxBand)
        {
            if (annualSalary < annualSalaryTaxBand.StartOfRange)
            {
                return null;
            }

            if (annualSalaryTaxBand.EndOfRange == null)
            {
                return annualSalary - annualSalaryTaxBand.StartOfRange;
            }

            decimal endOfTaxableRange;
            if (annualSalary < annualSalaryTaxBand.EndOfRange)
            {
                endOfTaxableRange = annualSalary;
            }
            else
            {
                endOfTaxableRange = (decimal)annualSalaryTaxBand.EndOfRange;
            }

            return endOfTaxableRange - annualSalaryTaxBand.StartOfRange;
        }
    }
}