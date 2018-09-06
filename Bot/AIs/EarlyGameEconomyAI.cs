using System.Collections.Generic;
using System.Linq;
using Bot.Models;
using Bot.Services;
using SC2APIProtocol;

namespace Bot.AIs
{
    public class EarlyGameEconomyAI : Bot
    {
        private readonly StrategicMapDetails _strategicMapDetails;
        private readonly UnitService _unitService;

        public EarlyGameEconomyAI(StrategicMapDetails strategicMapDetails, UnitService unitService)
        {
            _strategicMapDetails = strategicMapDetails;
            _unitService = unitService;
        }

        public void OnFrame()
        {
            DisplayHeader();

            CalculateStrategicMapDetails();

            CreateSVCsIfNeeded();
        }

        private void CalculateStrategicMapDetails()
        {
            if (_strategicMapDetails.StartingLocation != null)
            {
                return;
            }

            var position = Controller.resourceCenters.First().position;

            _strategicMapDetails.StartingLocation = _strategicMapDetails.StartingLocations.First(l => l.x == position.X && l.y == position.Y);
        }

        private void CreateSVCsIfNeeded()
        {
            var resourceCenters = Controller.GetUnits(Units.ResourceCenters);
            foreach (var rc in resourceCenters)
            {
                if (rc.order.AbilityId == Abilities.COMMAND_CENTER_TRAIN_SCV || rc.assignedWorkers >= rc.idealWorkers * 2)
                {
                    continue;
                }

                if (Controller.CanConstruct(Units.SCV))
                    rc.Train(Units.SCV);
            }
        }

        private void DisplayHeader()
        {
            if (Controller.frame != 0)
            {
                return;
            }

            Logger.Info("--------------------------------------");
            Logger.Info("EarlyGameEconomyAI");
            Logger.Info("--------------------------------------");
        }
    }
}