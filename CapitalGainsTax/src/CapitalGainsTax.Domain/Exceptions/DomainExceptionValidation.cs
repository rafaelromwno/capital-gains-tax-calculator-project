namespace CapitalGainsTax.Domain.Exceptions
{
    public class DomainExceptionValidation : Exception
    {
        #region Construtor
        public DomainExceptionValidation(string error) : base(error)
        { }
        #endregion

        #region Métodos
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
        #endregion
    }
}