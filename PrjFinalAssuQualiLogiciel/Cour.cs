using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrjFinalAssuQualiLogiciel;

public partial class Cour
{
    [Key]
    public string? NumeroCours { get; set; }

    public string? Code { get; set; }

    public string? Titre { get; set; }
}
