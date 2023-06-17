using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

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

    [StringLength(9)]
    public string phone { get; set; } = null!;

    public Guid role_id { get; set; }

    [InverseProperty("participant")]
    public virtual ICollection<activity_participant> activity_participants { get; set; } = new List<activity_participant>();

    [InverseProperty("organizer")]
    public virtual ICollection<event_info> event_infos { get; set; } = new List<event_info>();

    [InverseProperty("participant")]
    public virtual ICollection<event_regist> event_regists { get; set; } = new List<event_regist>();

    [InverseProperty("organizer")]
    public virtual ICollection<message> messageorganizers { get; set; } = new List<message>();

    [InverseProperty("participant")]
    public virtual ICollection<message> messageparticipants { get; set; } = new List<message>();

    [ForeignKey("role_id")]
    [InverseProperty("users")]
    public virtual role role { get; set; } = null!;
}
