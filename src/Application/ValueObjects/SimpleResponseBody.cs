namespace PickrTechnicalTest
{
    using System.Text.Json;

    public class SimpleResponseBody : IResponseBody
    {
        public SimpleResponseBody(
            decimal taxPayable)
        {
            this.TaxPayable = taxPayable;
        }

        public decimal TaxPayable { get; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}