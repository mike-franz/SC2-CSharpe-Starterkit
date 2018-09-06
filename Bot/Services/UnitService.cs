namespace Bot.Services
{
    public class UnitService
    {
        public int GetScvCount()
        {
            return Controller.GetUnits(Units.SCV).Count;
        }
    }
}