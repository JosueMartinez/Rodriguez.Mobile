using Rodriguez.Mobile.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Rodriguez.Mobile.Services.Interfaces
{
    public interface IBonosService<T>
    {
        Task<ObservableCollection<T>> GetAll();

        Task<T> Buy(Bono b);
    }
}
