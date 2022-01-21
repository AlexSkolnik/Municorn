using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Municorn.DAL.Entities
{
    [Table("Notifications", Schema = "public")]
    public class Notification
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string JsonObject { get; set; }

        [Required]
        public short Type { get; set; }

        [Required]
        public short Status { get; set; }
    }
}