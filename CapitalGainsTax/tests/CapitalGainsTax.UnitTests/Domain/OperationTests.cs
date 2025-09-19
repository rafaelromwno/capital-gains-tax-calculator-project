using CapitalGainsTax.Domain.Entities;
using CapitalGainsTax.Domain.Enums;
using CapitalGainsTax.Domain.Exceptions;
using FluentAssertions;

namespace CapitalGainsTax.UnitTests.Domain
{
    public class OperationTests
    {
        [Fact]
        public void CreateOperation_ShouldBeValid_WhenParametersAreCorrect()
        {
            // arrange
            int operationId = 1;
            decimal unitCost = 100m;
            int quantity = 5;
            OperationType type = OperationType.Buy;

            // act
            var operation = new Operation(operationId, unitCost, quantity, type);

            // assert
            operation.OperationId.Should().Be(operationId);
            operation.UnitCost.Should().Be(unitCost);
            operation.Quantity.Should().Be(quantity);
            operation.Type.Should().Be(type);
        }

        [Theory]
        [InlineData(-1, 100, 5, OperationType.Buy, "Invalid Id value.")]
        [InlineData(1, 0, 5, OperationType.Buy, "Unit cost must be greater than zero.")]
        [InlineData(1, 100, 0, OperationType.Buy, "Quantity must be greater than zero.")]
        [InlineData(1, 100, 5, (OperationType)99, "Invalid operation type.")]
        public void CreateOperation_ShouldThrowDomainException_WhenParametersAreInvalid(
            int operationId, decimal unitCost, int quantity, OperationType type, string expectedMessage)
        {
            // act
            Action act = () => new Operation(operationId, unitCost, quantity, type);

            // assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact]
        public void GetTotal_ShouldReturnCorrectValue()
        {
            // arrange
            var operation = new Operation(1, 50m, 4, OperationType.Sell);

            // act
            var total = operation.GetTotal();

            // assert
            total.Should().Be(200m); // 50 * 4
        }
    }
}