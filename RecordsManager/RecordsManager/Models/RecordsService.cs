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
    public class RecordsService : IRecordsService
    {
        private readonly IRepository<Record> _recordsRepository;
        private readonly IMapper _mapper;

        public RecordsService()
        {
            _recordsRepository = new RepositoryBase<Record>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Record, RecordModel>().ReverseMap();
            })
            .CreateMapper();
        }

        public async Task CreateAsync(RecordModel model)
        {
            await this.ValidateRecord(model);

            var record = _mapper.Map<Record>(model);
            await _recordsRepository.AddAsync(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            bool result = await _recordsRepository.DeleteAsync(id);

            if(!result)
            {
                throw new KeyNotFoundException("Record with such id not found.");
            }
        }

        public async Task<List<RecordGroupedModel>> GetAllGroupedAsync()
        {
            var records = await _recordsRepository.GetAllAsync();

            var recordsGrouped = records
                .OrderBy(x => x.Time)
                .GroupBy(x => x.Date)
                .Select(group => new RecordGroupedModel()
                {
                    Date = group.Key.ToString(),
                    TimeGrouped = string.Join("; ", group.Select(record => record.Time.ToString())),
                    TotalPrice = group.Sum(record => record.Price)
                })
                .ToList();

            this.FillEmptyDates(recordsGrouped);

            return recordsGrouped.OrderBy(record => record.Date.ToDateOnly()).ToList();
        }

        public async Task<List<RecordModel>> GetByDateAsync(string date)
        {
            var recordDateTime = Convert.ToDateTime(date);
            var records = 
                (await _recordsRepository.GetAllAsync(record => 
                record.DateTimeProperty.Date == recordDateTime))
            .OrderBy(record => record.Time)
            .ToList();

            return _mapper.Map<List<RecordModel>>(records);
        }

        public async Task<RecordModel> GetByIdAsync(Guid id)
        {
            var record = await _recordsRepository.GetByIdAsync(id);

            if(record is null)
            {
                throw new KeyNotFoundException("Record with such Id not found.");
            }

            return _mapper.Map<RecordModel>(record);
        }

        public async Task UpdateAsync(RecordModel model)
        {
            //Check for existance of the record with passed Id
            if (await _recordsRepository.GetByIdAsync(model.Id) is null)
            {
                throw new KeyNotFoundException("Record with such id is not found.");
            }

            await this.ValidateRecord(model);

            var record = _mapper.Map<Record>(model);
            await _recordsRepository.UpdateAsync(record);
        }

        private void FillEmptyDates(List<RecordGroupedModel> recordsGrouped)
        {
            if (!recordsGrouped.Any()) return;

            var startDate = recordsGrouped.Min(record => record.Date.ToDateOnly());
            var endDate = recordsGrouped.Max(record => record.Date.ToDateOnly());

            for(DateOnly date = startDate; date < endDate; date = date.AddDays(1))
            {
                if (recordsGrouped.Any(record => record.Date.ToDateOnly() == date)) continue;

                var newRecordGrouped = new RecordGroupedModel()
                {
                    Date = date.ToString(),
                    TimeGrouped = string.Empty,
                    TotalPrice = 0,
                };

                recordsGrouped.Add(newRecordGrouped);
            }
        }

        //Method for validating the record
        private async Task ValidateRecord(RecordModel model)
        {
            //Check whether the record with the same date and time already exists
            var modelDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day,
                model.Time.Hour, model.Time.Minute, model.Time.Second);

            var records = await _recordsRepository.GetAllAsync(record => record.DateTimeProperty == modelDateTime);

            if(records.Any(record => record.Id != model.Id))
            {
                throw new ArgumentException("Record with such date and time already exists.");
            }

            //Check for null or empty name
            if(string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Customer name cannot be blank.");
            }

            //Check for negative price
            if(model.Price <= 0)
            {
                throw new ArgumentException("Price of procedure must be positive.");
            }
        }
    }
}
