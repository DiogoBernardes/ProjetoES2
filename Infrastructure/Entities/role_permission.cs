using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IPVC.ESTG.ES2.Entities;

[Keyless]
[Table("role_permission")]
public partial class role_permission
{
    public Guid role_id { get; set; }

    public Guid permission_id { get; set; }

    [ForeignKey("permission_id")]
    public virtual permission permission { get; set; } = null!;

    [ForeignKey("role_id")]
    public virtual role role { get; set; } = null!;
}
