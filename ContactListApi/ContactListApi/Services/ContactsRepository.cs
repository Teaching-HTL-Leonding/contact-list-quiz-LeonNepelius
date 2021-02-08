using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.Services
{

    public record Person(int Id, string FirstName, string LastName, string Email);

    public class ContactsRepository : IContactsRepository
    {

        private List<Person> Contacts { get; } = new();

        public Person Add(Person newPerson)
        {
            Contacts.Add(newPerson);
            return newPerson;
        }

        public void Delete(int id)
        {
            var personToDelete = GetById(id);
            if (personToDelete == null) throw new ArgumentException("No person exists with the given id", nameof(id));
            Contacts.Remove(personToDelete);
        }

        public IEnumerable<Person> GetAll() => Contacts;

        public Person GetById(int id) => Contacts.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Person> GetByName(string name) => Contacts.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name));
    }
}
