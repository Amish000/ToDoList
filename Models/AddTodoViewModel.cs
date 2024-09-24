using System.ComponentModel.DataAnnotations;

namespace Todolist.Models
{
    public class AddTodoViewModel
    {
        [Required(ErrorMessage ="Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be exceeded 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Description is required")]
        [StringLength(500, ErrorMessage ="Descrption cannot be exceeded 500 characters")]
        public string Description { get; set; }
    }
}
