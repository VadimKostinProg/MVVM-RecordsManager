using RecordsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.ServiceContracts
{
    /// <summary>
    /// Service for calculation incomes, expenses and profit.
    /// </summary>
    public interface ICalculationsService
    {
        /// <summary>
        /// Method for counting total incomes, expenses and profit between the passed dates.
        /// </summary>
        /// <param name="startDate">Start date of the range.</param>
        /// <param name="endDate">End date of the range.</param>
        /// <returns>Report with information of total incomes, expenses and profit.</returns>
        Task<ReportModel> CountProfitAsync(DateOnly startDate, DateOnly endDate); 
    }
}
