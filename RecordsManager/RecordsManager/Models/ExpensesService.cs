using AutoMapper;
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
    public class ExpensesService : IExpensesService
    {
        private readonly IRepository<Expense> _expensesRepository;
        private readonly IMapper _mapper;

        public ExpensesService()
        {
            _expensesRepository = new RepositoryBase<Expense>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Expense, ExpenseModel>().ReverseMap();
            })
            .CreateMapper();
        }

        public async Task CreateAsync(ExpenseModel model)
        {
            await this.ValidateExpense(model);

            var expense = _mapper.Map<Expense>(model);
            await _expensesRepository.AddAsync(expense);
        }

        public async Task DeleteAsync(Guid id)
        {
            bool result = await _expensesRepository.DeleteAsync(id);
            if(!result)
            {
                throw new KeyNotFoundException("Expense with such Id is not found.");
            }
        }

        public async Task<List<ExpenseGroupedModel>> GetAllGroupedAsync()
        {
            var expenses = await _expensesRepository.GetAllAsync();

            var expensesGrouped = expenses
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Date)
                .Select(group => new ExpenseGroupedModel()
                {
                    Date = group.Key.ToString(),
                    PurposeGrouped = string.Join(", ", group.Select(record => record.Purpose)),
                    TotalPrice = group.Sum(x => x.Price)
                })
                .ToList();

            this.FillEmptyGroupedExpenses(expensesGrouped);

            return expensesGrouped.OrderBy(expense => expense.Date.ToDateOnly()).ToList();
        }

        public async Task<List<ExpenseModel>> GetByDateAsync(string date)
        {
            var expenseDateTime = Convert.ToDateTime(date);
            var expenses = await _expensesRepository.GetAllAsync(expense =>
                expense.DateTimeProperty == expenseDateTime);

            return _mapper.Map<List<ExpenseModel>>(expenses);
        }

        public async Task<ExpenseModel> GetByIdAsync(Guid id)
        {
            var expense = await _expensesRepository.GetByIdAsync(id);

            if(expense is null)
            {
                throw new KeyNotFoundException("Expense with such Id is not found.");
            }

            return _mapper.Map<ExpenseModel>(expense);
        }

        public async Task UpdateAsync(ExpenseModel model)
        {
            //Check for existance of the expense with passed Id
            if (await _expensesRepository.GetByIdAsync(model.Id) is null)
            {
                throw new KeyNotFoundException("Expense with such id is not found.");
            }

            await this.ValidateExpense(model);

            var expense = _mapper.Map<Expense>(model);
            await _expensesRepository.UpdateAsync(expense);
        }

        //Method for filling empty expenses grouped by date.
        private void FillEmptyGroupedExpenses(List<ExpenseGroupedModel> expensesGrouped)
        {
            if (!expensesGrouped.Any()) return;

            var startDate = expensesGrouped.Min(record => record.Date.ToDateOnly());
            var endDate = expensesGrouped.Max(record => record.Date.ToDateOnly());

            for (DateOnly date = startDate; date < endDate; date = date.AddDays(1))
            {
                if (expensesGrouped.Any(expense => expense.Date.ToDateOnly() == date)) continue;

                var newExpenseGrouped = new ExpenseGroupedModel()
                {
                    Date = date.ToString(),
                    PurposeGrouped = string.Empty,
                    TotalPrice = 0,
                };

                expensesGrouped.Add(newExpenseGrouped);
            }
        }

        //Method for validation the expense
        private async Task ValidateExpense(ExpenseModel model)
        {
            //Check whether the expense with such date and purpose already exists
            var modelDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day);

            var expenses = await _expensesRepository.GetAllAsync(expense =>
            expense.DateTimeProperty == modelDateTime && expense.Purpose == model.Purpose);

            if (expenses.Any(expense => expense.Id != model.Id))
            {
                throw new ArgumentException("Expense with such purpose for scuh date already exists.");
            }

            //Check for null or empty purpose
            if(string.IsNullOrEmpty(model.Purpose))
            {
                throw new ArgumentNullException("Purpose of expense cannot be blank.");
            }

            //Check for negative price
            if(model.Price <= 0)
            {
                throw new ArgumentException("Price of expense must be positive.");
            }
        }
    }
}
