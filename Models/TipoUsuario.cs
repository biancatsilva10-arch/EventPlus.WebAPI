using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

public partial class TipoUsuario
{
    [Key]
    [Column("idTipoUsuario")]
    public Guid IdTipoUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string TituloTipoUsuario { get; set; } = null!;
}
