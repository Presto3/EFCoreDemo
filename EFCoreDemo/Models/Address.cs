using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Models;

public partial class Address
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }

    public int PatientId { get; set; }

    public string Address1 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public virtual Patient? Patient { get; set; } = null!;
}
