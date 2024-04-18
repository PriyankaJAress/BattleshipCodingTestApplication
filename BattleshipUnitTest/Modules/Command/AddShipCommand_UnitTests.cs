using AutoFixture.Xunit2;
using BattleShipCodingTest.Modules.BattleShip;
using BattleShipCodingTest.Shared.Exceptions;
using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Service;
using BattleShipCodingTest.Shared.Wrappers;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;
using System.Reflection;

namespace BattleshipUnitTest.Modules.Command
{
    public class AddShipCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_AddShip_ThrowsException(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command)
        {
            /// Arrange            
            command.TotalShips = 0;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }

        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_AddShipOutsideBoard_ThrowsException(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command, int totalships)
        {
            /// Arrange            
            command.TotalShips = totalships;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }

        [Theory, AutoMoqData]
        public async Task Handle_ValidInput_AddShip(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command,
            [Frozen] Mock<IBattleShipService> battleShipService)
        {
            // Arrange            
            command.TotalShips = 2;
            battleShipService.Setup(x => x.SetBoardSize(5));
            battleShipService.Setup(x => x.GetBoardSize()).Returns(5);

            // Act           
            var result = await sut.Object.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Response<bool>>();
        }

    }
}
