using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        Person GetPersonById(int id);
        IList<Person> GetPersonList();
        bool PostNewPerson(Person person);
        bool PostUpdatePerson(Person person);
        bool PostDeletePerson(int id);

    }
}