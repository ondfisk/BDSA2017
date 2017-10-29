using BDSA2017.Lecture09.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BDSA2017.Lecture09.MVVM.ViewModels
{
    public class ContactsPageViewModel
    {
        private readonly ContactRepository _repository;

        public ObservableCollection<ContactPageViewModel> Contacts { get; private set; }

        public ContactsPageViewModel()
        {
            _repository = new ContactRepository();
        }

        public ICommand BackCommand { get; set; }

        public ICommand NewCommand { get; set; }

        public ICommand NewDummyCommand => new RelayCommand(o => Contacts.Add(new ContactPageViewModel { Id = 7, Name = Guid.NewGuid().ToString().Substring(0, 5) }));

        public void Initialize()
        {
            var contacts = from c in _repository.Read()
                           select new ContactPageViewModel { Id = c.Id, Name = c.Name, Email = c.Email, Message = c.Message };

            Contacts = new ObservableCollection<ContactPageViewModel>(contacts);
        }
    }
}
