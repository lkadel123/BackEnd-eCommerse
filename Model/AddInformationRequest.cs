namespace WebApplication2.Model
{
    public class AddInformationRequest
    {
        public string user_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AddInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
