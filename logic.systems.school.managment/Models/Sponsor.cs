using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Sponsor")]
    public class Sponsor : Common
    {
        [Required(ErrorMessage = "O campo Nome do encarregado de Educação é obrigatório.")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Education { get; set; }

        public List<Contacts> Contacts { get; set; }

        public Sponsor()
        {
            this.Contacts = new List<Contacts>()
            {
                new Contacts() {  }
            };


        }
    }
}
