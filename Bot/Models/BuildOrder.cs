using System.Collections.Generic;

namespace Bot.Models
{
    public class BuildOrder
    {
        public List<BuildOrderStep> Steps { get; set; }

        public void Initialize()
        {
            foreach (var step in Steps)
            {
                step.Initialize();
            }
        }
    }

    public class BuildOrderStep
    {
        public uint BuildingUnitID { get; set; }
        public int MinScvCount { get; set; }
        public string Type { get; set; }
        public string BuildUnit { get; set; }
        public string RequiredLocation { get; set; }
        public uint MineralsNeeded { get; set; }

        public void Initialize()
        {
            switch (BuildUnit.ToUpper().Trim())
            {
                case "SUPPLYDEPO":
                    BuildingUnitID = Units.SUPPLY_DEPOT;
                    MineralsNeeded = 100;
                    break;
                case "BARRACKS":
                    BuildingUnitID = Units.BARRACKS;
                    MineralsNeeded = 150;
                    break;
          
            }
        }
    }
}