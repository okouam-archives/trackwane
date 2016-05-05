namespace Trackwane.AccessControl.Contracts.Contracts
{
    public class UpdateUserRequest
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}