using BDSA2017.Lecture11.App.Models;
using BDSA2017.Lecture11.App.Views;
using BDSA2017.Lecture11.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Security.Credentials;

namespace BDSA2017.Lecture11.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICharacterRepository _repository;
        private readonly IAuthenticationHelper _helper;
        private readonly INavigationService _service;

        private WebAccount _account;

        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        public ICommand DetailsPageCommand { get; }

        public ICommand SignInOutCommand { get; }

        public MainPageViewModel(ICharacterRepository repository, IAuthenticationHelper helper, INavigationService service)
        {
            _repository = repository;
            _helper = helper;
            _service = service;

            Characters = new ObservableCollection<CharacterViewModel>();

            DetailsPageCommand = new RelayCommand(o =>
            {
                _service.Navigate(typeof(CharacterPage), o);
            });
            SignInOutCommand = new RelayCommand(async o =>
            {
                if (_account != null)
                {
                    await _helper.SignOutAsync(_account);
                    _account = null;
                    Characters.Clear();
                }
                else
                {
                    _account = await _helper.SignInAsync();
                    if (_account != null)
                    {
                        await Initialize();
                    }
                }
            });
        }

        public async Task Initialize()
        {
            Characters.Clear();

            _account = await _helper.GetAccountAsync();

            if (_account != null)
            {
                var characters = await _repository.ReadAsync();

                foreach (var character in characters.Select(c => new CharacterViewModel(c)))
                {
                    Characters.Add(character);
                }
            }
        }
    }
}
