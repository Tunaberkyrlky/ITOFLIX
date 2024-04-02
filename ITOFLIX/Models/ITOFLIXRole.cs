using System;
using Microsoft.AspNetCore.Identity;
namespace ITOFLIX.Models
{
	public class ITOFLIXRole : IdentityRole<long>
	{
		public ITOFLIXRole(string roleName):base(roleName)
		{ }
		public ITOFLIXRole()
		{ }
	}
}

