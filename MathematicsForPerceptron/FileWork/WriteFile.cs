using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.FileWork
{
    internal class WriteFile
    {
        static Random random = new Random();
        public void SaveMatrices(List<List<List<double>>> matrices, string filePath)
        {
            var matricesInFile = new StringBuilder();

            foreach (var mat in matrices)
            {
                matricesInFile.AppendLine($"{mat.First().Count};{mat.Count}");
                foreach (var mat2 in mat)
                {
                    matricesInFile.AppendLine(string.Join(";", mat2));
                }
                matricesInFile.AppendLine();
            }

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.Write(matricesInFile.ToString());
            }
        }

        public void CreateNewMatrices(string filePath, double minForCoeffic, double maxForCoeffic, List<int> layers)
        {
            var matricesInFile = new StringBuilder();

            for (int l = 0; l < layers.Count - 1; l++)
            {
                matricesInFile.AppendLine($"{layers[l]};{layers[l + 1]}");
                for (int i = 0; i < layers[l + 1]; i++) //количество в ПРАВОМ слое
                {
                    var list = new List<double>();
                    for (int j = 0; j < layers[l]; j++) //количество в ЛЕВОМ слое
                    {
                        list.Add((random.NextDouble() * (maxForCoeffic - minForCoeffic) + minForCoeffic));
                    }
                    matricesInFile.AppendLine(string.Join(";", list));
                }
                matricesInFile.AppendLine();
            }

            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.Write(matricesInFile.ToString());
            }
        }
    }
}
