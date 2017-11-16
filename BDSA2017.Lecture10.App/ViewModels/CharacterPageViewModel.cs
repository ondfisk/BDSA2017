﻿using BDSA2017.Lecture10.Common;

namespace BDSA2017.Lecture10.App.ViewModels
{
    public class CharacterPageViewModel : BaseViewModel
    {
        private readonly ICharacterRepository _repository;

        private int _id;
        public int Id { get => _id; set { if (value != _id) { _id = value; OnPropertyChanged(); } } }

        private int? _actorId;
        public int? ActorId { get => _actorId; set { if (value != _actorId) { _actorId = value; OnPropertyChanged(); } } }

        private string _name;
        public string Name { get => _name; set { if (value != _name) { _name = value; OnPropertyChanged(); } } }

        private string _actorName;
        public string ActorName { get => _actorName; set { if (value != _actorName) { _actorName = value; OnPropertyChanged(); } } }

        private string _species;
        public string Species { get => _species; set { if (value != _species) { _species = value; OnPropertyChanged(); } } }

        private string _planet;
        public string Planet { get => _planet; set { if (value != _planet) { _planet = value; OnPropertyChanged(); } } }

        private string _image;
        public string Image { get => _image; set { if (value != _image) { _image = value; OnPropertyChanged(); } } }

        private int _numberOfEpisodes;
        public int NumberOfEpisodes { get => _numberOfEpisodes; set { if (value != _numberOfEpisodes) { _numberOfEpisodes = value; OnPropertyChanged(); } } }

        public CharacterPageViewModel(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async void Initialize(CharacterViewModel character)
        {
            Id = character.Id;
            ActorId = character.ActorId;
            Name = character.Name;
            ActorName = character.ActorName;

            var details = await _repository.FindAsync(character.Id);

            Species = details.Species;
            Planet = details.Planet;
            Image = details.Image;
            NumberOfEpisodes = details.NumberOfEpisodes;
        }
    }
}
