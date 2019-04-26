using Rage;

namespace BasicVehicleFunctions.Utils
{
    internal static class Logger
    {
        private const string LogPrefix = "BasicVehicleCallouts";

        internal static void Log(string LogLine)
        {
            string log = string.Format("[{0}]: {1}", LogPrefix, LogLine);

            Game.LogTrivial(log);
        }

        internal static void DebugLog(string LogLine)
        {
            string log = string.Format("[{0}]: {1}", LogPrefix, LogLine);
            Game.LogTrivial(log);
        }
    }
}