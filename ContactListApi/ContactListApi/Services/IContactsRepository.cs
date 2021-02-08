using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.Services
{
    public interface IContactsRepository
    {
        public Person Add(Person newPerson);
        public IEnumerable<Person> GetAll();
        public IEnumerable<Person> GetByName(string name);
        public Person GetById(int id);
        public void Delete(int id);
    }
}
