﻿using System.ComponentModel.DataAnnotations;

namespace ASPNET_tutorial.DTOs
{
    public record class CreateGameDTO
     (
        [Required][StringLength(50)] string Name,
        [Required][StringLength(20)] string Genre,
        [Range(0, 100)] decimal Price,
        DateOnly ReleaseDate
     );
}
