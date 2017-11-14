using BDSA2017.Lecture10.Common;
using BDSA2017.Lecture10.Entities;
using BDSA2017.Lecture10.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BDSA2017.Lecture10.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICharacterRepository _repository;

        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        public RelayCommand SeedCommand { get; }

        public MainPageViewModel(ICharacterRepository repository)
        {
            _repository = repository;

            Characters = new ObservableCollection<CharacterViewModel>();

            SeedCommand = new RelayCommand(async o => await Seed(), o => Characters.Count == 0);

            Initialize();
        }

        private async void Initialize()
        {
            var characters = await _repository.ReadAsync();

            foreach (var character in characters.Select(c => new CharacterViewModel(c)))
            {
                Characters.Add(character);
            }

            SeedCommand.OnCanExecuteChanged(this);
        }

        private async Task Seed()
        {
            var characters = new[] 
            {
                new CharacterCreateDTO { Name = "Philip J. Fry", Planet = "Earth", Species = "Human", Image = "ms-appx:///Assets/images/philip-fry.png" },
                new CharacterCreateDTO { Name = "Turanga Leela", Planet = "Earth", Species = "Mutant-Human", Image = "ms-appx:///Assets/images/turanga-leela.png" },
                new CharacterCreateDTO { Name = "Bender Bending Rodríguez", Planet = "Earth", Species = "Robot", Image = "ms-appx:///Assets/images/bender.png" },
                new CharacterCreateDTO { Name = "Hubert J. Farnsworth", Planet = "Earth", Species = "Human", Image = "ms-appx:///Assets/images/hubert-farnsworth.png" },
                new CharacterCreateDTO { Name = "John A. Zoidberg", Planet = "Decapod 10", Species = "Decapodian", Image = "ms-appx:///Assets/images/zoidberg.png" },
                new CharacterCreateDTO { Name = "Amy Wong", Planet = "Mars", Species = "Human", Image = "ms-appx:///Assets/images/amy-wong.png" },
                new CharacterCreateDTO { Name = "Hermes Conrad", Planet = "Earth", Species = "Human", Image = "ms-appx:///Assets/images/hermes-conrad.png" },
                new CharacterCreateDTO { Name = "Scruffy", Planet = "Earth", Species = "Human", Image = null }
            };

            foreach (var character in characters)
            {
                await _repository.CreateAsync(character);
            }

            Initialize();
        }
    }
}
