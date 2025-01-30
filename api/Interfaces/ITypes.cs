using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.TypeDTOs;
using api.Helpers.QueryObject;
using api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Interfaces
{
    public interface ITypes
    {
        Task<List<Types>> GetAllAsync(TypeQueryObject query);
        Task<Types?> GetByIdAsync(int id);
        Task<Types> CreateAsync(Types typeModel);
        Task<Types?> UpdateAsync(int id, TypeUpdateDTO typeDto);
        Task<Types?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}