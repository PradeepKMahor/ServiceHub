using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Models
{
    [Serializable]
    public class DataField
    {
        public string DataValueField { get; set; }
        public string DataTextField { get; set; }
        public string DataGroupField { get; set; }
    }
}