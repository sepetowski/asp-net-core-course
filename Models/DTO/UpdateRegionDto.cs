namespace UdemyCourse.API.Models.DTO
{
    public class UpdateRegionDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
