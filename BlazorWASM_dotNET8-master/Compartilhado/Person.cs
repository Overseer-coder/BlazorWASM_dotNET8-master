using System.ComponentModel.DataAnnotations;

namespace Compartilhado
{
    public class Person
    {
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Email { get; set; }

        public bool Selected { get; set; }
    }
}
