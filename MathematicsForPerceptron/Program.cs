using MathematicsForPerceptron.Back;
using MathematicsForPerceptron.Forward;
using Perceptron.FileWork;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace MathematicsForPerceptron
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lambda = 0.7;
            var countEpochs = 100000;

            var fileName = "matrices_231.txt";
            var matrixFilePath = Path.Combine("..", "..", "..", "FileWork", "Files", fileName);

            var layers = new List<int>() { 2, 3, 1 };

            var writeFile = new WriteFile();

            var minForCoeffic = -1.0;
            var maxForCoeffic = 1.0;
            //writeFile.CreateNewMatrices(matrixFilePath, minForCoeffic, maxForCoeffic, layers);

            var startData = new StartData(layers, matrixFilePath);

            var dataset = new Dictionary<List<double>, List<double>>()
            {
                { new List<double>() {0,0}, new List<double>() {0}},
                { new List<double>() {0,1}, new List<double>() {1}},
                { new List<double>() {1,0}, new List<double>() {1}},
                { new List<double>() {1,1}, new List<double>() {0}},
            };


            var training = new NeuralNetworkTraining();
            training.Training(dataset, lambda, countEpochs, startData);

            var checkRes = new CheckingResult();
            checkRes.Check(dataset, startData);


        }




    }
}
