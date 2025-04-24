using System.Collections.Generic;
using ContactManager.Core.Entities;

namespace ContactManager.Core.Interfaces
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        Contact GetById(string id);
        Contact GetByName(string name);
        IEnumerable<Contact> GetAll();
        void Remove(string id);
        void Update(Contact contact);
    }
}
