using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DattingAppApi.Entities
{
    [Table("Photos")]
    public class Photo 
    {
        public int Id { get; set; }

        public required string Url { get; set; }

        public bool IsMainPhoto { get; set; }

        public string? PublicId { get; set; }

        //Navigtion properties

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; } = null!;
    }
}