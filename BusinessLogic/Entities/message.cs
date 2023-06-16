using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

public partial class message
{
    [Key]
    public Guid id { get; set; }

    public Guid participant_id { get; set; }

    public Guid organizer_id { get; set; }

    public Guid event_id { get; set; }

    [StringLength(500)]
    public string message_content { get; set; } = null!;

    [ForeignKey("event_id")]
    [InverseProperty("messages")]
    public virtual event_info _event { get; set; } = null!;

    [ForeignKey("organizer_id")]
    [InverseProperty("messageorganizers")]
    public virtual user organizer { get; set; } = null!;

    [ForeignKey("participant_id")]
    [InverseProperty("messageparticipants")]
    public virtual user participant { get; set; } = null!;
}
