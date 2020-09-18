using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dog_Site.Controllers
{
    public class ImageController : Controller
    {
        //     private string PathDB = string.Empty;
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private string path = string.Empty;
        private string newFileName = string.Empty;
        private string fileName = string.Empty;

        // Constructor
        public ImageController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            var newFileName = string.Empty;
            string fileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // path = "Images/" + myUniqueFileName + FileExtension;
                        path = "Images/" + newFileName;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "Images") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        //         PathDB = "Images/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }

            var input = new ModelInput();
            input.ImageSource = fileName;

            ModelOutput output = ConsumeModel.Predict(input);

            return View("Result", new ResultModel(path, output.Prediction.ToString()));
        }
    }
}