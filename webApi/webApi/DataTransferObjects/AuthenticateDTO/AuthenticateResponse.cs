using System.ComponentModel.DataAnnotations;


namespace webApi.DataTransferObjects.AuthenticateDTO
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}
