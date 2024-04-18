using BattleShipCodingTest.Shared.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Modules.BattleShip
{
    public class RestartGameQuery : IRequest<string> { }

    public class RestartGameQueryHandler : IRequestHandler<RestartGameQuery, string>
    {
        private readonly IBattleShipService _battleShipService;

        public RestartGameQueryHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<string> Handle(RestartGameQuery request, CancellationToken cancellationToken)
        {
            _battleShipService.RestartGame();
            return "Game Restarted Successfully! Please create board again and add ships";
        }        
    }
}
