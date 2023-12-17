namespace logic.systems.school.managment.Dto
{
    public class EnrollmentListDTO
    {
        //< th > Data de cração</ th >
        //< th > Classe da matricula</ th >
        //< th > Ano </ th >
        //< th > Itens </ th >
        //< th > Valor </ th >
        //< th > Total </ th >

        public DateTime createdDate { get; set; }
        public string level { get; set; }
        public int year { get; set; }
        public string items { get; set; }
        public string value { get; set; }
        public string Total { get; set; }
    }
}
