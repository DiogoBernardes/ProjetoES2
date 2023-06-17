using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("activity_participant")]
public partial class activity_participant
{
    [Key]
    public Guid id { get; set; }

    public Guid participant_id { get; set; }

    public Guid activity_id { get; set; }

    [ForeignKey("activity_id")]
    [InverseProperty("activity_participants")]
    public virtual activity_info activity { get; set; } = null!;

    [ForeignKey("participant_id")]
    [InverseProperty("activity_participants")]
    public virtual user participant { get; set; } = null!;
}
