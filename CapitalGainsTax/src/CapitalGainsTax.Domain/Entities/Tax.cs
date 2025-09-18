using CapitalGainsTax.Domain.Exceptions;

namespace CapitalGainsTax.Domain.Entities
{
    public sealed class Tax
    {
        #region Propriedades

        public int TaxId { get; private set; }
        public string Name { get; private set; }
        public decimal Value { get; private set; }

        #endregion

        #region Construtores

        private Tax() { }

        public Tax(int taxId, string name, decimal value)
        {
            ValidateDomain(taxId, name, value);
            TaxId = taxId;
            Name = name;
            Value = value;
        }

        #endregion

        #region Validação

        private void ValidateDomain(int taxId, string name, decimal value)
        {
            DomainExceptionValidation.When(taxId < 0, "Invalid Id value.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is required.");
            DomainExceptionValidation.When(name.Length < 3, "Name must have at least 3 characters.");
            DomainExceptionValidation.When(value < 0, "Tax value cannot be negative.");
            DomainExceptionValidation.When(value > 1, "Tax value must be a fraction (0 to 1).");
        }

        #endregion

        #region Métodos de Negócio

        /// <summary>
        /// Calcula o imposto devido com base no lucro tributável.
        /// Retorna 0 se o lucro for menor ou igual a zero.
        /// </summary>
        public decimal Calculate(decimal taxableProfit)
        {
            if (taxableProfit <= 0) return 0;
            return Math.Round(taxableProfit * Value, 2); // 2 casas decimais
        }

        #endregion
    }
}