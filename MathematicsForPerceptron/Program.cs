using System;

namespace MathematicsForPerceptron
{
    internal class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            var startData = new StartData();

            var dataset = new Dictionary<List<double>, List<double>>()
            {
                { new List<double>() {0,0}, new List<double>() {0}},
                { new List<double>() {0,1}, new List<double>() {1}},
                { new List<double>() {1,0}, new List<double>() {1}},
                { new List<double>() {1,1}, new List<double>() {0}},
            };


            for (int i = 0; i < 100000; i++)
            {
                foreach (var data in dataset)
                {
                    var valuesNeurons = Forwardpropagation(startData, data.Key);
                    var gradients = Backpropagation(startData, valuesNeurons, data.Value);
                    ChangeOfScales(startData, 0.7, valuesNeurons, gradients);
                }
            }


            foreach (var data in dataset)
            {
                var valuesNeurons = Forwardpropagation(startData, data.Key);
                foreach (var item in valuesNeurons.Last())
                {
                    Console.WriteLine($"{item} ");
                }
            }
        }



        public static List<List<double>> Forwardpropagation(StartData sd, List<double> inputs)
        {
            var valuesNeurons = new List<List<double>>();
            valuesNeurons.Add(inputs);


            for (int i = 0; i < sd.Layers.Count - 1; i++)
            {
                var matrix = sd.Matrices[i];
                var vector = valuesNeurons[i];
                var newVector = MultiplyMtxVector(matrix, vector);

                for (int j = 0; j < newVector.Count; j++)
                {
                    newVector[j] = (1.0 / (1.0 + Math.Exp(-newVector[j])));
                }

                valuesNeurons.Add(newVector);
            }

            //foreach (var item in valuesNeurons.Last())
            //{
            //    Console.WriteLine(item);
            //}
            return valuesNeurons;
        }




        public static List<List<double>> Backpropagation(StartData sd, List<List<double>> vn, List<double> expectedRes)
        {
            var eOutput = new List<double>();
            for (int i = 0; i < expectedRes.Count; i++)
            {
                var e = vn.Last()[i] - expectedRes[i];
                eOutput.Add(e);
            }
            var sumE = eOutput.Sum();


            var deltes = new List<List<double>>();

            var dList = new List<double>();

            for (int j = 0; j < sd.Layers.Last(); j++)
            {
                var value = vn.Last()[j];
                var derValue = 1.0 - (1.0 / (1.0 + Math.Exp(-value)));
                var delta = sumE * value * derValue;

                dList.Add(delta);
            }
            deltes.Add(dList);

            for (int i = sd.Layers.Count - 2; i > 0; i--)
            {
                var eList = MultiplyVectorMtx(deltes.Last(), sd.Matrices[i]);

                var deltesList = new List<double>();
                for (int j = 0; j < eList.Count; j++)
                {
                    var value = vn[i][j];
                    var derValue = 1.0 - (1.0 / (1.0 + Math.Exp(-value)));

                    var delta = eList[j] * value * derValue;
                    deltesList.Add(delta);
                }

                deltes.Add(deltesList);
            }

            deltes.Reverse();

            return deltes;
        }

        public static void ChangeOfScales(StartData sd, double learningRate, List<List<double>> vn, List<List<double>> gradients)
        {
            var matrices = sd.Matrices;
            for (int i = 0; i < matrices.Count; i++)
            {
                var matrix = matrices[i];
                for (int j = 0; j < matrix.Count; j++)
                {
                    for (int k = 0; k < matrix[j].Count; k++)
                    {
                        var fx = vn[i][k];
                        var gradient = gradients[i][j];
                        var deltaW = learningRate * gradient * fx;

                        matrix[j][k] -= deltaW;
                    }
                }
            }
        }


        private static List<double> MultiplyMtxVector(List<List<double>> matrix, List<double> vector)
        {
            if (matrix[0].Count != vector.Count)
            {
                throw new Exception($"\nРазмер строки матрицы отличается от размера вектора\nmtx.C = {matrix.Count}\tmtx[0].C = {matrix[0].Count}\tv.C = {vector.Count}");
            }

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


        private static List<double> MultiplyVectorMtx(List<double> vector, List<List<double>> matrix)
        {
            var vectorResult = new List<double>();

            for (int i = 0; i < matrix[0].Count; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < vector.Count; j++)
                {
                    sum += vector[j] * matrix[j][i];
                }
                vectorResult.Add(sum);
            }

            return vectorResult;
        }

    }
}
