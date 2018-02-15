using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace intcom.web.Models
{
    public class UploadModel
    {
        public List<HttpPostedFileBase> Files { get; set; }
        public string FirstName { get; set; }

        public UploadModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
    }
}