using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using DSPAlgorithms;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class SamplingRate : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public Signal FinalOutputSignal { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public List<float> OutputList = new List<float>();
        public List<float> TempList = new List<float>();
        List<float> output = new List<float>();
        public List<float> callFIR(Signal InputSignal)
        {
            FIR lowFilter = new FIR();
            lowFilter.InputTimeDomainSignal = InputSignal;
            lowFilter.InputFilterType = FILTER_TYPES.LOW;
            lowFilter.InputFS = 8000;
            lowFilter.InputCutOffFrequency = 1500;
            lowFilter.InputStopBandAttenuation = 50;
            lowFilter.InputTransitionBand = 500;
            lowFilter.Run();
            return lowFilter.OutputYn.Samples;
        }
        public override void Run()
        {
            Signal newSig = new Signal(TempList, false);
            FinalOutputSignal = newSig;
            OutputSignal = new Signal(output, false);
            if (M == 0 && L > 0)
            {
                L = L - 1;
                for (int i = 0; i < InputSignal.Samples.Count() - 1; i++)
                {
                    FinalOutputSignal.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L; j++)
                    {
                        FinalOutputSignal.Samples.Add(0);
                    }
                }
                FinalOutputSignal.Samples.Add(InputSignal.Samples.Count() - 1);
                OutputList = callFIR(FinalOutputSignal);
                for (int i = 0; i < 1165; i++)
                {
                    OutputSignal.Samples.Add(OutputList[i]);
                }
            }
            else if (M != 0 && L == 0)
            {
                M = M - 1;
                OutputList = callFIR(InputSignal);
                for (int i = 0; i < OutputList.Count()-1; i = (M + i + 1))
                {
                    OutputSignal.Samples.Add(OutputList[i]);
                }
            }
            else if (M != 0 && L != 0)
            {
                L = L - 1;
                M = M - 1;
                for (int i = 0; i < InputSignal.Samples.Count(); i++)
                {
                    FinalOutputSignal.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 1; j <= L; j++)
                    {
                        FinalOutputSignal.Samples.Add(0);
                    }
                }
                OutputList = callFIR(FinalOutputSignal);
                for (int i = 0; i < OutputList.Count(); i = (i + M + 1))
                {
                    OutputSignal.Samples.Add(OutputList[i]);
                }
            }
            else
                Console.WriteLine("Error");
        }
    }
}
