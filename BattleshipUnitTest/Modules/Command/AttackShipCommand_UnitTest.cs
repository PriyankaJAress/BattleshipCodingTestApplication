using AutoFixture.Xunit2;
using BattleShipCodingTest.Modules.BattleShip;
using BattleShipCodingTest.Shared.Interface;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;

namespace BattleshipUnitTest.Modules.Command
{
    public class AttackShipCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_AllShipsDestroyed_ReturnsRestartMessage(
            [Frozen] Mock<IBattleShipService> battleShipServiceMock,
            [Frozen] Mock<AttackShipCommandHandler> sutMock,
            AttackShipCommand command)
        {
            // Arrange
            command.Position = "A1"; // Assuming a valid position
            battleShipServiceMock.Setup(x => x.GetTotalShipsDestroyed()).Returns(10); // Assuming all ships are destroyed
            battleShipServiceMock.Setup(x => x.GetTotalShips()).Returns(10); // Assuming total ships is 10
            var sut = sutMock.Object;

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Data.Should().BeTrue();
            result.Message.Should().Be("All ships are destroyed. Please restart the game.");
        }

        [Theory, AutoMoqData]
        public async Task Handle_Hit_ReturnsHitMessage(
            [Frozen] Mock<IBattleShipService> battleShipServiceMock,
            [Frozen] Mock<AttackShipCommandHandler> sutMock,
            AttackShipCommand command)
        {
            // Arrange
            command.Position = "A1"; // Assuming a valid position
            battleShipServiceMock.Setup(x => x.GetTotalShipsDestroyed()).Returns(0); // Assuming no ships destroyed
            battleShipServiceMock.Setup(x => x.Fire(command.Position)).Returns(true); // Assuming the attack hits
            var sut = sutMock.Object;

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Data.Should().BeTrue();
            result.Message.Should().Be("Congratulations! You hit the target!");
        }

        [Theory, AutoMoqData]
        public async Task Handle_Miss_ReturnsMissMessage(
            [Frozen] Mock<IBattleShipService> battleShipServiceMock,
            [Frozen] Mock<AttackShipCommandHandler> sutMock,
            AttackShipCommand command)
        {
            // Arrange
            command.Position = "A1"; 
            battleShipServiceMock.Setup(x => x.GetTotalShipsDestroyed()).Returns(0); 
            battleShipServiceMock.Setup(x => x.Fire(command.Position)).Returns(false);
            var sut = sutMock.Object;

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Data.Should().BeTrue();
            result.Message.Should().Be("Sorry, you missed the target!");
        }

        [Theory, AutoMoqData]
        public async Task Handle_AllShipsDestroyed_Win_ReturnsWinMessage(
            [Frozen] Mock<IBattleShipService> battleShipServiceMock,
            [Frozen] Mock<AttackShipCommandHandler> sutMock,
            AttackShipCommand command)
        {
            // Arrange
            command.Position = "A1"; 
            battleShipServiceMock.Setup(x => x.GetTotalShipsDestroyed()).Returns(10); 
            battleShipServiceMock.Setup(x => x.GetTotalShips()).Returns(10);
            battleShipServiceMock.Setup(x => x.GetBoardSize()).Returns(10);
            var sut = sutMock.Object;

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Data.Should().BeTrue();
            result.Message.Should().Be("Congratulations, you won! All ships are destroyed.");
        }
    }
}
