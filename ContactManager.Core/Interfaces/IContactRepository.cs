using ContactManager.Core.Entities;
using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        Contact GetByName(string name);
        IEnumerable<Contact> GetAll();
    }
}
