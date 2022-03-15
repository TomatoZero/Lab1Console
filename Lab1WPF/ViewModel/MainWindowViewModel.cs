using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab1WPF;

namespace Lab1WPF.ViewModel
{
    internal class MainWindowViewModel : BasicViewModel
    {
        private string function;
        private string min;
        private string max;
        private ICommand startComand;
        private EquationWithOneUnknown equation;
        private List<double> result;
        private MainWindow mainWindow;

        public string Function
        { 
            get => function; 
            set => function = value; 
        }
        public string Min 
        {
            get => min; 
            set => min = value;
        }
        public string Max
        {
            get => max;
            set => max = value;
        }
                

        public ICommand StartComand 
        {
            get 
            {
                if(startComand == null) 
                {
                    startComand = new StartComand(this);
                }
                return startComand;
            }
        }

        internal EquationWithOneUnknown Equation 
        { 
            get => equation; 
            set => equation = value; 
        }
        public List<double> Result 
        { 
            get => result;
            set 
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public MainWindow MainWindow 
        { 
            get => mainWindow; 
            set => mainWindow = value; 
        }

        public MainWindowViewModel()
        {
            Equation = new EquationWithOneUnknown();
        }

        public MainWindowViewModel(MainWindow window)
        {
            Equation = new EquationWithOneUnknown();
            mainWindow = window;

            mainWindow.Start.Click += Start_Click;
        }

        private void Start_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int x1 = Convert.ToInt32(Min);
            int x2 = Convert.ToInt32(Max);
            Result = Equation.SolveTheEquation(Function, x1, x2);
            mainWindow.listBox.ItemsSource = result;
        }
    }

    abstract class MyComand : ICommand
    {
        protected MainWindowViewModel mainWindow;

        public MyComand(MainWindowViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public event EventHandler? CanExecuteChanged;

        public abstract bool CanExecute(object? parameter);

        public abstract void Execute(object? parameter);
    }

    class StartComand : MyComand
    {
        public StartComand(MainWindowViewModel mainWindow) : base(mainWindow)
        {
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            int x1 = Convert.ToInt32(mainWindow.Min);
            int x2 = Convert.ToInt32(mainWindow.Max);
            mainWindow.Result = mainWindow.Equation.SolveTheEquation(mainWindow.Function, x1, x2);
        }
    }
}
