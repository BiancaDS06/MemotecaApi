using System.ComponentModel.DataAnnotations;

namespace MemotecaApi.ViewModels
{
    public class CreateThoughtsViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string Authorship { get; set; }
        [Required]
        public string Model { get; set; }
    }
}