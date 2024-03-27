using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForPerceptron
{
    internal class ActivationFunctions
    {

        public (Func<double, double>, Func<double, double>) SigmoidAndDerivative()
        {
            Func<double, double> sigmoid = x => 1.0 / (1.0 + Math.Exp(-x));

            Func<double, double> derivative = x =>
            {
                var sigmoidValue = sigmoid(x);
                return sigmoidValue * (1.0 - sigmoidValue);
            };

            return (sigmoid, derivative);
        }

    }
}
