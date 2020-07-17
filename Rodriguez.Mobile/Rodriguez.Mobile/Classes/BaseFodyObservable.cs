using System.ComponentModel;

namespace Rodriguez.Mobile.Classes
{
    public abstract class BaseFodyObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
