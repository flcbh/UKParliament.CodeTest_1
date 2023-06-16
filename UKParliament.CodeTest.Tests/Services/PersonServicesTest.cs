using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Tests.Services
{
    internal class PersonServicesTest
    {
        PersonManagerContext context;

        internal PersonServicesTest()
        {
            if (context == null)
                context = new PersonManagerContext();

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
            return context.People.Where(a => a.Id == id).FirstOrDefault();
        }

        public IList<Person> GetPersonList()
        {
            return context.People.ToList();
        }

        public bool PostNewPerson(Person person)
        {
            context.People.Add(person);
            context.SaveChanges();
            return true;
        }
        public bool PostUpdatePerson(Person person)
        {

            context.Entry(person).State = EntityState.Modified;
            context.People.Attach(person);
            context.SaveChanges();
            return true;
        }

        public bool PostDeletePerson(int id)
        {
            var result = context.People.Where(a => a.Id == id).FirstOrDefault();

            if (result != null)
            {
                context.People.Remove(result);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
