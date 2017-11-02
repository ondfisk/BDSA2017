using BDSA2017.Lecture09.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BDSA2017.Lecture09.MVVM.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        private readonly AlbumRepository _repository;

        public ObservableCollection<Album> Albums { get; private set; }

        public AlbumViewModel()
        {
            _repository = new AlbumRepository();

            Initialize();
        }

        private async void Initialize()
        {
            var albums = await _repository.ReadAsync();

            Albums = new ObservableCollection<Album>(albums);
        }
    }
}
