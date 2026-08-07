using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

public partial class Evento
{
    [Key]
    public Guid IdEvento { get; set; }

    public Guid? IdTipoEvento { get; set; }

    public Guid? IdInstituicao { get; set; }

    [Column(TypeName = "text")]
    public string Descricao { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataEvento { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ImagemUrl { get; set; }
}
