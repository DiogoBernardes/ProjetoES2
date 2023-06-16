using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IPVC.ESTG.ES2.Entities;

public partial class permission
{
    [Key]
    public Guid id { get; set; }

    [StringLength(255)]
    public string name { get; set; } = null!;
}
