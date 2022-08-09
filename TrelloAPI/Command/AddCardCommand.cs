using MediatR;
using TrelloAPI.Enum;

namespace TrelloAPI.Command
{
    public class AddCardCommand : IRequest
    {
        public string Name { get; set; }
        public ListNames ListName { get; set; }
    }
}
