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
    public class ReportWindowViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly ICalculationsService _calculationsService;
        #endregion

        #region BINDING PROPERTIES
        public DateTime? SelectedStartDate { get; set; }
        public DateTime? SelectedEndDate { get; set; }

        //TextBox values
        private decimal _totalIncomes;
        public decimal TotalIncomes
        {
            get => _totalIncomes;
            set
            {
                _totalIncomes = value;
                OnPropertyChanged(nameof(TotalIncomes));
            }
        }

        private decimal _totalExpenses;
        public decimal TotalExpenses
        {
            get => _totalExpenses;
            set
            {
                _totalExpenses = value;
                OnPropertyChanged(nameof(TotalExpenses));
            }
        }

        private decimal _totalProfit;
        public decimal TotalProfit
        {
            get => _totalProfit;
            set
            {
                _totalProfit = value;
                OnPropertyChanged(nameof(TotalProfit));
            }
        }
        #endregion

        public ReportWindowViewModel()
        {
            _calculationsService = new CalculationService();
        }

        #region COMMANDS
        public RelayCommand CountProfitCommand
        {
            get => new RelayCommand(async _ =>
            {
                try
                {
                    var startDate = SelectedStartDate.Value.ToDateOnly();
                    var endDate = SelectedEndDate.Value.ToDateOnly();

                    var report = await _calculationsService.CountProfitAsync(startDate, endDate);

                    TotalIncomes = report.TotalIncomes;
                    TotalExpenses = report.TotalExpenses;
                    TotalProfit = TotalIncomes - TotalExpenses;
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
