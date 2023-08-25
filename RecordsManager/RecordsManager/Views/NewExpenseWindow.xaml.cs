using RecordsManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecordsManager.Views
{
    /// <summary>
    /// Логика взаимодействия для NewExpenseWindow.xaml
    /// </summary>
    public partial class NewExpenseWindow : Window
    {
        public NewExpenseWindow()
        {
            InitializeComponent();

            DataContext = new NewExpenseViewModel();
        }
    }
}
