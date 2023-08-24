using RecordsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.ServiceContracts
{
    public interface IRecordsService : ICRUDService<RecordModel, RecordGroupedModel>
    {
    }
}
