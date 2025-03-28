using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace LupiSoupizet.ResizeFunction
{ 
    public static class ResizeHttpTrigger
    {
        [FunctionName("ResizeHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            int w = int.TryParse(req.Query["w"], out var width) ? width : 0; // Valeur par défaut : 0
            int h = int.TryParse(req.Query["h"], out var height) ? height : 0; // Valeur par défaut : 0
            
            if (w <= 0 || h <= 0)
            {
                return new BadRequestObjectResult("Les paramètres 'w' et 'h' doivent être des entiers positifs.");
            }

            byte[]  targetImageBytes;
            using(var  msInput = new MemoryStream())
            {
                // Récupère le corps du message en mémoire
                await req.Body.CopyToAsync(msInput);
                msInput.Position = 0;

                try {

                    // Charge l'image       
                    using (var image = Image.Load(msInput)) 
                    {
                        // Effectue la transformation
                        image.Mutate(x => x.Resize(w, h));

                        // Sauvegarde en mémoire               
                        using (var msOutput = new MemoryStream())
                        {
                            image.SaveAsJpeg(msOutput);
                            targetImageBytes = msOutput.ToArray();
                        }
                    }
                }

                catch (Exception ex)
                {
                    log.LogError($"Erreur lors du traitement de l'image: {ex.Message}.");
                    return new BadRequestObjectResult("Erreur lors du traitement de l'image : " + ex.Message);
                }
            }
            return new FileContentResult(targetImageBytes, "image/jpeg");        }
    }
}
