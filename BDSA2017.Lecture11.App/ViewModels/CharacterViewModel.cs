using BDSA2017.Lecture11.Common;

namespace BDSA2017.Lecture11.App.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private int _id;
        public int Id { get => _id; set { if (value != _id) { _id = value; OnPropertyChanged(); } } }

        private int? _actorId;
        public int? ActorId { get => _actorId; set { if (value != _actorId) { _actorId = value; OnPropertyChanged(); } } }

        private string _name;
        public string Name { get => _name; set { if (value != _name) { _name = value; OnPropertyChanged(); } } }

        private string _actorName;
        public string ActorName { get => _actorName; set { if (value != _actorName) { _actorName = value; OnPropertyChanged(); } } }

        public CharacterViewModel(CharacterDTO character)
        {
            Id = character.Id;
            ActorId = character.ActorId;
            Name = character.Name;
            ActorName = character.ActorName;
        }
    }
}
