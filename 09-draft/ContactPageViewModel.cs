using BDSA2017.Lecture09.MVVM.Model;
using System.Windows.Input;

namespace BDSA2017.Lecture09.MVVM.ViewModels
{
    public class ContactPageViewModel : BaseViewModel
    {
        private int _id;
        public int Id
        {
            get => _id; 
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        private string _name;
        public string Name
        {
            get => _name; 
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { if (_email != value) { _email = value; OnPropertyChanged(); } }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { if (_message != value) { _message = value; OnPropertyChanged(); } }
        }

        private readonly ContactRepository _repository;

        public ICommand BackCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ContactPageViewModel()
        {
            _repository = new ContactRepository();

            SaveCommand = new RelayCommand(async o => {
                var contact = new Contact { Name = Name, Email = Email, Message = Message };
                await _repository.CreateAsync(contact);
                BackCommand?.Execute(null);
            });
        }
    }
}