using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;
using TrelloAPI.Constants;
using TrelloAPI.Models;
using TrelloAPI.Models.Config;
using TrelloAPI.Query;

namespace TrelloAPI.Services
{
    public class GetCardsInListHandler : IRequestHandler<GetCardsInListQuery, List<CardModel>>
    {
        private readonly TrelloOptions trelloOptions;
        readonly RestClient client;

        public GetCardsInListHandler(IOptions<TrelloOptions> trelloOptions, IMapper mapper)
        {
            this.trelloOptions = trelloOptions.Value;
            var options = new RestClientOptions(trelloOptions.Value.BaseUrl);
            client = new RestClient(options);
        }

        public async Task<List<CardModel>> Handle(GetCardsInListQuery query, CancellationToken cancellationToken)
        {
            var request = new RestRequest("1/lists/{id}/cards");

            request
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.ApiKey, trelloOptions.ApiKey)
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.Token, trelloOptions.Token)
                .AddUrlSegment("id", trelloOptions.ListIds[query.ListName]);

            var response = await client.GetAsync<List<CardModel>>(request, cancellationToken);
            return response;
        }
    }
}
