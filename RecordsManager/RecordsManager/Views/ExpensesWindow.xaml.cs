using RecordsManager.ViewModels;
using System.Windows;

namespace RecordsManager.Views
{
    /// <summary>
    /// Логика взаимодействия для ExpensesWindow.xaml
    /// </summary>
    public partial class ExpensesWindow : Window
    {
        public ExpensesWindow(string date)
        {
            InitializeComponent();

            this.Title = "Expenses on " + date;

            DataContext = new ExpensesWindowViewModel(date);
        }
    }
}
