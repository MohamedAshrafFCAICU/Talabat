using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models.Roles
{
	public class CreatedRoleViewModel
	{
		[Required(ErrorMessage = "Name is Required")]
		[StringLength(256)]
		public string Name { get; set; } = null!;
    }
}
