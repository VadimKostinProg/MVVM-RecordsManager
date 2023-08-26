using RecordsManager.Commands;
using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using RecordsManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IRecordsService _recordsService;
        private readonly IExpensesService _expensesService;
        #endregion

        #region BINDING PROPERTIES
        private ObservableCollection<RecordGroupedModel> _records;
        public ObservableCollection<RecordGroupedModel> Records
        {
            get { return _records; }
            set
            {
                _records = value;
                OnPropertyChanged(nameof(Records));
            }
        }

        private ObservableCollection<ExpenseGroupedModel> _expenses;
        public ObservableCollection<ExpenseGroupedModel> Expenses
        {
            get { return _expenses; }
            set
            {
                _expenses = value;
                OnPropertyChanged(nameof(Expenses));
            }
        }

        public RecordGroupedModel SelectedGroupedRecord { get; set; }
        public ExpenseGroupedModel SelectedGroupedExpense { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            _recordsService = new RecordsService();
            _expensesService = new ExpensesService();

            this.UpdateRecordsCollections().GetAwaiter().GetResult();
            this.UpdateExpensesCollections().GetAwaiter().GetResult();

            EventSingleton.Instance.RecordsCollectionChanged += UpdateRecordsCollections;
            EventSingleton.Instance.ExpensesCollectionChanged += UpdateExpensesCollections;
        }

        #region COMMANDS
        /// <summary>
        /// Command for opening Report window.
        /// </summary>
        public RelayCommand OpenReportWindowCommand
        {
            get => new RelayCommand(obj =>
            {
                var window = new ReportWindow();

                window.ShowDialog();
            });
        }

        /// <summary>
        /// Command for opening New Record window.
        /// </summary>
        public RelayCommand OpenNewRecordWindowCommand
        {
            get => new RelayCommand(obj =>
            {
                var window = new NewRecordWindow();

                window.ShowDialog();
            });
        }

        /// <summary>
        /// Command for opening New Expense window.
        /// </summary>
        public RelayCommand OpenNewExpenseWindowCommand
        {
            get => new RelayCommand(obj =>
            {
                var window = new NewExpenseWindow();

                window.ShowDialog();
            });
        }

        public RelayCommand OpenRecordsWindowCommand
        {
            get => new RelayCommand(obj =>
            {
                var window = new RecordsWindow(SelectedGroupedRecord.Date);

                window.ShowDialog();
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS
        private async Task UpdateRecordsCollections()
        {
            var recordsList = await _recordsService.GetAllGroupedAsync();

            this.Records = new ObservableCollection<RecordGroupedModel>(recordsList);
        }

        private async Task UpdateExpensesCollections()
        {
            var expensesList = await _expensesService.GetAllGroupedAsync();

            this.Expenses = new ObservableCollection<ExpenseGroupedModel>(expensesList);
        }
        #endregion
    }
}