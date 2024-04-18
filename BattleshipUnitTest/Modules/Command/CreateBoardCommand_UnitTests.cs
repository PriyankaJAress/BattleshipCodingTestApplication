using AutoFixture.Xunit2;
using BattleShipCodingTest.Modules.BattleShip;
using BattleShipCodingTest.Shared.Exceptions;
using BattleShipCodingTest.Shared.Wrappers;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;

namespace BattleshipUnitTest.Modules.Command
{
    public class CreateBoardCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ValidInput_CreateBoard(
            [Frozen] Mock<CreateBoardCommandHandler> sut, CreateBoardCommand command)
        {
            // Arrange            
            command.BoardSize = 5;

            // Act           
            var result = await sut.Object.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Response<bool>>();
        }

        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_CreateBoard_ThrowsException(
            [Frozen] Mock<CreateBoardCommandHandler> sut, CreateBoardCommand command)
        {
            /// Arrange            
            command.BoardSize = 0;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }
    }
}