using Lab2_Dementiev.Classes.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Dementiev.Classes
{
    internal class XMLContext
    {
        private IStrategy? strategy;
        public string? path;

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public List<Service> ExecuteStrategy()
        {
            if(string.IsNullOrEmpty(path) || strategy == null)
            {
                return null;
            }
            else
            {
                return strategy.Analyze(path);
            }
        }
    }
}
