using BattleShipCodingTest.Modules.BattleShip;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipCodingTest.Functions.BattleShip
{
    public class BattleShipFunction : BaseFunction
    {
        public BattleShipFunction(IMediator mediator) : base(mediator) { }

        [FunctionName("CreateBoard")]
        public async Task<IActionResult> CreateBoardFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateBoard/{BoardSize}")] CreateBoardCommand createBoardCommand)
        {
            return await Ok(createBoardCommand);
        }

        [FunctionName("CreateShip")]
        public async Task<IActionResult> CreateShipFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateShip/{TotalShips}")] AddShipCommand addShipCommand)
        {
            return await Ok(addShipCommand);
        }

        [FunctionName("AttackShip")]
        public async Task<IActionResult> AttackShipFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AttackShip/{Position}")] AttackShipCommand attackShipCommand)
        {
            return await Ok(attackShipCommand);
        }

        [FunctionName("RestartGame")]
        public async Task<IActionResult> RestartGameFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "RestartGame")] RestartGameQuery restartGameQuery)
        {
            return await Ok(restartGameQuery);
        }

        [FunctionName("DisplayBoard")]
        public async Task<HttpResponseMessage> DisplayBoardFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DisplayBoard")] DisplayBoardQuery displayBoardQuery)
        {
            var board = await Ok(displayBoardQuery);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(board.Value.ToString(), Encoding.UTF8, "text/plain")
            };
            return response;
        }
    }
}
