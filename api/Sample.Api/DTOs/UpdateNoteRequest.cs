using System.ComponentModel.DataAnnotations;

namespace Sample.Api.DTOs;

public class UpdateNoteRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;
}
