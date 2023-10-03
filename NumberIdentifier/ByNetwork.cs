using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NumberIdentifier
{
    public delegate void NetworkStatusUpdate(int index, int count, string msg);
    public class ByNetwork
    {
        private int numLayers;
        public List<int> sizes;
        public List<double[,]> biases;
        public List<double[,]> weights;

        public event NetworkStatusUpdate statusChange;

        public ByNetwork(List<int> sizes)
        {
            this.numLayers = sizes.Count;
            this.sizes = sizes;
            this.biases = sizes.GetRange(1, sizes.Count - 1).Select(y => new double[y, 1]).ToList();

            List<int> newSize = new List<int>(sizes);
            newSize.Reverse();

            var range = newSize.GetRange(0, sizes.Count - 1);

            range.Reverse();

            this.weights = range
                .Select((y, x) => new double[y, sizes[x]]).ToList();



            Random rand = new Random();
            foreach (var weight in weights)
            {
                for (int i = 0; i < weight.GetLength(0); i++)
                {
                    for (int j = 0; j < weight.GetLength(1); j++)
                    {

                        //generates random numbers from a standard normal distribution (also known as a Gaussian distribution) with a mean of 0 and a standard deviation of 1.
                        double u1 = 1.0 - rand.NextDouble(); // Uniform random variable in (0, 1]
                        double u2 = 1.0 - rand.NextDouble();
                        double z1 = u1 * 2 - 1; //Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);


                        weight[i, j] = z1;
                    }
                }
            }

            foreach (var bias in biases)
            {
                for (int i = 0; i < bias.GetLength(0); i++)
                {
                    double u1 = 1.0 - rand.NextDouble(); // Uniform random variable in (0, 1]
                    double u2 = 1.0 - rand.NextDouble();
                    double z1 = u1 * 2 - 1;//Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                    bias[i, 0] = z1;
                }
            }
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }

        private double SigmoidPrime(double z)
        {
            return Sigmoid(z) * (1 - Sigmoid(z));
        }

        public double[,] FeedForward(double[,] a)
        {
            for (int i = 0; i < numLayers - 1; i++)
            {
                double[,] b = biases[i];
                double[,] w = weights[i];
                /*
                double[,] dot = MatrixDotProduct(w, a);
                double[,] sum = MatrixAddition(dot, b);

                a = ApplySigmoid(sum);*/

                a = ApplySigmoid(MatrixAddition(MatrixDotProduct(w, a), b));
            }
            return a;
        }

        //Stochastic Gradient Descent
        public void SGD(List<Tuple<double[,], double[,]>> trainingData, int epochs, int miniBatchSize, double eta, List<Tuple<double[,], double[,]>> testData = null)
        {
            int nTest = testData != null ? testData.Count : 0;
            int n = trainingData.Count;

            for (int j = 0; j < epochs; j++)
            {
                this.statusChange(j, epochs, $"Learning Epoch {j}");

                // introduces randomness and prevents the network from getting stuck in local minima
                trainingData = Shuffle(trainingData);
                var miniBatches = SplitIntoMiniBatches(trainingData, miniBatchSize);

                int batchIndex = 0;
                foreach (var miniBatch in miniBatches)
                {
                    UpdateMiniBatch(miniBatch, eta);
                    batchIndex++;

                    this.statusChange(j, epochs, $"Learning Epoch {j}, Batch {batchIndex}/{miniBatches.Count}");
                }

                if (testData != null)
                {
                    Console.WriteLine($"Epoch {j}: Evaluating {nTest}");
                    int correct = Evaluate(testData);
                    Console.WriteLine($"Epoch {j}: {correct} / {nTest}");
                    this.statusChange(j, epochs, $"Epoch {j}: {correct} / {nTest}");
                }
                else
                {
                    Console.WriteLine($"Epoch {j} complete");
                    this.statusChange(j, epochs, $"Epoch {j}: Epoch {j} complete");
                }
            }
        }

        private List<Tuple<double[,], double[,]>> Shuffle(List<Tuple<double[,], double[,]>> data)
        {
            Random rand = new Random();
            return data.OrderBy(x => rand.Next()).ToList();
        }

        private List<List<Tuple<double[,], double[,]>>> SplitIntoMiniBatches(List<Tuple<double[,], double[,]>> data, int batchSize)
        {
            List<List<Tuple<double[,], double[,]>>> miniBatches = new List<List<Tuple<double[,], double[,]>>>();

            for (int i = 0; i < data.Count; i += batchSize)
            {
                miniBatches.Add(data.GetRange(i, Math.Min(batchSize, data.Count - i)));
            }

            return miniBatches;
        }
        //computes the gradients of the cost function with respect to weights and biases for each example in a mini-batch
        private void UpdateMiniBatch(List<Tuple<double[,], double[,]>> miniBatch, double eta)
        {
            List<double[,]> nablaB = biases.Select(b => new double[b.GetLength(0), b.GetLength(1)]).ToList();
            List<double[,]> nablaW = weights.Select(w => new double[w.GetLength(0), w.GetLength(1)]).ToList();

            foreach (var tuple in miniBatch)
            {
                var x = tuple.Item1;
                var y = tuple.Item2;

                var result = Backprop(x, y);

                var deltaNablaB = result.Item1;
                var deltaNablaW = result.Item2;

                nablaB = nablaB.Zip(deltaNablaB, (nb, dnb) => MatrixAddition(nb, dnb)).ToList();
                nablaW = nablaW.Zip(deltaNablaW, (nw, dnw) => MatrixAddition(nw, dnw)).ToList();
            }

            double miniBatchCount = miniBatch.Count;
            weights = weights.Zip(nablaW, (w, nw) => MatrixSubtraction(w, MatrixScalarMultiply((eta / miniBatchCount), nw))).ToList();
            biases = biases.Zip(nablaB, (b, nb) => MatrixSubtraction(b, MatrixScalarMultiply((eta / miniBatchCount), nb))).ToList();
        }

        private Tuple<List<double[,]>, List<double[,]>> Backprop(double[,] x, double[,] y)
        {
            List<double[,]> nabla_b = biases.Select(b => new double[b.GetLength(0), b.GetLength(1)]).ToList();
            List<double[,]> nabla_w = weights.Select(w => new double[w.GetLength(0), w.GetLength(1)]).ToList();

            //forward process
            double[,] activation = x;
            List<double[,]> activations = new List<double[,]>() { x };
            List<double[,]> zs = new List<double[,]>();

            for (int i = 0; i < numLayers - 1; i++)
            {
                double[,] b = biases[i];
                double[,] w = weights[i];

                double[,] z = MatrixAddition(MatrixDotProduct(w, activation), b); //

                zs.Add(z);
                activation = ApplySigmoid(z);
                activations.Add(activation);
            }

            double[,] delta = CostDerivative(activations.Last(), y);
            nabla_b[nabla_b.Count - 1] = delta;
            nabla_w[nabla_w.Count - 1] = MatrixDotProduct(delta, Transpose(activations[activations.Count - 2]));

            for (int l = 2; l < numLayers; l++)
            {
                double[,] z = zs[zs.Count - l]; //represents the weighted input for the current hidden layer
                double[,] sp = ApplySigmoidPrime(z); //represents the derivative of the sigmoid function applied to

                double[,] mt = Transpose(weights[weights.Count - l + 1]); //Transposed weights
                double[,] mx = MatrixDotProduct(mt, delta); //calculates the product of the weight matrix of the next layer and the error delta from the previous layer.


                delta = MatrixMultiplication(mx, sp);
                nabla_b[nabla_b.Count - l] = delta;
                nabla_w[nabla_w.Count - l] = MatrixDotProduct(delta, Transpose(activations[activations.Count - l - 1]));
            }

            return new Tuple<List<double[,]>, List<double[,]>>(nabla_b, nabla_w);
        }

        private int Evaluate(List<Tuple<double[,], double[,]>> testData)
        {
            int correctCount = 0;

            foreach (var tuple in testData)
            {
                var x = tuple.Item1;
                var y = tuple.Item2;

                var predictedForward = FeedForward(x).Cast<double>().ToArray();
                var predictedForwardMax = FeedForward(x).Cast<double>().ToArray().Max();

                var predicted = Array.IndexOf(predictedForward, predictedForwardMax);
                var expected = Array.IndexOf(y.Cast<double>().ToArray(), y.Cast<double>().Max());
                if (predicted == expected)
                {
                    correctCount++;
                }
            }

            return correctCount;
        }

        private double[,] CostDerivative(double[,] outputActivations, double[,] y)
        {
            return MatrixSubtraction(outputActivations, y);
        }

        private double[,] MatrixAddition(double[,] matrix1, double[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }

        private double[,] MatrixSubtraction(double[,] matrix1, double[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return result;
        }

        
        private double[,] MatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int cols2 = matrix2.GetLength(1);

            if (rows1 != rows2 || cols1 != cols2)
            {
//                return null;
                throw new ArgumentException("Matrix dimensions are not compatible for multiplication.");
            }

            double[,] result = new double[rows1, cols1];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    result[i, j] = matrix1[i,j] * matrix2[i, j];
                }
            }

            return result;
        }

        private double[,] MatrixDotProduct(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int cols2 = matrix2.GetLength(1);

            if (cols1 != rows2)
            {
               // return null;
               throw new ArgumentException("Matrix dimensions are not compatible for multiplication.");
            }

            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        private double[,] MatrixScalarMultiply(double scalar, double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = scalar * matrix[i, j];
                }
            }

            return result;
        }

        private double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result[i, j] = matrix[j, i];
                }
            }

            return result;
        }

        private double[,] ApplySigmoid(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = Sigmoid(matrix[i, j]);
                }
            }

            return result;
        }

        private double[,] ApplySigmoidPrime(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = SigmoidPrime(matrix[i, j]);
                }
            }

            return result;
        }
    }

}

interface ActivationFunction 
{
    double[,] calculate(double[,] matrix);
}

class SigmoidActivation : ActivationFunction
{
   
    double[,] ActivationFunction.calculate(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = Sigmoid(matrix[i, j]);
            }
        }

        return result;
    }
    private double Sigmoid(double z)
    {
        return 1.0 / (1.0 + Math.Exp(-z));
    }
}

class ReLu : ActivationFunction
{

    double[,] ActivationFunction.calculate(double [,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if(result[i, j] < 0)
                result[i, j] = 0;
            }
        }

        return result;

    }


}
