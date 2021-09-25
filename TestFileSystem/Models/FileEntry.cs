using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFileSystem.Models
{
    public record FileEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Url { get; set; }

        public FileDTO ToDTO() {
            return new FileDTO() {
                Id = this.Id,
                FileName = this.FileName,
                Url = this.Url
            };
        }

        public ActionFileDTO ToActionDTO() {
            return new ActionFileDTO() {
                FileName = this.FileName,
                Url = this.Url
            };
        }
    }
}