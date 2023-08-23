using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3 , ErrorMessage ="Only 3 character is possible")]
        [MaxLength(3, ErrorMessage = "Only 3 character is possible")]

        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Only 100 character is possible")]

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}
