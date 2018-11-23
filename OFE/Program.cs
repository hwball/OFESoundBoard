using MediaToolkit;
using MediaToolkit.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace OFE
{
    class Program
    {

        static Dictionary<String, Tuple<String, String>> SoundDict;
        static List<String> helpList;

        static string soundboardPath = "C:\\Users\\Hamish\\Music\\Soundboard\\";
        static string videoDownloadPath = "C:\\Users\\Hamish\\Music\\Youtube\\";     

        static string hotkeyHelp =
            "         __________________________________________  \n" +
            "        |                                          | \n" +
            "        | NumLock       -      'Turn Off hotkeys'  | \n" +
            "        |__________________________________________| \n" +
            "        |   Numpad 7   |   Numpad 8   |   Numpad 9 | \n" +
            "        |  '!Sense'    |'Is Only Game'|   'Nani!'  | \n" +
            "        |______________|______________|____________| \n" +
            "        |   Numpad 4   |   Numpad 5   |   Numpad 6 | \n" +
            "        | 'Why Running'|   'Alert'    |   'Deja Vu'| \n" +
            "        |______________|______________|____________| \n" +
            "        |   Numpad 1   |   Numpad 2   |   Numpad 3 | \n" +
            "        |  'Run Away'  |   'Losing'   |   'Wining' | \n" +
            "        |______________|______________|____________| \n";

        static string functionKeyHelp =
            "   _______________________________________________________    \n" +
            "  |   F1    |   F2   |   F3    |   F4    |  F5   |   F6   |   \n" +
            "  | 'Brave' | 'Law'  |'Monster'| 'Happy' |'Hello'|'later' |   \n" +
            "  |         |        | 'kill'  |'Landing'|'there'|        |   \n" +
            " _|_________|________|_________|_________|_______|________|_  \n" +
            "|    F7   |   F8   |   F9    |   F10    |  F11   |   F12    | \n" +
            "|  'Dont' | 'Yoo'  | 'Laugh' | 'soviet' | 'Hell' |  'Cool'  | \n" +
            "|  'Feed' |        | 'Track' |          |  'NO!' |          | \n" +
            "|_________|________|_________|__________|________|__________| \n";


        private static Random random = new Random();

        private static void InjectMicrophone(string audioFileName)
        {
            //output 
            var outputDevice = new WaveOutEvent();
            outputDevice.DeviceNumber = 1;
            var outputDeviceMe = new WaveOutEvent();

            var audioFile = new AudioFileReader(audioFileName);
            var audioFileMe = new AudioFileReader(audioFileName);

            //using (outputDevice)
            //{
            //    for (int n = -1; n < WaveOut.DeviceCount; n++)
            //    {
            //        var caps = WaveOut.GetCapabilities(n);
            //        Console.WriteLine($"{n}: {caps.ProductName}");
            //    }
            //}

            outputDeviceMe.Init(audioFileMe);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDeviceMe.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
            outputDevice.Dispose();
            Console.WriteLine("Done.\n");
        }

        private static void CheckForSound(string soundName)
        {
            Console.WriteLine("Playing '" + SoundDict[soundName].Item1 + "'");
            InjectMicrophone(soundboardPath + SoundDict[soundName].Item2);
        }

        private static void CheckForHelpCommand(string command)
        {
            if (command.StartsWith("help"))
            {
                try
                {
                    if (command == "help")
                    {
                        Console.Clear();
                        Console.WriteLine(helpList[0]);
                    }

                    var result = Convert.ToInt32(Regex.Match(command, @"\d+$").Value);

                    if (result < helpList.Count)
                    {
                        Console.Clear();
                        Console.WriteLine(helpList[result]);
                    }
                }
                catch (System.FormatException e)
                {
                    //ignore
                }

            }

            switch (command.ToLower())
            { 
                case "clear":
                    Console.Clear();
                    Console.WriteLine("Type 'help' for commands.");
                    break;
                case "exit":
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }

        public static void play_youtube_audio(string url)
        {
            YouTube youtube = YouTube.Default;
            Video vid = youtube.GetVideo(url);
            System.IO.File.WriteAllBytes(videoDownloadPath + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = videoDownloadPath + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{videoDownloadPath + vid.FullName}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
            InjectMicrophone(outputFile.Filename);
        
            //removes files when done
            if (System.IO.File.Exists(inputFile.Filename))
            {
                System.IO.File.Delete(inputFile.Filename);
            }
        }

        public static string Pickrandom(List<string> stringArray)
        {           
            int index = random.Next(stringArray.Count);
            var name = stringArray[index];
            stringArray.RemoveAt(index);
            return name;
        }

        public static void kbh_OnKeyPressed(object sender, Keys e)
        {

            switch (e)
            {
                //Keypad
                case Keys.NumPad1://run away
                    CheckForSound(Pickrandom(new List<string> { "fly", "run away", "run" }));
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad2://sad/losing
                    CheckForSound(Pickrandom(new List<string> { "losing horn", "m", "lol defeat", "sad", "disappointment", "hello", "bother", "twin", "didntwant"}));
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad3://winning
                    CheckForSound(Pickrandom(new List<string> { "sweet victory", "champions", "lol victory", "best", "ff victory", "ctwin", "gg" }));
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad4:
                    CheckForSound("r");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad5:
                    CheckForSound("a");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad6:
                    CheckForSound("deja");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad7:
                    CheckForSound("kronk");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad8:
                    CheckForSound("mad");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumPad9:
                    CheckForSound("nani");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.NumLock:
                    Console.Clear();
                    Console.WriteLine("Type 'help' for commands.");
                    Application.Exit();
                    return;
                //function keys
                case Keys.F1://run away
                    CheckForSound("brave");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F2://sad/losing
                    CheckForSound("law");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F3://winning
                    CheckForSound("dota mk");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F4:
                    CheckForSound("landing");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F5:
                    CheckForSound("hello there");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F6:
                    CheckForSound("later");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F7:
                    CheckForSound("dont feed");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F8:
                    CheckForSound("yoo");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F9:
                    CheckForSound("laugh");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    break;
                case Keys.F10:
                    CheckForSound("soviet");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    return;
                case Keys.F11:
                    CheckForSound("hell no");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    return;
                case Keys.F12:
                    CheckForSound("cool");
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);
                    return;
            }
        }    

        static void Main(string[] args) //build : dotnet build -r win10-x64
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);


            Console.WriteLine("Type 'help' for commands.");

            SoundDict = SoundFactory.BuildSoundList();

            helpList = SoundFactory.BuildHelpList(SoundDict);

            //foreach (var thing in help)
            //{
            //    Console.WriteLine(thing);
            //}
            //while (true)
            //{

            //}

            while (true)
            {
                string line = Console.ReadLine().ToLower();
                if (line.ToLower() == "hotkey") //hot key mode
                {
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);
                    Console.WriteLine(functionKeyHelp);

                    LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
                    kbh.OnKeyPressed += kbh_OnKeyPressed;
                    kbh.HookKeyboard();

                    Application.Run();

                    kbh.UnHookKeyboard(); 
                }
                else if(SoundDict.ContainsKey(line))//console mode
                {    
                    CheckForSound(line);
                }
                else
                {
                    CheckForHelpCommand(line);
                }
            }
        }
    }
}
