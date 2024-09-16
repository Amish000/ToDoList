namespace Todolist.Models
{
    public class AddTodoViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
