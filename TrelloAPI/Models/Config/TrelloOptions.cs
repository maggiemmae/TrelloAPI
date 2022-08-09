using TrelloAPI.Enum;

namespace TrelloAPI.Models.Config
{
    public class TrelloOptions
    {
        public string ApiKey { get; set; }
        public string Token { get; set; }
        public string BaseUrl { get; set; }
        public Dictionary<ListNames, string> ListIds { get; set; }
    }
}
