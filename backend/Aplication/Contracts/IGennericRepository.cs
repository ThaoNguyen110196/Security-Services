using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Responses;

namespace Aplication.Contracts
{
  public interface IGennericRepository<T>
    {
        Task<List<T>> GetAll();
        Task <T> GetById(int id);
           
        Task<GeneralReponse> Inser(T item);
        Task<GeneralReponse> Update(T item);

        Task<GeneralReponse> Delete(int id);
            
    }
}
