using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Model
{
    [Serializable]
    public class ExportDataForJson
    {
        public DataForTable selectedUser { get; set; }
        public List<Person> people { get; set; }
    }
}
