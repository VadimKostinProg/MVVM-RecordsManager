using DataAccess.Entities;
using DataAccess.Repositories;
using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class CalculationService : ICalculationsService
    {
        private readonly IRepository<Record> _recordsRepository;
        private readonly IRepository<Expense> _expensesRepository;

        public CalculationService()
        {
            _recordsRepository = new RepositoryBase<Record>();
            _expensesRepository = new RepositoryBase<Expense>();
        }

        public async Task<ReportModel> CountProfitAsync(DateOnly startDate, DateOnly endDate)
        {
            if(startDate > endDate)
            {
                throw new ArgumentException("Start date must be less or equel the end date.");
            }

            var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            var endDateTime = endDate.ToDateTime(TimeOnly.MinValue).AddDays(1);

            var recrods = await _recordsRepository.GetAllAsync(record => 
            record.DateTimeProperty >= startDateTime && record.DateTimeProperty < endDateTime);

            var expenses = await _expensesRepository.GetAllAsync(expense =>
            expense.DateTimeProperty >= startDateTime && expense.DateTimeProperty < endDateTime);

            var report = new ReportModel()
            {
                TotalIncomes = recrods.Sum(record => record.Price),
                TotalExpenses = expenses.Sum(expense => expense.Price)
            };

            return report;
        }
    }
}
