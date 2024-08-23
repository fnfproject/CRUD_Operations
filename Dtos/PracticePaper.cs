using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class PracticePaper
    {
        public int PaperId { get; set; }
        [Required]
        public string PaperName { get; set; }
        [Required]
        public string Subject { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
