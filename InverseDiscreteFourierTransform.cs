using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }
        public List<Complex> complex = new List<Complex>();
        public List<Complex> complex_trigo = new List<Complex>();
        public List <float> resultat = new List<float>();
        public double Power;
        public float RealValue;
        public float ImaginaryValue;
        public override void Run()
        {
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); i++)
            {
                complex.Add(Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[i], InputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }

            for (int n = 0; n < InputFreqDomainSignal.FrequenciesAmplitudes.Count; n++)
            {
                complex_trigo.Add(0);
                for (int k = 0; k < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); k++)
                {
                    Power = ( (Math.PI ) * 2 * n * k) / InputFreqDomainSignal.FrequenciesAmplitudes.Count();
                    complex_trigo[n] += complex[k] * ( Math.Cos(Power) + (Math.Sin(Power) * Complex.ImaginaryOne));
                }
                complex_trigo[n] /= InputFreqDomainSignal.FrequenciesAmplitudes.Count();
            }
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); i++)
            {
                resultat.Add((float)complex_trigo[i].Real);
            }
            OutputTimeDomainSignal = new Signal( resultat,false);
        }
    }
}
