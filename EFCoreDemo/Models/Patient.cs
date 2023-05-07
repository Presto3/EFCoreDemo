using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Models;

public partial class Patient
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime Birthdate { get; set; }

    public string? Gender { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DateAdded { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<EmailAddress> EmailAddresses { get; } = new List<EmailAddress>();

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; } = new List<PhoneNumber>();
}
