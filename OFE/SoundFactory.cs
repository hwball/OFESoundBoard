using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OFE
{
    public class SoundFactory
    {
        public static Dictionary<String, Tuple<String, String>> BuildSoundList()
        {
            var appSettings = ConfigurationManager.AppSettings;

            String line;
            Dictionary<String, Tuple<String, String>> OutputDict = new Dictionary<string, Tuple<string, string>>();
            StreamReader sr = new StreamReader(appSettings["soundFile"]);

            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                //pass over notes
                if (line.StartsWith("#") || line == "")
                {
                    //Read the next line
                    line = sr.ReadLine();
                    continue;
                }

                //write the lie to console window
                string[] lineParts = line.Split(',');
                OutputDict[lineParts[0].ToLower()] = new Tuple<string, string>(lineParts[1], lineParts[2].ToLower());


                //Read the next line
                line = sr.ReadLine();
            }

            //close the file
            sr.Close();

            return OutputDict;
        }

        public static List<String> BuildHelpList(Dictionary<String, Tuple<String, String>> soundDict)
        {
            List<String> outList = new List<string>();

            List<String> keys_sorted = soundDict.Keys.ToList();
            keys_sorted.Sort();

            int numPerPage = 25;
            int numOfPages = (keys_sorted.Count + numPerPage - 1) / numPerPage;
            int currentCount = 0;

            for (int page = 0; page <= numOfPages; page++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("____________________Help Page ").Append(page).Append("____________________\n");

                while (currentCount <= (numPerPage * (page + 1)) && currentCount < soundDict.Count)
                {
                   builder.AppendLine(string.Format("{0,-40}'{1, -40}", keys_sorted[currentCount], soundDict[keys_sorted[currentCount]].Item1+"'"));
                   currentCount++;
                }

                builder.Append("___________________________________________________\n");

                outList.Add(builder.ToString());
            }


            return outList;
        }
    }
}
