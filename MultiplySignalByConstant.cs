using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            float multiplied_signals;
            List<float> output_signals = new List<float>();
            for (int i = 0; i < InputSignal.Samples.Count; i++)  //or InputSignals[0].Samples.Count
            {
                multiplied_signals = InputSignal.Samples[i] * InputConstant;
                output_signals.Add(multiplied_signals);
            }
            OutputMultipliedSignal = new Signal(output_signals, true);
        }
    }
}
