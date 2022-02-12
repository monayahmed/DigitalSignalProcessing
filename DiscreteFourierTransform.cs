using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; } //sample.
        public float InputSamplingFrequency { get; set; } //Fs
        public Signal OutputFreqDomainSignal { get; set; } //output of freq/phase shift.
        
     

        public float RealValue, ImaginaryValue;
        public float frequency, Power;
        public List<float> Amplitudes = new List<float>();
        public List<float> PhaseShifts = new List<float>();
        
        public override void Run()
        {
            //Calculate power for each K, n times.
            List<float> SignalList = InputTimeDomainSignal.Samples;
            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)   //For each K, there is N values.
            {
                RealValue = 0;
                ImaginaryValue = 0;
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++) //2PI*J*k*n/N
                {
                    Power = ( (((float)Math.PI) * 2 * n * k) / InputTimeDomainSignal.Samples.Count() ); //neglige negative sign.
                    RealValue += (float)Math.Cos(Power) * InputTimeDomainSignal.Samples[n];
                    ImaginaryValue += (float)Math.Sin(Power) * -1 *InputTimeDomainSignal.Samples[n];
                }
               var Amplitude =(float) Math.Sqrt(RealValue*RealValue + ImaginaryValue*ImaginaryValue);
               var PhaseShift = Math.Atan2(ImaginaryValue, RealValue);
                Amplitudes.Add(Amplitude);
                PhaseShifts.Add((float)PhaseShift);
            }

            OutputFreqDomainSignal = new Signal(SignalList , false);
            OutputFreqDomainSignal.FrequenciesAmplitudes = Amplitudes;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = PhaseShifts;
        }
    }
}
