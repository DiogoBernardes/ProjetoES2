using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("ticket_type")]
public partial class ticket_type
{
    [Key]
    public Guid id { get; set; }

    [StringLength(50)]
    public string name { get; set; } = null!;

    [InverseProperty("ticket_type")]
    public virtual ICollection<event_regist> event_regists { get; set; } = new List<event_regist>();
}
