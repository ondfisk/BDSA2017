using BDSA2017.Lecture09.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BDSA2017.Lecture09.MVVM.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        private readonly AlbumRepository _repository;

        public ObservableCollection<Album> Albums { get; private set; }

        public ICommand BackCommand { get; set; }

        public AlbumViewModel()
        {
            _repository = new AlbumRepository();
        }

        public void Initialize()
        {
            Albums = new ObservableCollection<Album>(_repository.Read());
        }
    }
}
