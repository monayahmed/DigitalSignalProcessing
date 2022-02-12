using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public float SignalMean = 0;
        public List<float> OutputSamples = new List<float>();
        public override void Run()
        {
            for(int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                SignalMean += InputSignal.Samples[i];
            }
            SignalMean /= InputSignal.Samples.Count();
            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                OutputSamples.Add(InputSignal.Samples[i] - SignalMean);
            }
            OutputSignal = new Signal(OutputSamples, false);
        }
    }
}