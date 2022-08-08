using System.ComponentModel.DataAnnotations;

namespace TrelloAPI.Models
{
    public class AddCardModel
    {
        [Required]
        public string Name { get; set; }
    }
}