using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TestTask.Model
{
    public class WorkWithJson
    {
        public void ReadFromFile(string filePath, UserData userData)
        {
            List<Person> people = new List<Person>();
            people = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(filePath));
            
            foreach (var person in people)
            {
                userData.addDayResultForUser(person.User, person);
            }
        }
        public void SaveInJsonFile(string filePath, ExportDataForJson exportDataForJson)
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteInFile = JsonConvert.SerializeObject(exportDataForJson);
                writer = new StreamWriter(filePath, false);
                writer.Write(contentsToWriteInFile);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
