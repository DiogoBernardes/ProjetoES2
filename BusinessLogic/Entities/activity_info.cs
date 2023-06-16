using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("activity_info")]
public partial class activity_info
{
    [Key]
    public Guid id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    [StringLength(100)]
    public string description { get; set; } = null!;

    public Guid event_id { get; set; }

    [ForeignKey("event_id")]
    [InverseProperty("activity_infos")]
    public virtual event_info _event { get; set; } = null!;
}
