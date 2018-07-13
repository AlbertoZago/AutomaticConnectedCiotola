using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.EventHubs;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartphoneApp.Models;
using Microsoft.Azure.Devices

namespace SmartphoneApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        public async Task Richiedi()
        {

            var deviceId = "ManualConnectedCiotola";

            var deviceKey = "qkSsTqeQCklq7/ADFFLkPsCo5h0n1RJAlG7R8Noe6eI=";

            var transportType = Microsoft.Azure.Devices.Client.TransportType.Mqtt_WebSocket_Only;

            var autenticationMethod = new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey);

            var client = DeviceClient.Create("AlbZagIotHub2.azure - devices.net", autenticationMethod, transportType);

            string messaggio = "Carica";


            Byte[] bytes = Encoding.UTF8.GetBytes(messaggio);

            Message message = new Message(bytes);

            await client.SendEventAsync(message);



            return true;
        }
    }
}
