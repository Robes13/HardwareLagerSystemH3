using api.Data;
using api.DTOs.UserHardwareDTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

public class UserHardwareRepository : IUserHardware
{
    private readonly ApiDbContext _context;

    // Inject ApiDbContext instead of connection string
    public UserHardwareRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<UserHardware?> AddUserHardware(UserHardware userHardware)
    {
        //Check if the user exists
        var user = await _context.User.FirstOrDefaultAsync(u => u.id == userHardware.userid);
        if (user == null)
        {
            return null;
        }
        //Check if the hardware exists
        var hardware = await _context.Hardware.FirstOrDefaultAsync(h => h.id == userHardware.hardwareid);
        if (hardware == null)
        {
            return null;
        }
        await _context.UserHardware.AddAsync(userHardware);
        await _context.SaveChangesAsync();
        return userHardware;
    }

    public Task<bool> Exists(int UserHardwareId)
    {
        return _context.UserHardware.AnyAsync(uh => uh.id == UserHardwareId);
    }

    public async Task<List<Hardware>> GetAvailableHardware(List<int> categoryIds, List<int> typeIds, int weeks, string searchString)
    {
        // Calculate the start date based on weeks
        DateTime startDate = DateTime.UtcNow.AddDays(weeks * 7);

        // Raw MySQL Query
        string sqlQuery = @"
            SELECT h.*
            FROM Hardware h
            LEFT JOIN UserHardware uh ON h.id = uh.hardwareid AND uh.isRented = true
            WHERE (uh.id IS NULL OR uh.endDate < @startDate)
            AND h.typeid IN (" + string.Join(",", typeIds) + @")
            AND h.id IN (
                SELECT hc.hardwareid 
                FROM HardwareCategory hc 
                WHERE hc.categoryid IN (" + string.Join(",", categoryIds) + @")
            )
            AND h.name LIKE CONCAT('%', @searchString, '%');";

        // Execute the raw SQL query using EF Core
        var hardwareList = await _context.Hardware
            .FromSqlRaw(sqlQuery, new MySqlParameter("@startDate", startDate),
                                 new MySqlParameter("@searchString", searchString))
            .ToListAsync();

        return hardwareList;
    }

    public Task<List<UserHardware>> GetUserHardwareByUserId(int userId)
    {
        return _context.UserHardware.Where(uh => uh.userid == userId).ToListAsync();
    }

    public async Task<UserHardware?> UpdateUserHardware(int id, UpdateUserHardwareDTO userHardware)
    {
        var existingUserHardware = await _context.UserHardware.FirstOrDefaultAsync(uh => uh.id == id);
        if (existingUserHardware == null)
        {
            return null;
        }

        existingUserHardware.isRented = userHardware.isRented;
        existingUserHardware.startDate = userHardware.startDate;
        existingUserHardware.endDate = userHardware.endDate;
        existingUserHardware.deliveryDate = userHardware.deliveryDate;

        await _context.SaveChangesAsync();
        return existingUserHardware;
    }

}
