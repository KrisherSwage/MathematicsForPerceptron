using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron.Back
{
    internal class BackWork
    {
        private List<double> errors = new List<double>();
        public List<double> Errors { get { return errors; } set { errors = value; } }

        public double Error;

        public List<List<double>> Backpropagation(StartData sd, List<List<double>> vn, List<double> expectedRes)
        {
            var actvFunc = new ActivationFunctions();


            var eOutput = new List<double>();
            for (int i = 0; i < expectedRes.Count; i++)
            {
                var e = vn.Last()[i] - expectedRes[i];
                eOutput.Add(e);
            }
            var sumE = eOutput.Sum();

            //Errors.Add(sumE); // для отчета
            Error = sumE;


            var deltes = new List<List<double>>();

            var dList = new List<double>();

            for (int j = 0; j < sd.Layers.Last(); j++)
            {
                var value = vn.Last()[j];

                //var derValue = 1.0 - (1.0 / (1.0 + Math.Exp(-value)));
                var derValue = actvFunc.SigmoidAndDerivative().Item2(value);

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
                    //var derValue = 1.0 - (1.0 / (1.0 + Math.Exp(-value)));
                    var derValue = actvFunc.SigmoidAndDerivative().Item2(value);

                    var delta = eList[j] * value * derValue;
                    deltesList.Add(delta);
                }

                deltes.Add(deltesList);
            }

            deltes.Reverse();

            return deltes;
        }

        public void ChangeOfScales(StartData sd, double learningRate, List<List<double>> vn, List<List<double>> gradients)
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

        private List<double> MultiplyVectorMtx(List<double> vector, List<List<double>> matrix)
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
