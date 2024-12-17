using System.ComponentModel.DataAnnotations;

namespace eCommerce.Data
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Category Name is Mandatory")]
        public string Name { get; set; }

    }
}
