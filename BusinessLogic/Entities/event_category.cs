using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("event_category")]
public partial class event_category
{
    [Key]
    public Guid id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    [InverseProperty("categoryNavigation")]
    public virtual ICollection<event_info> event_infos { get; set; } = new List<event_info>();
}
