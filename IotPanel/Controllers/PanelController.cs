using DatabaseProject.Interface;
using DatabaseProject.Model;
using IotPanel.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Server;
using System.Text.Json;

namespace IotPanel.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PanelController : ControllerBase
    {
        private readonly NewPanelRepository _PanelRepository;

        private readonly MQTTHub _server;

        //public MQTTHub()

        //{

        //    var mqttFactory = new MqttFactory();

        //    var mqttServerOptions = mqttFactory.CreateServerOptionsBuilder().WithDefaultEndpoint().Build();

        //    _server = mqttFactory.CreateMqttServer(mqttServerOptions);

        //}


        public PanelController(NewPanelRepository panelRepository, MQTTHub mqttHub)
        {
            _PanelRepository = panelRepository;
            _server = mqttHub;
            
        }

        [HttpGet]

        public ActionResult getRecordById(int panelId)
        {
            var record = _PanelRepository.getRecordById(panelId);

            return Ok(record);
        }
        [HttpPost]

        public async Task<ActionResult> AddPanel(PanelDTO panel, string topic)
        {
            try
            {

                string senderClientId = "iotPanel";

                Panel newRecord = new()
                {
                    panelId = panel.panelId,
                    key_1 = panel.key_1,
                    key_2 = panel.key_2,
                    key_3 = panel.key_3,
                    key_4 = panel.key_4,
                };

                string payload = JsonSerializer.Serialize(newRecord);
                var addPanel = _PanelRepository.AddPanel(newRecord);
                await _server.PublishMessageFromHub(topic, payload, senderClientId);

                return Ok(addPanel);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }

        }
    }
}
