using BDSA2017.Lecture09.MVVM.Model;
using System.Windows.Input;

namespace BDSA2017.Lecture09.MVVM.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { if (_email != value) { _email = value; OnPropertyChanged(); } }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { if (_message != value) { _message = value; OnPropertyChanged(); } }
        }

        private readonly ContactRepository _repository;

        public ICommand BackCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ContactPageViewModel()
        {
            _repository = new ContactRepository();

            SaveCommand = new RelayCommand(o => {
                var contact = new Contact { Name = Name, Email = Email, Message = Message };
                _repository.Create(contact);
                BackCommand?.Execute(null);
            });
        }
    }
}