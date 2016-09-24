using SavingVariables.DAL;
using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Expression
    {
        string[] array = new string[] { "Command Not Found" };
        public Expression (string expression, string lastq)
        {
            string patternSave = @"^(\w)\s?(=)\s?(\-?\d)$";
            string patternRemove = @"^(remove|delete|clear)\s(\w)$";
            string patternRemoveAll = @"^(remove|delete|clear)\s(all)$";
            string patternShowAll = @"^(show)\s(all)$";
            string patternQuit = @"^(exit|quit)$";
            string pattern = @"^(\w)$";
            Regex rgx = new Regex(pattern);
            Regex rgxSave = new Regex(patternSave);
            Regex rgxRemove = new Regex(patternRemove);
            Regex rgxRemoveAll = new Regex(patternRemoveAll);
            Regex rgxShowAll = new Regex(patternShowAll);
            Regex rgxQuit = new Regex(patternQuit);
            VariableRepository repo = new VariableRepository();
            if (rgxRemoveAll.IsMatch(expression))
            {
                List<Variables> varList = repo.GetVar();
                foreach(var i in varList)
                {
                    repo.Delete(i.Var);
                }
                array[0] = "ALL variables have been deleted";
            } else if (rgxShowAll.IsMatch(expression))
            {
                List<Variables> varList = repo.GetVar();
                array[0] = "Var | Value\r\n-----------\r\n";
                foreach (var i in varList)
                {
                    array[0] += "| " + i.Var + " | " + i.Value + " |\r\n";
                }

            } else if (rgxQuit.IsMatch(expression))
            {
                array[0] = "goodbye cruel world";
            } else if (rgxRemove.IsMatch(expression))
            {
                array = Regex.Split(expression, patternRemove);
                Variables deleteThis = repo.GetVar(array[2]);
                repo.Delete(deleteThis.Var);
                array[0] = "Deleted Var";
            } else if (rgxSave.IsMatch(expression))
            {
                array = Regex.Split(expression, patternSave);
                Variables variable = repo.GetVar(array[1]);
                if (variable != null)
                {
                    array[0] = "There is already a variable " + array[1];
                } else
                {
                    array[0] = "Variable saved!";
                    Variables addThis = new Variables { Value = array[3], Var = array[1] };
                    repo.Add(addThis);
                }
            } else if (expression == "lastq")
            {
                array[0] = lastq;
            } else if (expression.Length == 1 && rgx.IsMatch(expression))
            {
                Variables var = repo.GetVar(expression);
                if (var != null)
                {
                    array[0] = "= " + var.Value;
                } else
                {
                    array[0] = "No variable by that name";
                }
            }
            
        }

        internal string Response()
        {
            return array[0];
        }
    }
}
