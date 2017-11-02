using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2017.Lecture09.MVVM.Model
{
    public class ContactRepository
    {
        private static readonly ICollection<Contact> _contacts = new List<Contact>();

        public async Task<IEnumerable<Contact>> ReadAsync()
        {
            await Task.CompletedTask;

            return _contacts.AsEnumerable();
        }

        public async Task CreateAsync(Contact contact)
        {
            await Task.CompletedTask;

            _contacts.Add(contact);
        }
    }
}
