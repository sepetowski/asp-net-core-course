using System.ComponentModel.DataAnnotations;

namespace UdemyCourse.API.Models.DTO
{
    public class UpdateRegionDto
    {
        [MinLength(3, ErrorMessage = "Code has to be a 3 characters word")]
        [MaxLength(3, ErrorMessage = "Code has to be a 3 characters word")]
        public required string Code { get; set; }

        [MaxLength(100, ErrorMessage = "Name has to be a maxiumom of 100 characters")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
