using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using RecordsManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.ViewModels
{
    public class MainWindowViewModel
    {
        #region SERVICES
        private readonly IRecordsService _recordsService;
        private readonly IExpensesService _expensesService;
        #endregion

        #region BINDING PROPERTIES
        public List<RecordGroupedModel> Records { get; set; }
        public List<ExpenseGroupedModel> Expenses { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            _recordsService = new RecordsService();
            _expensesService = new ExpensesService();

            Records = _recordsService.GetAllGroupedAsync().GetAwaiter().GetResult();
            Expenses = _expensesService.GetAllGroupedAsync().GetAwaiter().GetResult();
        }
    }
}
