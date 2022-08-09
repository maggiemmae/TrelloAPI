using MediatR;
using TrelloAPI.Enum;
using TrelloAPI.Models;

namespace TrelloAPI.Query
{
    public class GetCardsInListQuery : IRequest<IList<CardModel>>
    {
        public ListNames ListName { get; set; }
    }
}
