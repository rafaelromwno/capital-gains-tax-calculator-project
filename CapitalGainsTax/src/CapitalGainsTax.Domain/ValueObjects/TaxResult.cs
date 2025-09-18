namespace CapitalGainsTax.Domain.ValueObjects
{
    public sealed class TaxResult
    {
        #region Propriedades
        public decimal Profit { get; }
        public decimal TaxDue { get; }
        #endregion

        #region Construtores 

        public TaxResult() { }


        public TaxResult(decimal profit, decimal taxDue)
        {
            Profit = profit;
            TaxDue = taxDue;
        }

        #endregion
    }
}