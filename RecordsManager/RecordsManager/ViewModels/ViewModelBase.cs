using RecordsManager.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecordsManager.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand
        {
            get => new RelayCommand(obj =>
            {
                try
                {
                    var window = obj as Window;

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
