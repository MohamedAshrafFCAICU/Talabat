using LinkDev.Talabat.Dashboard.Models.Roles;

namespace LinkDev.Talabat.Dashboard.Models.Users
{
	public class EditUserViewModel
	{
		public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

		public List<EditRoleViewMoel> Roles { get; set; } = null!;
    }
}
