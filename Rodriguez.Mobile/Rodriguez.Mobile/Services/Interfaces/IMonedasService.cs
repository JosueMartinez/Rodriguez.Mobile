using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodriguez.Mobile.Services.Interfaces
{
    public interface IMonedasService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);
    }
}
