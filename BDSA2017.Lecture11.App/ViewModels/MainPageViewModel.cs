using BDSA2017.Lecture11.Common;
using System.Collections.ObjectModel;
using System.Linq;

namespace BDSA2017.Lecture11.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICharacterRepository _repository;

        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        public MainPageViewModel(ICharacterRepository repository)
        {
            _repository = repository;

            Characters = new ObservableCollection<CharacterViewModel>();
        }

        public async void Initialize()
        {
            var characters = await _repository.ReadAsync();

            foreach (var character in characters.Select(c => new CharacterViewModel(c)))
            {
                Characters.Add(character);
            }
        }
    }
}
