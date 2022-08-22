namespace EngineOverheating
{
    public class Application
    {
        private void updateSegment(Engine engine, double timeDiff, ref double Vcur, ref double Mcur, ref int index)
        {
            if (Vcur >= engine.V[index + 1] - engine.M[index + 1] / engine.I * timeDiff
                                && Vcur <= engine.V[index + 1] + engine.M[index + 1] / engine.I * timeDiff)
            {
                index++;
                Vcur = engine.V[index];
                Mcur = engine.M[index];
            }
        }
        public void Run()
        {
            string path = "../../../data/engineconfig.json";
            List<Engine> engineList = InputOutputEngine.readEnginParamsJSON(path);
            foreach (var engine in engineList)
            {
                double Tenv = InputOutputEngine.readTemperature();
                double Tengine = Tenv;
                double timeModel = 0;
                double timeDiff = 1;
                double Vcur = engine.V[0];
                double Mcur = engine.M[0];

                double Vh, Vc;
                int index = 0;
                while (Math.Round(Tengine, 0) < engine.Tover)
                {
                    timeModel++;
                    if (index + 1 >= engine.V.Length)
                    {
                        break;
                    }
                    updateSegment(engine, timeDiff, ref Vcur, ref Mcur, ref index);
                    Vh = EngineOverheating.speedHeating(Mcur, engine.Hm, Vcur, engine.Hv);
                    Vc = EngineOverheating.speedCooling(engine.C, Tenv, Tengine);
                    if (Math.Round(Vh + Vc, 2) == 0)
                    {
                        Console.WriteLine("Двигатель не перегреется");
                        return;
                    }
                    Tengine += timeDiff * (Vh + Vc);
                    Vcur = EngineOverheating.updateV(engine, Vcur, Mcur, timeDiff);
                    Mcur = EngineOverheating.updateM(engine, Mcur, index);
                }
                InputOutputEngine.writeTime(timeModel);
            }
        }
    }
}
