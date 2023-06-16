using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("regist_state")]
public partial class regist_state
{
    [Key]
    public Guid id { get; set; }

    [StringLength(50)]
    public string state { get; set; } = null!;

    [InverseProperty("state")]
    public virtual ICollection<event_regist> event_regists { get; set; } = new List<event_regist>();
}
