using System.ComponentModel.DataAnnotations;

namespace UdemyCourse.API.Models.DTO
{
    public class UpdateWalkDto
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        [Range(0, 100)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public required Guid DifficultyId { get; set; }
        public required Guid RegionId { get; set; }
    }
}
