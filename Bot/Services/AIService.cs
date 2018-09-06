using System.Collections;
using System.Collections.Generic;
using System.IO;
using Bot.AIs;
using Bot.Helpers;
using Bot.Models;
using Newtonsoft.Json;
using SC2APIProtocol;

namespace Bot.Services
{
    public class AIService
    {
        private List<Bot> _bots;
        private StrategicMapDetails _strategicMapDetails;
        private UnitService _unitService;

        public AIService()
        {
            _strategicMapDetails = JsonConvert.DeserializeObject<StrategicMapDetails>(EmbededResourceHelper.Get("Bot.MapDetails.16BitLE.json"));
            _unitService = new UnitService();

            _bots = new List<Bot>
            {
                new EarlyGameEconomyAI(_strategicMapDetails,_unitService),
                new TwoOneOneBuildOrder(_strategicMapDetails,_unitService)
            };


        }


        public IEnumerable<Action> Frame()
        {
            Controller.OpenFrame();

            foreach (var bot in _bots)
            {
                bot.OnFrame();
            }

            return Controller.CloseFrame();
        }
    }
}