using System.Collections.Generic;
using System.Linq;

namespace BDSA2017.Lecture09.MVVM.Model
{
    public class ContactRepository
    {
        private static readonly ICollection<Contact> _contacts = new List<Contact>();

        public IEnumerable<Contact> Read()
        {
            return _contacts.AsEnumerable();
        }

        public void Create(Contact contact)
        {
            _contacts.Add(contact);
        }
    }
}
