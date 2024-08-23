namespace ProjectApi.Dtos
{
    public class TestTrainee
    {
        public int TestTraineeId { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal? Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
