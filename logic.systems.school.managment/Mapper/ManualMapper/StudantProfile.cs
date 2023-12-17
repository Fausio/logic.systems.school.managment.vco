using Humanizer;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Mapper.ManualMapper
{
    public static class StudantProfile
    {
        public static Student ToClass(CreateStudantDTO dTO)
        {
            try
            {
                var s = dTO.SponsorContacts[0];
                var myClass = new Student()
                {

                    Name = dTO.Name,
                    BirthDate = dTO.BirthDate,
                    Gender = dTO.Gender,
                    FatherName = dTO.FatherName,
                    MatherName = dTO.MatherName,
                    Naturalness = dTO.Naturalness,
                    PersonId = dTO.PersonId,
                    DistrictId = dTO.DistrictId,
                    CurrentSchoolLevelId = dTO.CurrentSchoolLevelId,
                    SchoolClassRoomId = dTO.SchoolClassRoomId,
                };

                myClass.Sponsor.Name = dTO.SponsorName;
                myClass.Sponsor.Address = dTO.SponsorAddress;
                myClass.Sponsor.Education = dTO.SponsorEducation;
                myClass.Sponsor.Contacts = new List<Contacts>()
                {
                   new Contacts()
                   {
                    ContactsType = "",
                    Number = dTO.SponsorContacts[0]
                   }
                };

                return myClass;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static EditStudantDTO ToDTO(Student Class)
        {

            try
            {

                var dto = new EditStudantDTO()
                {
                    id = Class.Id,
                    Name = Class.Name,
                    BirthDate = Class.BirthDate,
                    Gender = Class.Gender,
                    FatherName = Class.FatherName,
                    MatherName = Class.MatherName,
                    Naturalness = Class.Naturalness,
                    PersonId = Class.PersonId,
                    DistrictId = Class.DistrictId,
                    CurrentSchoolLevelId = Class.CurrentSchoolLevelId,
                    SchoolClassRoomId = Class.SchoolClassRoomId,
                    Suspended = Class.Suspended,
                    Enrollments = Class.Enrollments
                };

                dto.SponsorName = Class.Sponsor.Name;
                dto.SponsorAddress = Class.Sponsor.Address;
                dto.SponsorEducation = Class.Sponsor.Education;
                dto.SponsorContacts[0] = Class.Sponsor.Contacts[0].Number;

                dto.Enrollments = dto.Enrollments.OrderByDescending(x => x.EnrollmentYear).ToList();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
 