using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Index("Cnpj", Name = "UQ__Institui__AA57D6B496227035", IsUnique = true)]
public partial class Instituicao
{
    [Key]
    [Column("idInstituicao")]
    public Guid IdInstituicao { get; set; }

    [Column("CNPJ")]
    [StringLength(14)]
    [Unicode(false)]
    public string Cnpj { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string NomeFantasia { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Endereco { get; set; } = null!;
}
