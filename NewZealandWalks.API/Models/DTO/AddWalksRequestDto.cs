﻿using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Models.DTO
{
    public class AddWalksRequestDto
    {
        [Required]
        [MaxLength(1000)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0,50)]
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
