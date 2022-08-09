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
    public class GetCardsInListHandler : IRequestHandler<GetCardsInListQuery, IList<CardModel>>
    {
        private readonly TrelloOptions trelloOptions;
        private readonly RestClient client;

        public GetCardsInListHandler(IOptions<TrelloOptions> trelloOptions)
        {
            this.trelloOptions = trelloOptions.Value;
            var options = new RestClientOptions(trelloOptions.Value.BaseUrl);
            if (options == null) {
                throw new ArgumentNullException(nameof(options));
            }

            client = new RestClient(options);
        }

        public async Task<IList<CardModel>> Handle(GetCardsInListQuery query, CancellationToken cancellationToken)
        {
            var request = new RestRequest("1/lists/{id}/cards");

            request
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.ApiKey, trelloOptions.ApiKey)
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.Token, trelloOptions.Token)
                .AddUrlSegment("id", trelloOptions.ListIds[query.ListName]);

            var response = await client.GetAsync<IList<CardModel>>(request, cancellationToken);
            return response;
        }
    }
}
