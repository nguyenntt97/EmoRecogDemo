using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecog.Services
{
    class Utils
    {
        public static String getResourceURI(String relUri)
        {
            return Environment.CurrentDirectory + "../../" + relUri;
        }
    }
}
