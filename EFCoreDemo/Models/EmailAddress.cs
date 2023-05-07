using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Models;

public partial class EmailAddress
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmailId { get; set; }

    public int PatientId { get; set; }

    public string Email { get; set; } = null!;

    public virtual Patient? Patient { get; set; } = null!;
}
