using RecordsManager.Commands;
using RecordsManager.Models;
using RecordsManager.ServiceContracts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecordsManager.ViewModels
{
    public class RecordsWindowViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IRecordsService _recordsService;
        private readonly string _date;
        #endregion

        #region BINDING PROPERTIES
        private ObservableCollection<RecordModel> _records;
        public ObservableCollection<RecordModel> Records
        {
            get => _records; 
            set
            {
                _records = value;

                OnPropertyChanged(nameof(Records));
            }
        }

        private RecordModel _selectedRecord;
        public RecordModel? SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                _selectedRecord = value;

                this.IsEditAllowed = _records != null;

                OnPropertyChanged(nameof(SelectedRecord));
                OnPropertyChanged(nameof(Time));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Phone));
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Procedure));
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
        public string? Time
        {
            get => SelectedRecord?.Time.ToString();
            set => SelectedRecord.Time = value.ToTimeOnly();
        }

        public string? Name
        {
            get => SelectedRecord?.Name;
            set => SelectedRecord.Name = value;
        }

        public string? Email
        {
            get => SelectedRecord?.Email;
            set => SelectedRecord.Email = value;
        }

        public string? Phone
        {
            get => SelectedRecord?.PhoneNumber;
            set => SelectedRecord.PhoneNumber = value;
        }

        public string? Address
        {
            get => SelectedRecord?.Address;
            set => SelectedRecord.Address = value;
        }

        public string? Procedure
        {
            get => SelectedRecord?.Procedure;
            set => SelectedRecord.Procedure = value;
        }

        public decimal? Price
        {
            get => SelectedRecord?.Price;
            set => SelectedRecord.Price = value.Value;
        }
        #endregion

        public RecordsWindowViewModel(string date)
        {
            _date = date;

            _recordsService = new RecordsService();

            this.UpdateCollection().GetAwaiter().GetResult();

            EventSingleton.Instance.RecordsCollectionChanged += UpdateCollection;
        }

        #region COMMANDS
        public RelayCommand UpdateRecordCommand
        {
            get => new RelayCommand(async _ =>
            {
                try
                {
                    await _recordsService.UpdateAsync(SelectedRecord);
                    SelectedRecord = null;
                    await EventSingleton.Instance.InvokeRecordsCollectionChangedAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public RelayCommand DeleteRecordCommand
        {
            get => new RelayCommand(async _ =>
            {
                try
                {
                    var result = MessageBox.Show("Are you shure you want to delete this record?", string.Empty, MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No) return;

                    await _recordsService.DeleteAsync(SelectedRecord.Id);
                    SelectedRecord = null;
                    await EventSingleton.Instance.InvokeRecordsCollectionChangedAsync();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS
        private async Task UpdateCollection()
        {
            var records = await _recordsService.GetByDateAsync(_date);

            Records = new ObservableCollection<RecordModel>(records);
        }
        #endregion
    }
}
