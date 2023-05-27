using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public record RegionDto(Guid Id, string Code, string Name, string? RegionImageUrl);

    public record RegionAddRequestDto(
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        string Code,

        [Required]
        [MinLength(5, ErrorMessage = "Name has to be minimum of 5 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        string Name,

        string? RegionImageUrl);


    public record RegionUpdateRequestDto(
         [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        string Code,
        [Required]
        [MinLength(5, ErrorMessage = "Name has to be minimum of 5 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        string Name,
        string? RegionImageUrl);


    public record DifficultyDto(Guid Id, string Name);


    public record WalkDto(Guid Id, string Name, string Description, double LengthInKm, string? WalkImageUrl,
        RegionDto Region, DifficultyDto Difficulty);


    public record AddWalkRequestDto(
        [Required]
        [MaxLength(100)]
        string Name,

        [Required]
        [MaxLength(1000)]
        string Description,

        [Required]
        [Range(0, 50)]
        double LengthInKm,

        string? WalkImageUrl,

        [Required]
        Guid DifficultyId,

        [Required]
        Guid RegionId);


    public record UpdateWalkRequestDto(
        [Required]
         [MaxLength(100)]
        string Name,

        [Required]
        [MaxLength(1000)]
        string Description,

        [Required]
        [Range(0, 50)]
        double LengthInKm,

        string? WalkImageUrl,

        [Required]
        Guid DifficultyId,

        [Required]
        Guid RegionId);

    public record RegistrationRequest(
            [Required][DataType(DataType.EmailAddress)] string Email,
            [Required] string Username,
            [Required] string Password,
            string[] Roles
        );

    public record LoginRequest(
        [Required]
        [DataType(DataType.EmailAddress)]
        string Username,

        [Required]
        [DataType(DataType.Password)]
        string Password
        );
}