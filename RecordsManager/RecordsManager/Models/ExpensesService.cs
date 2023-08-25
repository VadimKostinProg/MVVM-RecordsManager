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

            return expensesGrouped;
        }

        public async Task<List<ExpenseModel>> GetByDateAsync(string date)
        {
            var expenses = await _expensesRepository.GetAllAsync(expense => expense.Date.ToString() == date);

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
            await this.ValidateExpense(model);

            var expense = _mapper.Map<Expense>(model);
            await _expensesRepository.UpdateAsync(expense);
        }

        private async Task ValidateExpense(ExpenseModel model)
        {
            var modelDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day);

            var expenses = await _expensesRepository.GetAllAsync(expense =>
            expense.DateTimeProperty == modelDateTime && expense.Purpose == model.Purpose);

            if (expenses.Any())
            {
                throw new ArgumentException("Expense with such purpose for scuh date already exists.");
            }

            if(string.IsNullOrEmpty(model.Purpose))
            {
                throw new ArgumentNullException("Purpose of expense cannot be blank.");
            }

            if(model.Price <= 0)
            {
                throw new ArgumentException("Price of expense must be positive.");
            }
        }
    }
}
