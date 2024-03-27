using MathematicsForPerceptron.Forward;
using Perceptron.FileWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron.Back
{
    internal class NeuralNetworkTraining
    {
        double minError = double.MaxValue;

        public void Training(Dictionary<List<double>, List<double>> dataset, double lambda, int countEpochs, StartData startData)
        {

            var forward = new ForwardWork();
            var training = new BackWork();

            var bufListErrors = new List<double>();

            for (int i = 0; i < countEpochs; i++)
            {

                foreach (var data in dataset)
                {
                    var valuesNeurons = forward.Forwardpropagation(startData, data.Key);
                    var gradients = training.Backpropagation(startData, valuesNeurons, data.Value);
                    training.ChangeOfScales(startData, lambda, valuesNeurons, gradients);

                    //bufListErrors.Add(training.Error);
                }

                //if (!EarlyShutdown(bufListErrors))
                //{
                //    Console.WriteLine($"{i}\n{countEpochs}");
                //    break;
                //}
                //bufListErrors.Clear();

            }
            var writeFile = new WriteFile();
            writeFile.SaveMatrices(startData.Matrices, startData.FilePath);


            ///*
            //var errorsForFile = training.Errors.Select(Math.Abs).ToList();
            //errorsForFile = errorsForFile
            //    .Select((x, i) => new { Index = i, Value = x })
            //    .GroupBy(x => x.Index / dataset.Count)
            //    .Select(x => x.Average(v => v.Value))
            //    .ToList();
            //var errors = new StringBuilder();
            //errors.AppendLine(string.Join("\n", errorsForFile));
            //using (StreamWriter file = new StreamWriter(Path.Combine("..", "..", "..", "FileWork", "Files", "errors.csv")))
            //    file.Write(errors.ToString());
            //*/

        }

        private bool EarlyShutdown(List<double> errorsOneEpoch)
        {
            var bufError = 0.0;
            for (int i = 0; i < errorsOneEpoch.Count; i++)
            {
                bufError += Math.Abs(errorsOneEpoch[i]);
            }
            bufError /= errorsOneEpoch.Count;

            if (bufError < minError)
            {
                minError = bufError;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
