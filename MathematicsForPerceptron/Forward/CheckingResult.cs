using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron.Forward
{
    internal class CheckingResult
    {

        public void Check(Dictionary<List<double>, List<double>> dataset, StartData startData)
        {
            var forward = new ForwardWork();
            foreach (var data in dataset)
            {
                //var valuesNeurons = Forwardpropagation(startData, data.Key);
                var valuesNeurons = forward.Forwardpropagation(startData, data.Key);
                foreach (var item in valuesNeurons.Last())
                {
                    Console.WriteLine($"{item} ");
                }
            }
        }
    }
}
