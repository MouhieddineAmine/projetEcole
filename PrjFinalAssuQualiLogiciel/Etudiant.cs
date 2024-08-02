using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrjFinalAssuQualiLogiciel;

public partial class Etudiant
{
    [Key]
    public int NumeroEtudiant { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }
}
