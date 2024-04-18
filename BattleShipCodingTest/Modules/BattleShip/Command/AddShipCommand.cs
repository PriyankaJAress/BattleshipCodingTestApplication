using BattleShipCodingTest.Shared.Exceptions;
using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Modules.BattleShip
{
    public class AddShipCommand : IRequest<Response<bool>>
    {
        public int TotalShips { get; set; }
    }

    public class AddShipCommandHandler : IRequestHandler<AddShipCommand, Response<bool>>
    {
        private readonly IBattleShipService _battleShipService;

        public AddShipCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<Response<bool>> Handle(AddShipCommand request, CancellationToken cancellationToken)
        {
            ValidateInput(request);
            _battleShipService.AddShips(request.TotalShips);
            return new Response<bool>(true, "Ships added successfully.");
        }

        private void ValidateInput(AddShipCommand request)
        {
            var boardSize = _battleShipService.GetBoardSize();

            if (request.TotalShips == 0)
            {
                throw new BattleShipApiException("Please specify at least 1 ship to add.");
            }
            else if (boardSize==0)
            {
                throw new BattleShipApiException("Please create board first and then add ship");
            }
            else if (request.TotalShips > boardSize)
            {
                throw new BattleShipApiException("Cannot add ships more than board size");
            }        
        }
    }
} 
