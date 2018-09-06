using System.Linq;
using System.Numerics;
using SC2APIProtocol;

namespace Bot.Models
{
    class EarlyGameScout
    {
        private readonly Unit _scout;
        private Vector3? _scoutingLocation;

        public EarlyGameScout(Unit scvOffMineralLine)
        {
            _scout = scvOffMineralLine;
            _scoutingLocation = null;
        }


        public bool IsAlive()
        {
            if (_scout == null)
            {
                return false;
            }

            return _scout.integrity > 0;
        }

        public bool IsScouting()
        {
            if (_scout == null)
            {
                return false;
            }

            return _scout.order.AbilityId == Abilities.MOVE;
        }

        public void Scout()
        {
            if (_scout?.order.AbilityId == Abilities.MOVE)
            {
                return;
            }

            if (_scoutingLocation.HasValue)
            {
                var firstResourceCenter = Controller.resourceCenters.FirstOrDefault();
                if (firstResourceCenter == null)
                {
                    return;
                }

                _scout?.Move(firstResourceCenter.position);
            }
            else
            {
                _scoutingLocation = Controller.enemyLocations.First();
                _scout?.Move(_scoutingLocation.Value);
            }

            
        }
    }
}
