using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class Result
    {
        public int ResultId { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        [Required]
        public decimal Score { get; set; }
        [Required]
        public decimal Percentage { get; set; }
        [Required]
        public int NoOfRightAnswers { get; set; }
        [Required]
        public int NoOfWrongAnswers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
