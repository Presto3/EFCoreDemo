using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Models;

public partial class PhoneNumber
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PhoneId { get; set; }

    public int PatientId { get; set; }

   public string PhoneNumber1 { get; set; } = null!;

    public string? PhoneType { get; set; }

    public virtual Patient? Patient { get; set; } = null!;
}
