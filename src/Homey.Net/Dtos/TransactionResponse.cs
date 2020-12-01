namespace Homey.Net.Dtos
{
    public class TransactionResponse
    {
        public string TransactionId { get; set; }

        public string TransactionTime { get; set; }

        private bool Value { get; set; }
    }
}
