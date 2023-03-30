using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    public class Calculator
    {
        private int result = 0;
        public void Operation(char _operator, int operand) 
        {
            switch (_operator)
            {
                case '+':
                    result += operand;
                    break;
                case '-':
                    result -= operand;
                    break;
                case '*':
                    result += operand;
                    break;
                case '/':
                    result += operand;
                    break;
            }
            string buffer = new string(("Current value = {0} (following {1}{2})", result, _operator, operand).ToString().ToCharArray());
            Console.WriteLine(buffer);
        }
    }

    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }

    public class CalculatorCommand : ICommand
    {
        private char _operator;
        private int operand;
        Calculator calculator = new Calculator();
        public CalculatorCommand(Calculator calculator, char _operator, int operand)
        {
            this.calculator = calculator;
            this.operand = operand;
            this._operator = _operator;
        }
        public void Execute()
        {
            calculator.Operation(_operator, operand);
        }

        public void UnExecute()
        {
            calculator.Operation(Undo(_operator), operand);
        }
        private char Undo(char _opearator)
        {
            char undo;
            switch (_operator)
            {
                case '+':
                    undo = '-';
                    break;
                    case '-':
                    undo = '+';
                    break;
                    case '*':
                    undo = '/';
                    break;
                    case '/':
                    undo = '*';
                    break;
                    default:
                    undo = ' ';
                    break;
            }
            return undo;
        }
    }
    public class User
    {
        private List<ICommand> commands = new List<ICommand>();
        private int current = 0;
        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels",levels);
            for (int i=0;i< levels;i++)
            {
                if(current<commands.Count -1)
                {
                    commands[current++].Execute();
                }
            }
        }
        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels", levels);
            for(int i=0;i< levels; i++)
            {
                if(current<commands.Count -1) { commands[--current].UnExecute(); }
            }
        }
        public void Compute(ICommand command)
        {
            command.Execute();
            commands.Add(command);
            current++;
        }
    }
    internal class Program
    {
        static void Main()
        {

        }
    }
}
