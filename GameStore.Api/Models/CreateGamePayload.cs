using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.Api.Models;

[method: JsonConstructor]
public record CreateGamePayload(
    [Required]
    [StringLength(50)]
    string Name,
    
    [Required]
    [StringLength(20)]
    string Genre,
    
    [Range(1, 100)]
    decimal Price, 
    DateTime ReleaseDate,
    
    [Url]
    [StringLength(100)]
    string ImageUri
    );
    