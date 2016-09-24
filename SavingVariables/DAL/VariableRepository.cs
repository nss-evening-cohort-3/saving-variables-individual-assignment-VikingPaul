using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariableRepository
    {
        public VariableRepository()
        {
            Context = new VariableContext();
        }

        public VariableRepository(VariableContext _context)
        {
            Context = _context;
        }

        public VariableContext Context { get; set; }

        public void Add(Variables var)
        {
            Context.Vars.Add(var);
            Context.SaveChanges();
        }

        public Variables GetVar(string v)
        {
            List<Variables> vars = this.GetVar();
            foreach (var var in vars)
            {
                if (var.Var == v)
                {
                    return var;
                }
            }
            return null;
        }

        public Variables Delete(string v)
        {
            Variables deleteThis = GetVar(v);
            if (deleteThis != null)
            {
                Context.Vars.Remove(deleteThis);
                Context.SaveChanges();
            }
            return deleteThis;
        }

        public List<Variables> GetVar()
        {
            return Context.Vars.ToList();
        }
    }
}
