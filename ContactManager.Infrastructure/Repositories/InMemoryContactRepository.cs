using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Core.Entities;
using ContactManager.Core.Interfaces;

namespace ContactManager.Infrastructure.Repositories
{
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly Dictionary<string, Contact> _contacts = new Dictionary<string, Contact>();

        public void Add(Contact contact)
        {
            var id = Guid.NewGuid().ToString();
            contact.Id = id;
            _contacts.Add(id, contact);
        }

        public Contact GetById(string id)
        {
            return _contacts.ContainsKey(id) ? _contacts[id] : null;
        }

        public Contact GetByName(string name)
        {
            return _contacts.Values.FirstOrDefault(c => c.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contacts.Values;
        }

        public void Remove(string id)
        {
            if (!_contacts.ContainsKey(id))
            {
                throw new Exception("Contact not found.");
            }
            _contacts.Remove(id);
        }

        public void Update(Contact contact)
        {
            if (!_contacts.ContainsKey(contact.Id))
            {
                throw new Exception("Contact not found.");
            }
            _contacts[contact.Id] = contact;
        }
    }
}
