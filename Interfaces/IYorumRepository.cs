using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IYorumRepository
    {
        Task<List<Yorum>> GetAllAsync();
        Task<Yorum?> GetByIdAsync(int id);

        Task<Yorum> CreateAsync(Yorum yorumModel);
        Task<Yorum?> UpdateAsync(int id, Yorum yorumModel);
        Task<Yorum?> DeleteAsync(int id);

    }
}