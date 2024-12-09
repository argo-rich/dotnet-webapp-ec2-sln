using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnet_webapp_ec2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the NorthwindUser class
public class NorthwindUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set;} = null!;
}

