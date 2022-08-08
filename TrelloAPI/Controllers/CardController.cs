using Microsoft.AspNetCore.Mvc;
using RestSharp;
using TrelloAPI.Models;

namespace TrelloAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CardController : ControllerBase
    {
        readonly RestClient client;
        private readonly IConfiguration configuration;

        public CardController(IConfiguration configuration)
        {
            this.configuration = configuration;

            var options = new RestClientOptions("https://api.trello.com/1/cards");

            client = new RestClient(options);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] AddCardModel card, [FromQuery] ListId list)
        {
            var request = new RestRequest();

            ChooseList(list, request);

            request.AddParameter("key", configuration["Trello:ApiKey"])
                .AddParameter("token", configuration["Trello:Token"])
                .AddParameter("name", card.Name);

            var response = await client.ExecutePostAsync(request);
            return Ok(response);
        }

        public static void ChooseList(ListId list, RestRequest request)
        {
            switch (list)
            {
                case ListId.ToDo:
                    request.AddParameter("idList", "62ea9d88b86327106422ab16");
                    break;
                case ListId.InProcess:
                    request.AddParameter("idList", "62ea9d88b86327106422ab17");
                    break;
                case ListId.Done:
                    request.AddParameter("idList", "62ea9d88b86327106422ab18");
                    break;
                default:
                    break;
            }
        }
    }
}
