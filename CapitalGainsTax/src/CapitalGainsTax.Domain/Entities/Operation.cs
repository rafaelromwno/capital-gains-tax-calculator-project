using CapitalGainsTax.Domain.Enums;
using CapitalGainsTax.Domain.Exceptions;

namespace CapitalGainsTax.Domain.Entities
{
    public sealed class Operation
    {
        #region Propriedades

        public int OperationId { get; private set; }
        public decimal UnitCost { get; private set; }
        public int Quantity { get; private set; }
        public OperationType Type { get; private set; } // buy or sell

        #endregion

        #region Construtores

        private Operation() { }

        public Operation(int operationId, decimal unitCost, int quantity, OperationType type)
        {
            ValidateDomain(operationId, unitCost, quantity, type);

            OperationId = operationId;
            UnitCost = unitCost;
            Quantity = quantity;
            Type = type;
        }

        #endregion

        #region Validação

        private void ValidateDomain(int operationId, decimal unitCost, int quantity, OperationType type)
        {
            DomainExceptionValidation.When(operationId < 0, "Invalid Id value.");
            DomainExceptionValidation.When(unitCost <= 0, "Unit cost must be greater than zero.");
            DomainExceptionValidation.When(quantity <= 0, "Quantity must be greater than zero.");
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(OperationType), type), "Invalid operation type.");
        }

        #endregion

        #region Métodos de Negócio

        public decimal GetTotal()
        {
            return UnitCost * Quantity;
        }

        #endregion
    }
}