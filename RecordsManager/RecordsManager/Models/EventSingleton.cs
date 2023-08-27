using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    /// <summary>
    /// Singleton object for global events.
    /// </summary>
    public sealed class EventSingleton
    {
        private static Lazy<EventSingleton> _instance = new Lazy<EventSingleton>(() => new EventSingleton());
        public static EventSingleton Instance => _instance.Value;

        private EventSingleton() { }

        public delegate Task UpdateCollectionDelegate();

        public event UpdateCollectionDelegate RecordsCollectionChanged;
        public event UpdateCollectionDelegate ExpensesCollectionChanged;

        public async Task InvokeRecordsCollectionChangedAsync()
        {
            if(RecordsCollectionChanged != null)
            {
                await RecordsCollectionChanged();
            }
        }

        public async Task InvokeExpensesCollectionChangedAsync()
        {
            if (ExpensesCollectionChanged != null)
            {
                await ExpensesCollectionChanged();
            }
        }
    }
}
