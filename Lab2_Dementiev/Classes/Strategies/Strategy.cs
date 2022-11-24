using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Dementiev.Classes.Strategies
{
    internal interface IStrategy
    {
        public abstract List<Service> Analyze(string path);
    }
}
