using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron.Forward
{
    internal class ForwardWork
    {


        public List<List<double>> Forwardpropagation(StartData sd, List<double> inputs)
        {
            var valuesNeurons = new List<List<double>>();
            valuesNeurons.Add(inputs);

            var actvFunc = new ActivationFunctions();

            for (int i = 0; i < sd.Layers.Count - 1; i++)
            {
                var matrix = sd.Matrices[i];
                var vector = valuesNeurons[i];
                var newVector = MultiplyMtxVector(matrix, vector);

                for (int j = 0; j < newVector.Count; j++)
                {
                    //newVector[j] = (1.0 / (1.0 + Math.Exp(-newVector[j])));
                    newVector[j] = actvFunc.SigmoidAndDerivative().Item1(newVector[j]);
                }

                valuesNeurons.Add(newVector);
            }

            return valuesNeurons;
        }

        private List<double> MultiplyMtxVector(List<List<double>> matrix, List<double> vector)
        {
            if (matrix[0].Count != vector.Count)
                throw new Exception($"\nРазмер строки матрицы отличается от размера вектора\nmtx.C = {matrix.Count}\tmtx[0].C = {matrix[0].Count}\tv.C = {vector.Count}");

            var vectorResult = new List<double>();

            foreach (var row in matrix)
            {
                var sum = 0.0;
                for (int i = 0; i < row.Count; i++)
                {
                    sum += row[i] * vector[i];
                }
                vectorResult.Add(sum);

            }

            return vectorResult;
        }

    }
}
