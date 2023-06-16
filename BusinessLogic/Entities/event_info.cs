using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("event_info")]
public partial class event_info
{
    [Key]
    public Guid id { get; set; }

    public Guid organizer_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    public DateOnly event_date { get; set; }

    public TimeOnly event_hour { get; set; }

    [StringLength(100)]
    public string localization { get; set; } = null!;

    [StringLength(100)]
    public string description { get; set; } = null!;

    public int capacity { get; set; }

    [InverseProperty("_event")]
    public virtual ICollection<activity_info> activity_infos { get; set; } = new List<activity_info>();

    [InverseProperty("_event")]
    public virtual ICollection<event_regist> event_regists { get; set; } = new List<event_regist>();

    [InverseProperty("_event")]
    public virtual ICollection<message> messages { get; set; } = new List<message>();

    [ForeignKey("organizer_id")]
    [InverseProperty("event_infos")]
    public virtual user organizer { get; set; } = null!;
}
