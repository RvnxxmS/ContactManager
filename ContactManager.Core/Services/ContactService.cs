using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Core.Entities;
using ContactManager.Core.Interfaces;

namespace ContactManager.Core.Services
{
    public class ContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public void AddContact(Contact contact)
        {
            var existingContact = _repository.GetByName(contact.Name);
            if (existingContact != null && existingContact.Id != contact.Id)
            {
                throw new Exception("Contact with this name already exists.");
            }
            if (string.IsNullOrEmpty(contact.Id))
            {
                _repository.Add(contact);
            }
            else
            {
                _repository.Update(contact);
            }
        }

        public Contact FindContactByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var contacts = _repository.GetAll();
            return contacts.FirstOrDefault(c => c.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public Contact GetContactById(string id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _repository.GetAll();
        }

        public void RemoveContact(string id)
        {
            var contact = _repository.GetById(id);
            if (contact == null)
            {
                throw new Exception("Contact not found.");
            }
            _repository.Remove(id);
        }
    }
}