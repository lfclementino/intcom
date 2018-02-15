using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using intcom.web.Models;

namespace intcom.web.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadModel model)
        {
            var file = model.Files[0];

            //AWS Upload
            AWS Amazon = new AWS("bucketName", " keyName");
            Amazon.Upload(file);

            return View(model);
        }


    }
}