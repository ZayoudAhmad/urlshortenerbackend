using System.ComponentModel.DataAnnotations;

namespace urlshortenerbackend.Models;

public class Folder
{
    [Key]
    public long Id { get; set; }   

    [Required]
    public required string Name { get; set; }

    public ICollection<Link> Links{ get; set; } = new List<Link>();
}