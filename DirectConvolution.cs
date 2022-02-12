using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; } //X
        public Signal InputSignal2 { get; set; } //h
        public Signal OutputConvolvedSignal { get; set; }
        public List<float> list = new List<float>();
        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int End = InputSignal1.SamplesIndices[InputSignal1.Samples.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.Samples.Count - 1];
            int Start = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            double result;
            OutputConvolvedSignal = new Signal(list, false);
            for (int n = Start; n <= End; n++)
            {
                result = 0;
                for (int k = InputSignal1.SamplesIndices[0]; k < InputSignal1.Samples.Count(); k++)  //k = InputSignal1.SamplesIndices[0]
                {
                    if ((n - k) >= InputSignal2.Samples.Count() || k > InputSignal1.SamplesIndices.Max() ||
                        (n - k) > InputSignal2.SamplesIndices.Max() || k < InputSignal1.SamplesIndices.Min())
                        continue;
                    else if ((n - k) < InputSignal2.SamplesIndices[0])
                        break;
                    else
                    {
                        int x = InputSignal1.SamplesIndices.IndexOf(k);
                        int y = InputSignal2.SamplesIndices.IndexOf(n - k);
                        result += (InputSignal1.Samples[x] * InputSignal2.Samples[y]);
                    }

                }
                if (n == End && result == 0)
                    continue;
                OutputConvolvedSignal.Samples.Add((float)result);
                OutputConvolvedSignal.SamplesIndices.Add(n);
            }
        }
    }
}
