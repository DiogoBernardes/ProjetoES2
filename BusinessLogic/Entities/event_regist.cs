using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("event_regist")]
public partial class event_regist
{
    [Key]
    public Guid id { get; set; }

    public Guid event_id { get; set; }

    public Guid participant_id { get; set; }

    public DateTime regist_date { get; set; }

    public Guid state_id { get; set; }

    public Guid ticket_type_id { get; set; }

    [ForeignKey("event_id")]
    [InverseProperty("event_regists")]
    public virtual event_info _event { get; set; } = null!;

    [ForeignKey("participant_id")]
    [InverseProperty("event_regists")]
    public virtual user participant { get; set; } = null!;

    [ForeignKey("state_id")]
    [InverseProperty("event_regists")]
    public virtual regist_state state { get; set; } = null!;

    [ForeignKey("ticket_type_id")]
    [InverseProperty("event_regists")]
    public virtual ticket_type ticket_type { get; set; } = null!;
}
