using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Helper
{
    public class GoogleDriveSettings
    {
        public string ApplicationName { get; set; }
        public string CredentialPath { get; set; }
        public string[] Scopes { get; set; }
    }
}
