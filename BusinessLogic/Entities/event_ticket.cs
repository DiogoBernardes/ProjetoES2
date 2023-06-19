using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Entities;

[Table("event_ticket")]
public partial class event_ticket
{
    [Key]
    public Guid id { get; set; }

    public Guid ticket_type { get; set; }

    public Guid event_id { get; set; }

    public int quantity { get; set; }

    [Precision(10, 2)]
    public decimal price { get; set; }

    [ForeignKey("event_id")]
    [InverseProperty("event_tickets")]
    public virtual event_info _event { get; set; } = null!;

    [ForeignKey("ticket_type")]
    [InverseProperty("event_tickets")]
    public virtual ticket_type ticket_typeNavigation { get; set; } = null!;
}
