using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinalAssuQualiLogiciel;

public partial class Note
{
    [Key]
    public int Id { get; set; }
    public int NumeroEtudiant { get; set; }

    public string? NumeroCours { get; set; }

    public double? Notee { get; set; }
}