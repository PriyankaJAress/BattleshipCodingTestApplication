using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Modules.BattleShip
{
    public class AttackShipCommand : IRequest<Response<bool>>
    {
        public string Position { get; set; }
    }

    public class AttackShipCommandHandler : IRequestHandler<AttackShipCommand, Response<bool>>
    {
        private readonly IBattleShipService _battleShipService;

        public AttackShipCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<Response<bool>> Handle(AttackShipCommand request, CancellationToken cancellationToken)
        {
            var message = GetMessage(request);
            _battleShipService.GetMatrixToDisplay();
            return new Response<bool>(true, message);
        }

        private string GetMessage(AttackShipCommand request)
        {
            if (_battleShipService.GetTotalShipsDestroyed() > 0 && _battleShipService.GetTotalShips() == _battleShipService.GetTotalShipsDestroyed())
            {
                return "All ships are destroyed. Please restart the game.";
            }
            else
            {
                var isHit = _battleShipService.Fire(request.Position);
                if (isHit)
                {
                    return "Congratulations! You hit the target!";
                }
                if (_battleShipService.GetTotalShips() == _battleShipService.GetTotalShipsDestroyed())
                {
                    return "Congratulations, you won! All ships are destroyed.";
                }
                return "Sorry, you missed the target!";
            }
        }
    }
}
