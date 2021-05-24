using System.ComponentModel.DataAnnotations;


namespace webApi.DataTransferObjects.AuthenticateDTO
{
    public class AuthenticateResponse
    {
        public string apiKey { get; set; } //example "1, customer"
    }
}
