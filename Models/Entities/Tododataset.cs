using System.ComponentModel.DataAnnotations;

namespace Todolist.Models.Entities
{
    public class Tododataset
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title can't exceed 100 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }

        public bool Status { get; set; }

        [Display(Name = "Added On")]
        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }

        [Display(Name = "Deleted On")]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; } 

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Updated On")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }
    }
}
