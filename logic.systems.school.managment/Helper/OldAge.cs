using System.ComponentModel.DataAnnotations;

namespace logic.systems.school.managment.Helper
{
    public sealed class OldAge : ValidationAttribute
    {
        public override bool IsValid(object date)
        {
            if (date == null)
            {
                return false;
            }

            DateTime beforeCurrentDate = (DateTime)date;
            return beforeCurrentDate > DateTime.Now.AddYears(-70);
        }
    }
}
