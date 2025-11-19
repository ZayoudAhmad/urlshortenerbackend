using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urlshortenerbackend.Models;

public class Folder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public long UserId { get; set; }

    public ICollection<ShortUrlMeta> Links { get; set; } = new List<ShortUrlMeta>();
}