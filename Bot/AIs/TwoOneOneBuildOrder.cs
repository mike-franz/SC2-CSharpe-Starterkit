using System;
using System.Linq;
using System.Numerics;
using Bot.Helpers;
using Bot.Models;
using Bot.Services;
using Newtonsoft.Json;

namespace Bot.AIs
{
    public class TwoOneOneBuildOrder : Bot
    {
        private readonly StrategicMapDetails _strategicMapDetails;
        private readonly UnitService _unitService;
        private readonly BuildOrder _buildOrder;

        public TwoOneOneBuildOrder(StrategicMapDetails strategicMapDetails, UnitService unitService)
        {
            _strategicMapDetails = strategicMapDetails;
            _unitService = unitService;
            _buildOrder = JsonConvert.DeserializeObject<BuildOrder>(EmbededResourceHelper.Get("Bot.BuildOrders.2-1-1.json"));
            _buildOrder.Initialize();
        }

        public void OnFrame()
        {
            BuildStructures();
            BuildUnits();
        }

        private void BuildUnits()
        {
            var barracks = Controller.GetUnits(Units.BARRACKS);
            if (!barracks.Any())
            {
                return;
            }

            foreach (var barrack in barracks)
            {
                if (barrack.buildProgress < 1)
                {
                    continue;
                }

                if (!Controller.CanAfford(Units.MARINE))
                {
                    continue;
                }

                //If it is already building something, leave it alone
                if (barrack.order?.AbilityId > 0)
                {
                    continue;
                }

                barrack.Train(Units.MARINE);
            }

        }

        private void BuildStructures()
        {
            var nextStep = _buildOrder.Steps.FirstOrDefault();
            if (nextStep == null)
            {
                return;
            }

            if (nextStep.MinScvCount > _unitService.GetScvCount())
            {
                return;
            }


            if (nextStep.MineralsNeeded > Controller.GetMineralCount())
            {
                return;
            }

            if (!Controller.CanConstruct(nextStep.BuildingUnitID))
            {
                return;
            }

            if (Controller.GetPendingCount(nextStep.BuildingUnitID) > 0)
            {
                return;
            }

            var strategicPoint = _strategicMapDetails.StrategicPoints.First(p =>
                p.startingLocation == _strategicMapDetails.StartingLocation.id &&
                string.Equals(p.key, nextStep.RequiredLocation, StringComparison.InvariantCultureIgnoreCase));

            Controller.Construct(nextStep.BuildingUnitID, new Vector3(strategicPoint.x, strategicPoint.y, 0));

            //Remove this and verify that it build properly.
            _buildOrder.Steps.Remove(nextStep);
        }
    }
}