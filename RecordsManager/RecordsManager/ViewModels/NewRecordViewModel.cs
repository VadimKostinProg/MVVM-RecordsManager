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
    public class NewRecordViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IRecordsService _recordService;
        #endregion

        #region BINDING PROPERTIES
        public DateTime Date { get; set; } = DateTime.Now;
        public string Time { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Procedure { get; set; }
        public decimal Price { get; set; }
        #endregion

        public NewRecordViewModel()
        {
            _recordService = new RecordsService();
        }

        #region COMMANDS
        public RelayCommand AddNewRecordCommand
        {
            get => new RelayCommand(async obj =>
            {
                try
                {
                    var window = obj as Window;

                    var record = new RecordModel()
                    {
                        Date = new DateOnly(this.Date.Year, this.Date.Month, this.Date.Day), 
                        Time = this.ConvertToTimeOnly(this.Time),
                        Name = this.Name,
                        Email = this.Email,
                        PhoneNumber = this.PhoneNumber,
                        Address = this.Address,
                        Procedure = this.Procedure,
                        Price = this.Price
                    };

                    await _recordService.CreateAsync(record);

                    await EventSingleton.Instance.InvokeRecordsCollectionChanged();

                    window.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        #endregion

        #region PRIVATE MEMBERS
        private TimeOnly ConvertToTimeOnly(string time)
        {
            if(time.Count(chr => chr == ':') != 1 || time.Length > 5 || time.Length < 4)
            {
                throw new ArgumentException("Incorrect format of time");
            }

            var args = time.Split(':').Select(arg => int.Parse(arg)).ToArray();

            return new TimeOnly(args[0], args[1]);
        }
        #endregion
    }
}
