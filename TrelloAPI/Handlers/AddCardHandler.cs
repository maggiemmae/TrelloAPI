using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;
using TrelloAPI.Command;
using TrelloAPI.Constants;
using TrelloAPI.Models;
using TrelloAPI.Models.Config;

namespace TrelloAPI.Services
{
    public class AddCardHandler : IRequestHandler<AddCardCommand>
    {
        private readonly TrelloOptions trelloOptions;
        private readonly IMapper mapper;
        private readonly RestClient client;

        public AddCardHandler(IOptions<TrelloOptions> trelloOptions, IMapper mapper)
        {
            this.trelloOptions = trelloOptions.Value;
            this.mapper = mapper;
            var options = new RestClientOptions(trelloOptions.Value.BaseUrl);
            if (options == null) {
                throw new ArgumentNullException(nameof(options));
            }

            client = new RestClient(options);
        }

        public async Task<Unit> Handle(AddCardCommand command, CancellationToken cancellationToken)
        {
            var request = new RestRequest("1/cards");

            request
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.ApiKey, trelloOptions.ApiKey)
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.Token, trelloOptions.Token)
                .AddQueryParameter(ConfigurationConstants.TrelloApiConstants.IdList, trelloOptions.ListIds[command.ListName])
                .AddBody(mapper.Map<CardModel>(command));

            await client.ExecutePostAsync(request, cancellationToken);
            return Unit.Value;
        }
    }
}
