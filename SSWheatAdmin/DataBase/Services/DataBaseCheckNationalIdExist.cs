﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSWheatAdmin.DataBase.DbContexts;
using SSWheatAdmin.Models;

namespace SSWheatAdmin.DataBase.Services
{
    public class DataBaseCheckNationalIdExist
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseCheckNationalIdExist(InvestmentFundDbContextFactory investmentFundDbContextFactory)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
        }

        public async Task<bool> CheckNationalIdExist(PeoplesModel people)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var existingPeople = await context.Peoples
                    .Where(r => r.NationalId == people.NationalId)
                    .FirstOrDefaultAsync();
                return existingPeople != null;
            }
        }
    }
}