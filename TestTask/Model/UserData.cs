using System;
using System.Collections.Generic;
using System.Linq;


namespace TestTask.Model
{
    public class UserData
    {
        public Dictionary<string, List<Person>> PersonDictionary;
        public UserData()
        {
            PersonDictionary = new Dictionary<string, List<Person>>();
        }

        public void addDayResultForUser(string fio, Person dayResult)
        {
            if (PersonDictionary.ContainsKey(fio))
            {
                PersonDictionary[fio].Add(dayResult);
            }
            else
            {
                List<Person> list = new List<Person> { dayResult };
                PersonDictionary.Add(fio, list);
            }
        }
        public List<int> getDayResultForUser(string name)
        {
            List<int> everyDaySteps = new();

            if (PersonDictionary.ContainsKey(name))
            {
                List<Person> person = PersonDictionary.GetValueOrDefault(name);
                foreach (var man in person)
                {
                    everyDaySteps.Add(man.Steps);
                }
            }
            return everyDaySteps;
        }
        public int getAverageScoreForUser(string name)
        {
            int averageScore = 0;
            int allSteps = 0;
            if (PersonDictionary.ContainsKey(name))
            {
                List<Person> person = PersonDictionary.GetValueOrDefault(name);
                for (int i = 0; i < PersonDictionary[name].Count; i++)
                {
                    allSteps += person[i].Steps;
                }
            }
            averageScore = allSteps / PersonDictionary[name].Count(); 
            return averageScore;
        }
        public int getMinScoreForUser(string name)
        {
            int minScore = 0;
            if (PersonDictionary.ContainsKey(name))
            {
                List<Person> person = PersonDictionary.GetValueOrDefault(name);
                minScore = person[0].Steps;
                foreach(var man in person)
                {
                    if (minScore > man.Steps)
                    {
                        minScore = man.Steps;
                    }
                }
            }
            return minScore;
        }
        public int getMaxScoreForUser(string name)
        {
            int maxScore = 0;
            if (PersonDictionary.ContainsKey(name))
            {
                List<Person> person = PersonDictionary.GetValueOrDefault(name);
                foreach (var man in person)
                {
                    if (maxScore < man.Steps)
                    {
                        maxScore = man.Steps;
                    }
                }
            }
            return maxScore;
        }
    }
}
