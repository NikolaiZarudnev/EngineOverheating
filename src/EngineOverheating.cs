namespace EngineOverheating
{
    public static class EngineOverheating
    {
        public static double speedHeating(double M, double Hm, double V, double Hv)
        {
            return M * Hm + V * V * Hv;
        }
        public static double speedCooling(double C, double Tenv, double Tengine)
        {
            return C * (Tenv - Tengine);
        }
        public static double updateM(Engine engine, double Mcur, int index)
        {
            if (engine.V[index + 1] == engine.V[index]) return Mcur;
            double k = (engine.M[index + 1] - engine.M[index]) / (engine.V[index + 1] - engine.V[index]);
            return Mcur / engine.I * k + Mcur;
        }
        public static double updateV(Engine engine, double Vcur, double Mcur, double timeDiff)
        {
            if (engine.I == 0) return Vcur;
            Vcur = Vcur + Mcur / engine.I * timeDiff;
            return Vcur;
        }
    }
}
