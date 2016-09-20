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
        public Expression (string expression)
        {
            string pattern = @"^(\w)\s?(=)\s?(\-?\d)$";
            Regex rgx = new Regex(pattern);
            VariableRepository repo = new VariableRepository();
            if (rgx.IsMatch(expression))
            {
                array = Regex.Split(expression, pattern);
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
            }
        }

        internal string Response()
        {
            return array[0];
        }
    }
}
