using RecordsManager.Commands;
using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecordsManager.ViewModels
{
    public class ExpensesWindowViewModel : ViewModelBase
    {
        #region PRIVATE FIELDS
        private readonly IExpensesService _expenseService;
        private readonly string _date;
        #endregion

        #region BINDING PROPERTIES
        private ObservableCollection<ExpenseModel> _expenses;
        public ObservableCollection<ExpenseModel> Expenses
        {
            get => _expenses;
            set
            {
                _expenses = value;

                OnPropertyChanged(nameof(Expenses));
            }
        }

        private ExpenseModel _selectedExpense;
        public ExpenseModel SelectedExpense
        {
            get => _selectedExpense;
            set
            {
                _selectedExpense = value;

                IsEditAllowed = _selectedExpense != null;

                OnPropertyChanged(nameof(SelectedExpense));
                OnPropertyChanged(nameof(Purpose));
                OnPropertyChanged(nameof(Price));
            }
        }

        private bool _isEditAllowed = false;
        public bool IsEditAllowed
        {
            get => _isEditAllowed;
            set
            {
                _isEditAllowed = value;
                OnPropertyChanged(nameof(IsEditAllowed));
            }
        }

        //TextBox values
        public string? Purpose
        {
            get => SelectedExpense?.Purpose;
            set => SelectedExpense.Purpose = value;
        }

        public decimal? Price
        {
            get => SelectedExpense?.Price;
            set => SelectedExpense.Price = value.Value;
        }
        #endregion

        public ExpensesWindowViewModel(string date)
        {
            _date = date;

            _expenseService = new ExpensesService();

            this.UpdateCollection().GetAwaiter().GetResult();

            EventSingleton.Instance.ExpensesCollectionChanged += UpdateCollection;
        }

        #region COMMANDS
        public RelayCommand UpdateExpenseCommand
        {
            get => new RelayCommand(async _ =>
            {
                try
                {
                    await _expenseService.UpdateAsync(SelectedExpense);
                    SelectedExpense = null;
                    await EventSingleton.Instance.InvokeExpensesCollectionChangedAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public RelayCommand DeleteExpenseCommand
        {
            get => new RelayCommand(async _ =>
            {
                try
                {
                    var result = MessageBox.Show("Are you shure you want to delete this expense?", string.Empty, MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No) return;

                    await _expenseService.DeleteAsync(SelectedExpense.Id);
                    SelectedExpense = null;
                    await EventSingleton.Instance.InvokeExpensesCollectionChangedAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS
        private async Task UpdateCollection()
        {
            var expenses = await _expenseService.GetByDateAsync(_date);

            Expenses = new ObservableCollection<ExpenseModel>(expenses);
        }
        #endregion
    }
}
