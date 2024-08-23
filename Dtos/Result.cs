using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class Result
    {
        public int ResultId { get; set; }
        public int TestId { get; set; }
        public int TraineeId { get; set; }
        [Required]
        public decimal Score { get; set; }
        [Required]
        public decimal Percentage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
