namespace LinkDev.Talabat.Dashboard.Models.Users
{
	public class ReturnedUserViewModel
	{
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;

    }
}
