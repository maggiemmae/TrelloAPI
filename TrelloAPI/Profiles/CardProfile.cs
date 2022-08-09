using AutoMapper;
using TrelloAPI.Command;
using TrelloAPI.Models;

namespace TrelloAPI.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<AddCardCommand, CardModel>();
        }
    }
}
