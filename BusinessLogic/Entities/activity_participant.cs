using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Keyless]
[Table("activity_participant")]
public partial class activity_participant
{
    public Guid participant_id { get; set; }

    public Guid activity_id { get; set; }

    [ForeignKey("activity_id")]
    public virtual activity_info activity { get; set; } = null!;

    [ForeignKey("participant_id")]
    public virtual user participant { get; set; } = null!;
}
