using BattleShipCodingTest.Shared.Exceptions;
using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Modules.BattleShip
{
    public class CreateBoardCommand : IRequest<Response<bool>> 
    { 
        public int BoardSize { get; set; }
    }

    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Response<bool>>
    {
        private readonly IBattleShipService _battleShipService;
        public CreateBoardCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<Response<bool>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            if (request.BoardSize == 0)
            {
                throw new BattleShipApiException("Board Size cannot be 0. Please specify a valid size.");
            }
            _battleShipService.CreateBoard(request.BoardSize);
            return new Response<bool>(true, "Board Created Successfully");
        }        
    }
}
