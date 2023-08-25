using RecordsManager.Commands;
using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecordsManager.ViewModels
{
    public class NewExpenseViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IExpensesService _expensesService;
        #endregion

        #region BINDING PROPERTIES
        public DateTime Date { get; set; } = DateTime.Now;
        public string Purpose { get; set; }
        public decimal Price { get; set; }
        #endregion

        public NewExpenseViewModel()
        {
            _expensesService = new ExpensesService();
        }

        #region COMMANDS
        public RelayCommand AddNewExpenseCommand
        {
            get => new RelayCommand(async obj =>
            {
                try
                {
                    var window = obj as Window;

                    var expense = new ExpenseModel()
                    {
                        Date = new DateOnly(this.Date.Year, this.Date.Month, this.Date.Day),
                        Purpose = this.Purpose,
                        Price = this.Price
                    };

                    await _expensesService.CreateAsync(expense);

                    await EventSingleton.Instance.InvokeExpensesCollectionChanged();

                    window.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        #endregion
    }
}
