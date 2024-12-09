using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.EntityModels;

public partial class User
{
    [Key]
    public int? UserId { get; set; }

    [Column(TypeName = "nvarchar (20)")]
    [StringLength(20, MinimumLength = 6)]
    [Required]
    public string Username { get; set; } = null!;

    [NotMapped]
    [StringLength(25, MinimumLength = 8)]
    [Required]
    public string Password { get; set; } = null!;

    [Column("Password", TypeName = "nvarchar (70)")]
    [StringLength(70)]
    public string EncryptedPassword { get; set; } = null!;

    [Column(TypeName = "nvarchar (100)")]
    [StringLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar (100)")]
    [StringLength(100)]
    [Required]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar (256)")]
    [StringLength(256)]
    [Required]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? Created { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Modified { get; set; }
}
