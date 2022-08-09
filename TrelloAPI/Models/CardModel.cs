using System.Text.Json.Serialization;

namespace TrelloAPI.Models
{
    public class CardModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}