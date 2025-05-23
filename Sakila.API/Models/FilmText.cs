using System;
using System.Collections.Generic;

namespace Sakila.API.Models;

public partial class FilmText
{
    public int FilmId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Film Film { get; set; } = null!;
}
