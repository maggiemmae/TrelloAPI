using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrelloAPI.Command;
using TrelloAPI.Enum;
using TrelloAPI.Query;

namespace TrelloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ISender mediatr;
        public CardController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds a card to list.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddCard([FromQuery] string name, [FromQuery] ListNames list)
        {
            var command = new AddCardCommand()
            {
                ListName = list,
                Name = name,
            };
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Gets cards in list.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCardsInList([FromQuery] ListNames list)
        {
            var query = new GetCardsInListQuery()
            {
                ListName = list,
            };
            var response = await mediatr.Send(query);
            return Ok(response);
        }
    }
}
