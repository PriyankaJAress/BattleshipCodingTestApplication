using BattleShipCodingTest.Shared.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Modules.BattleShip
{
    public class DisplayBoardQuery : IRequest<string> { }

    public class DisplayBoardQueryHandler : IRequestHandler<DisplayBoardQuery, string>
    {
        private readonly IBattleShipService _battleShipService;

        public DisplayBoardQueryHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<string> Handle(DisplayBoardQuery request, CancellationToken cancellationToken)
        {
            return _battleShipService.GetMatrixToDisplay();
        }        
    }
}
