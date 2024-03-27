using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.FileWork
{
    internal class ReadFile
    {

        public List<List<List<double>>> LoadMatrices(string filePath)
        {
            var matrices = new List<List<List<double>>>();

            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    //var layerNumbers = line.Split(';').Select(int.Parse).ToList();
                    var communicateMatrix = new List<List<double>>();
                    while (!string.IsNullOrWhiteSpace(line = file.ReadLine()))
                    {
                        var row = line.Split(';').Select(double.Parse).ToList();
                        communicateMatrix.Add(row);
                    }
                    matrices.Add(communicateMatrix);
                }
            }

            return matrices;
        }

    }
}
