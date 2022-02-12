using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos : Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        float result;
        float DiscreteFrequency;
        float n;
        public override void Run()
        {
            samples = new List<float>();
            DiscreteFrequency = AnalogFrequency / SamplingFrequency;
            if (type == "sin")
            {
                for (n = 0; n < SamplingFrequency; n++) 
                {
                    result = (float)(Math.Sin((2 * Math.PI * DiscreteFrequency * n) + PhaseShift));
                    samples.Add(A * result);
                }
            }
            else if (type == "cos") 
            {
                for (n = 0; n < SamplingFrequency; n++) 
                {
                    result = (float)(Math.Cos((2 * Math.PI * DiscreteFrequency * n) + PhaseShift));
                    samples.Add(A * result);
                }
            }
        }
    }
}