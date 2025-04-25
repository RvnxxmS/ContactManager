using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContactManager.Core.Entities;
using ContactManager.Core.Interfaces;
using Newtonsoft.Json;

namespace ContactManager.Infrastructure.Repositories
{
    public class FileContactRepository : IContactRepository
    {
        private readonly string _filePath;
        private readonly Dictionary<string, Contact> _contacts;

        public FileContactRepository(string filePath = "contacts.json")
        {
            _filePath = filePath;
            _contacts = LoadContacts();
        }

        private Dictionary<string, Contact> LoadContacts()
        {
            if (!File.Exists(_filePath))
            {
                return new Dictionary<string, Contact>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, Contact>>(json) ?? new Dictionary<string, Contact>();
        }

        private void SaveContacts()
        {
            var json = JsonConvert.SerializeObject(_contacts, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void Add(Contact contact)
        {
            var id = Guid.NewGuid().ToString();
            contact.Id = id;
            _contacts.Add(id, contact);
            SaveContacts();
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
            SaveContacts();
        }

        public void Update(Contact contact)
        {
            if (!_contacts.ContainsKey(contact.Id))
            {
                throw new Exception("Contact not found.");
            }
            _contacts[contact.Id] = contact;
            SaveContacts();
        }
    }
}