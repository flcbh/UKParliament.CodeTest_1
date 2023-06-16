using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {

        PersonManagerContext _context;
        public PersonService(PersonManagerContext context)
        {
            _context = context;
            FillDb();
        }

        private void FillDb()
        {
            if (GetPersonList().Count == 0)
            {
                using (var context = new PersonManagerContext())
                {
                    var persons = new List<Person>
                    {
                        new Person
                        {
                            Name = "Lee Chu Gin",
                            Address = "150 Amazon Street",
                            City = "London",
                            Email = "Test@test.com",
                            Phone = "07400142529",
                            PostCode = "VC1 7TR",
                        },
                        new Person
                        {
                            Name = "Maria Carlos",
                            Address = "Street Road",
                            City = "Manchester",
                            Email = "Maria@Carlos.com",
                            Phone = "07102262799",
                            PostCode = "BH25 7TR",
                        }
                    };
                    context.People.AddRange(persons);
                    context.SaveChanges();
                }
            }
        }

        public Person GetPersonById(int id)
        {
            return _context.People.Where(a => a.Id == id).FirstOrDefault();
        }

        public IList<Person> GetPersonList()
        {
            return _context.People.ToList();
        }

        public bool PostNewPerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return true;
        }
        public bool PostUpdatePerson(Person person)
        {
            
            _context.Entry(person).State = EntityState.Modified;
            _context.People.Attach(person);
            _context.SaveChanges();
            return true;
        }

        public bool PostDeletePerson(int id)
        {
            var result = _context.People.Where(a => a.Id == id).FirstOrDefault();

            if (result != null)
            {
                _context.People.Remove(result);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}