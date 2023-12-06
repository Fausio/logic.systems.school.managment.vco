using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Contacts")]
    public class Contacts : Common
    {

         public  const string Home = "Casa";
         public  const string Wotk = "Trabalho/Serviço";
         public  const string pricate = "Pessoal";

        [Required(ErrorMessage = "O campo Contacto do encarregado de educação é obrigatório.")]
        public string Number { get; set; }
        public string ContactsType { get; set; }

        public virtual Sponsor Sponsor { get; set; }
        public int SponsorId { get; set; }
    }





}