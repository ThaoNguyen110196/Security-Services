namespace Aplication.DTOS
{
    public class EmailTokenDto
    {
        public int ClientId { get; set; }
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}

