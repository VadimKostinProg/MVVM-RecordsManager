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

namespace RecordsManager.Services
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
                .OrderBy(x => x.Date)
                .GroupBy(x => x.Date)
                .Select(group => new RecordGroupedModel()
                {
                    Date = group.Key.ToString(),
                    TimeGrouped = string.Join("; ", group.Select(record => record.Time.ToString("dd:mm"))),
                    TotalPrice = group.Sum(record => record.Price)
                })
                .ToList();

            return recordsGrouped;
        }

        public async Task<List<RecordModel>> GetByDateAsync(string date)
        {
            var records = await _recordsRepository.GetAllAsync(record => record.Date.ToString() == date);

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
            var record = _mapper.Map<Record>(model);
            await _recordsRepository.UpdateAsync(record);
        }
    }
}
