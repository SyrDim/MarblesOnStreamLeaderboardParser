using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.Json;
using GvasFormat.Serialization;
using Newtonsoft.Json;

namespace GvasConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                return;
            }

            //Console.WriteLine("Parsing UE4 save file structure...");
            GvasFormat.Gvas save;
            using (var stream = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read))
                save = UESerializer.Read(stream);

            //Console.WriteLine("Converting to json...");
            var json = JsonConvert.SerializeObject(save, new JsonSerializerSettings{Formatting = Formatting.Indented});

            //Console.WriteLine("Saving json...");
            using (var stream = File.Open(args[0] + ".json", FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                writer.Write(json);


            //Winner Check
            string[] jsonfile = args;
            jsonfile[0] = jsonfile[0] + ".json";    //assign .json(new/updated) file path to string[]
            string formatedfile;
            formatedfile = jsonfile[0].Remove(jsonfile[0].Length - 4);
            formatedfile += "txt";    //assign .txt (trimmed/formated json) file path to string

            if (!File.Exists(formatedfile))
            {
                //Read converted json file
                using StreamReader fs = File.OpenText(jsonfile[0]);
                string result = fs.ReadToEnd();
                fs.Close();

                string ok = "";     //value of property
                string nm = "";     //name of property
                int players = 0;    //player count
                List<object> dataObjects = new();   //list of each player's properties

                var data = JsonConvert.DeserializeObject<dynamic>(result);

                if (data != null)
                {
                    //Count the amount of players
                    foreach (var pro1 in data.Properties[0].Properties[0].Items)
                    {
                        players++;
                    }

                    //format and fill dataObjects with only the necessary info from json
                    foreach (var pro1 in data.Properties[0].Properties[0].Items)
                    {
                        var dataObject = new ExpandoObject() as IDictionary<string, object>;
                        int i = -1;
                        foreach (var prop2 in pro1.Properties)
                        {
                            i++;
                            ok = prop2["Value"];
                            nm = prop2["Name"];
                            if (i == 3 || ok == null)   //ignore properties with no value and "_SeasonPoints"
                                continue;
                            dataObject.Add(nm, ok);
                        }
                        dataObjects.Add(dataObject);
                    }

                    //format dataObjects to json and write to file
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(dataObjects, new JsonSerializerOptions()
                    {
                        WriteIndented = true
                    });
                    File.WriteAllText(formatedfile, jsonString);

                    var data2 = JsonConvert.DeserializeObject<dynamic>(jsonString);
                }
            }
            else
            {
                //read files to be compared
                using StreamReader orig = new(jsonfile[0]);    //updated player file
                string result = orig.ReadToEnd();
                using StreamReader newfi = new(formatedfile);     //old player file
                string newresult = newfi.ReadToEnd();
                orig.Close();
                newfi.Close();

                string ok = "";
                string nm = "";
                int players = 0;        //new player count
                int newplayers = 0;     //old player count
                string winner, second, third;
                winner = second = third = "";
                List<object> dataObjects = new();

                var data = JsonConvert.DeserializeObject<dynamic>(result);
                var newdata = JsonConvert.DeserializeObject<dynamic>(newresult);

                if (data != null)
                {
                    if (newdata != null)
                    {
                        //Count the old player file
                        foreach (var pro1 in newdata)
                        {
                            newplayers++;
                        }
                        //Count the updated player file
                        foreach (var pro1 in data.Properties[0].Properties[0].Items)
                        {
                            players++;
                        }

                        //format and fill dataObjects with only the necessary info from updated json
                        foreach (var pro1 in data.Properties[0].Properties[0].Items)
                        {
                            var dataObject = new ExpandoObject() as IDictionary<string, object>;
                            int i = -1;
                            foreach (var prop2 in pro1.Properties)
                            {
                                i++;
                                ok = prop2["Value"];
                                nm = prop2["Name"];
                                if (i == 3 || ok == null)   //ignore properties with no value and "_SeasonPoints"
                                    continue;
                                dataObject.Add(nm, ok);
                            }
                            dataObjects.Add(dataObject);
                        }

                        //format dataObjects to json and update old file
                        var jsonString = System.Text.Json.JsonSerializer.Serialize(dataObjects, new JsonSerializerOptions()
                        {
                            WriteIndented = true
                        });
                        File.WriteAllText(formatedfile, jsonString);

                        var data2 = JsonConvert.DeserializeObject<dynamic>(jsonString);
                        if (data2 != null)
                        {
                            //check if new players added and check first 3 places
                            if (newplayers < players)
                            {
                                for (int i = 0; i < newplayers; i++)
                                {
                                    if (data2[i]._Points != newdata[i]._Points)
                                    {
                                        int bef = Convert.ToInt32(data2[i]._Points);
                                        int after = Convert.ToInt32(newdata[i]._Points);
                                        if (bef - after == 3)
                                            winner = data2[i]._DisplayName;
                                        else if (bef - after == 2)
                                            second = data2[i]._DisplayName;
                                        else if (bef - after == 1)
                                            third = data2[i]._DisplayName;
                                    }
                                }
                                for (int i = newplayers; i < players; i++)
                                {
                                    if (data2[i]._Points == 3)
                                        winner = data2[i]._DisplayName;
                                    else if (data2[i]._Points == 2)
                                        second = data2[i]._DisplayName;
                                    else if (data2[i]._Points == 1)
                                        third = data2[i]._DisplayName;
                                }
                            }
                            //check first 3 places
                            else
                            {
                                for (int i = 0; i < players; i++)
                                {
                                    if (data2[i]._Points != newdata[i]._Points)
                                    {
                                        int bef = Convert.ToInt32(data2[i]._Points);
                                        int after = Convert.ToInt32(newdata[i]._Points);
                                        if (bef - after == 3)
                                            winner = data2[i]._DisplayName;
                                        else if (bef - after == 2)
                                            second = data2[i]._DisplayName;
                                        else if (bef - after == 1)
                                            third = data2[i]._DisplayName;
                                    }
                                }
                            }
                        }
                        string winners = "";
                        if (winner != "")
                            winners = winners + "!addpoints " + winner + " 1000";
                        if (second != "")
                            winners = winners + "\n!addpoints " + second + " 700";
                        if (third != "")
                            winners = winners + "\n!addpoints " + third + " 500";

                        //Check if file is empty
                        if (winners != "")
                        {
                            var exte = Path.GetDirectoryName(formatedfile);
                            exte += @"\winners.txt";
                            File.WriteAllText(exte, winners);
                            Path.GetDirectoryName(formatedfile);
                        }
                    }
                }
            }
        }
    }
}
