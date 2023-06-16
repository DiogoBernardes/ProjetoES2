using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IPVC.ESTG.ES2.Entities;

public partial class user
{
    [Key]
    public Guid id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    [StringLength(100)]
    public string username { get; set; } = null!;

    [StringLength(100)]
    public string password { get; set; } = null!;

    [StringLength(100)]
    public string email { get; set; } = null!;

    public Guid role_id { get; set; }

    [ForeignKey("role_id")]
    [InverseProperty("users")]
    public virtual role role { get; set; } = null!;
}
