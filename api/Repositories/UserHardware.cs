using api.Data;
using api.DTOs.UserHardwareDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using DTOs.HardwareDTOs;
using Mappers;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserHardwareRepository : IUserHardware
{
    private readonly ApiDbContext _context;

    // Inject ApiDbContext instead of connection string
    public UserHardwareRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hardware>> GetAllAsync()
    {
        // Get all the hardware assigned to users (UserHardware) from the database
        List<UserHardware> userhardwares = await _context.UserHardware.ToListAsync();

        // Count how many times each hardware is assigned (grouping by hardware id)
        var hardwareCounts = userhardwares
            .GroupBy(uh => uh.hardwareid)
            .Select(group => new { HardwareId = group.Key, Count = group.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        // Fetch the corresponding hardware entries from the Hardware table with all details
        var hardwaresToReturn = await _context.Hardware
            .Include(h => h.hardwarestatus)
            .Include(h => h.type)
            .Include(h => h.HardwareCategories)
                .ThenInclude(hc => hc.category)
            .Where(h => hardwareCounts.Select(hc => hc.HardwareId).Contains(h.id))
            .ToListAsync();

        // Order the hardware list based on the frequency count
        return hardwaresToReturn
            .OrderByDescending(h => hardwareCounts.First(hc => hc.HardwareId == h.id).Count)
            .ToList();
    }


    public async Task<ReadUserHardwareDTO?> AddUserHardware(CreateUserHardwareDTO userHardware)
    {
        try
        {
            // Use the mapping extension method
            UserHardware userHardwareToLoan = userHardware.MapToUserHardware();
            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userHardwareToLoan.userid);
            if (user == null)
            {
                return null;  // Or handle this scenario appropriately
            }

            // Check if hardware exists
            var hardware = await _context.Hardware.FirstOrDefaultAsync(h => h.id == userHardwareToLoan.hardwareid);
            if (hardware == null)
            {
                return null;  // Or handle accordingly
            }

            await _context.UserHardware.AddAsync(userHardwareToLoan);
            await _context.SaveChangesAsync();

            // Return using the mapping extension method
            return userHardwareToLoan.MapToReadUserHardwareDTO();
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            throw new InvalidOperationException("Error occurred while adding user hardware", ex);
        }
    }

    public Task<bool> Exists(int UserHardwareId)
    {
        return _context.UserHardware.AnyAsync(uh => uh.id == UserHardwareId);
    }

    public async Task<List<HardwareReadDTO>> GetAvailableHardware(
       List<int>? categoryIds,
       List<int>? typeIds,
       string? searchString,
       DateTime startDate,
       DateTime endDate)
    {
        var typeIdsParam = typeIds?.Any() == true ? string.Join(",", typeIds) : null;
        var categoryIdsParam = categoryIds?.Any() == true ? string.Join(",", categoryIds) : null;

        string sqlQuery = @"
    SELECT DISTINCT h.*
    FROM Hardware h
    LEFT JOIN UserHardware uh 
        ON h.id = uh.hardwareid 
        AND uh.isRented = true 
        AND (uh.startDate <= @endDate)
        AND (uh.endDate >= @startDate)
    LEFT JOIN HardwareCategory hc 
        ON h.id = hc.hardwareid
    WHERE
        (uh.id IS NULL)
        AND (
            (@typeIds IS NULL OR h.typeid IN (@typeIds))
            OR 
            (@categoryIds IS NULL OR hc.categoryid IN (@categoryIds))
        )
        AND (@searchString IS NULL OR h.name LIKE CONCAT('%', @searchString, '%'))";

        // Execute the raw SQL query using EF Core
        List<Hardware> hardwareList = await _context.Hardware
            .FromSqlRaw(sqlQuery,
                new MySqlParameter("@startDate", startDate),
                new MySqlParameter("@endDate", endDate),
                new MySqlParameter("@searchString", string.IsNullOrEmpty(searchString) ? DBNull.Value : searchString),
                new MySqlParameter("@typeIds", typeIds?.Any() == true ? string.Join(",", typeIds) : DBNull.Value),
                new MySqlParameter("@categoryIds", categoryIds?.Any() == true ? string.Join(",", categoryIds) : DBNull.Value)
            )
            .Include(h => h.hardwarestatus)
            .Include(h => h.type)
            .Include(h => h.HardwareCategories)
                .ThenInclude(hc => hc.category)
            .ToListAsync();

        List<HardwareReadDTO> result = new List<HardwareReadDTO>();
        foreach (Hardware hardware in hardwareList)
        {
            result.Add(hardware.ToHardwareDto());
        }

        return result;
    }

    public async Task<List<UserHardware>> GetUserLoanHistoryAsync(int userId)
    {
        return await _context.UserHardware
            .Include(uh => uh.Hardware)
                .ThenInclude(h => h.hardwarestatus)
            .Include(uh => uh.Hardware)
                .ThenInclude(h => h.type)
            .Include(uh => uh.Hardware)
                .ThenInclude(h => h.HardwareCategories)
                    .ThenInclude(c => c.category)
            .Where(uh => uh.userid == userId)
            .ToListAsync();
    }

    public async Task<List<UserHardware>> GetActiveLoansByUserAsync(int userId)
    {
        return await _context.UserHardware
           .Include(uh => uh.Hardware)
                .ThenInclude(h => h.hardwarestatus)
            .Include(uh => uh.Hardware)
                .ThenInclude(h => h.type)
            .Include(uh => uh.Hardware)
                .ThenInclude(h => h.HardwareCategories)
                    .ThenInclude(c => c.category)
            .Where(uh => uh.userid == userId && uh.isRented)
            .ToListAsync();
    }


    public async Task<UserHardware?> UpdateUserHardware(int id, UpdateUserHardwareDTO userHardware)
    {
        var existingUserHardware = await _context.UserHardware.FirstOrDefaultAsync(uh => uh.id == id);
        if (existingUserHardware == null)
        {
            return null;
        }

        existingUserHardware.isRented = userHardware.isRented;
        // Convert the string dates to DateTime using the conversion methods
        existingUserHardware.startDate = userHardware.GetStartDateTime();
        existingUserHardware.endDate = userHardware.GetEndDateTime();
        existingUserHardware.deliveryDate = userHardware.deliveryDate;

        await _context.SaveChangesAsync();
        return existingUserHardware;
    }
}
