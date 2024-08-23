using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class Question
    {
        public int QuestionId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string DifficultyLevel { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string OptionA { get; set; }
        //public string OptionAImagePath { get; set; }
        [Required]
        public string OptionB { get; set; }
        //public string OptionBImagePath { get; set; }
        [Required]
        public string OptionC { get; set; }
        //public string OptionCImagePath { get; set; }
        [Required]
        public string OptionD { get; set; }
        //public string OptionDImagePath { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        [Required]
        public string Explaination { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
