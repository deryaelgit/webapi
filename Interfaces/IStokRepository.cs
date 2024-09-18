using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stok;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStokRepository
    {
        Task<List<Stok>> GetAllAsync(QueryObject query);
        Task<Stok?> GetByIdAsync(int id);
        Task<Stok?> GetBySembolAsync(string sembol);

        Task<Stok> CreateAsync(Stok stokModel);
        Task<Stok> UpdateAsync(int id, UpdateStokRequestDto stokDto);
        Task<Stok?> DeleteAsync(int id);

        Task<bool> StokExists(int id);

    }
}