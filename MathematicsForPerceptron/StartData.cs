using Perceptron.FileWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron
{
    internal class StartData
    {
        Random random = new Random();

        public List<int> Layers { get; set; }

        public List<List<List<double>>> Matrices { get; set; }

        public string FilePath { get; set; }

        public StartData(List<int> layers, string filePath)
        {
            //var layers = new List<int>() { 2, 3, 1 };
            Layers = layers;
            FilePath = filePath;
            //var minForCoeffic = -1.0;
            //var maxForCoeffic = 1.0;
            //var matices = new List<List<List<double>>>();
            //for (int l = 0; l < layers.Count - 1; l++)
            //{
            //    var matrix = new List<List<double>>();
            //    for (int i = 0; i < layers[l + 1]; i++) //количество в ПРАВОМ слое
            //    {
            //        matrix.Add(new List<double>());
            //        for (int j = 0; j < layers[l]; j++) //количество в ЛЕВОМ слое
            //        {
            //            matrix.Last().Add((random.NextDouble() * (maxForCoeffic - minForCoeffic) + minForCoeffic));
            //        }
            //    }
            //    matices.Add(matrix);
            //}

            var readF = new ReadFile();
            Matrices = readF.LoadMatrices(filePath);

        }


    }
}
