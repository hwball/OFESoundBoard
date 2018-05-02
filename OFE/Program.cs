using MediaToolkit;
using MediaToolkit.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace OFE
{
    class Program
    {
        static string soundboardPath = "C:\\Users\\Hamish\\Music\\Soundboard\\";
        static string videoDownloadPath = "C:\\Users\\Hamish\\Music\\Youtube\\";

        static string help =
            "___________________________________________________________\n\n" +
            "help               - Show Commands \n"+
            "hotkey             - start to hot key mode\n"+
            "numLock            - Stop hotkey mode\n"+
            "play <youtube_Url> - plays the audio from the video \n"+
            "\n"+
            "fly                - 'Fly You Fools'\n" +
            "deja               - 'Deja Vu' \n" +
            "a                  - 'Metal Gear alert' \n" +
            "m                  - 'Mission Failed' \n" +
            "r                  - 'Why are you running' \n" +
            "mad                - 'Is only game' \n" +
            "shroud             - 'The fuck i am' \n" +
            "nani               - 'Nani!'\n" +
            "omae               - 'Omae wa'\n" +
            "off                - 'oof'\n" +
            "brave              - 'Brave sir robin'\n" +
            "law                - 'Law and Order Dun dun'\n"+
            "hello              - 'Hello Darkness my old friend'\n"+
            "leeroy             - 'Leeroy Jenkins'\n"+
            "losing horn        - 'Losing horn'\n"+
            "wilhelm            - 'Wilhelm Scream'\n"+
            "pentakill          - 'Penta kill'\n"+
            "sweet victoy       - 'Sweet Victoy'\n"+
            "champions          - 'We are the Champions'\n"+
            "lol victory        - 'League of Legends Victory'\n"+
            "lol defeat         - 'League of Legends Defeat'\n"+
            "sad                - 'Sad Violin'\n" +
            "disappointment     - 'Disappointment is Immeasurable'\n"+
            "kronk              - 'Doesn't Make Sense'\n" +
            "run away           - 'Run Away!'\n"+
            "\n"+
            "___________________________________________________________\n\n";

        static string hotkeyHelp =
            "___________________________________________\n\n" +
            "NumLock            - 'Turn Off hotkeys'    \n" +
            "\n" +
            "__________________________________________ \n" +
            "   Numpad 7   |   Numpad 8   |   Numpad 9  \n" +
            "  '!Sense'    |'Is Only Game'|   'Nani!'   \n" +
            "______________|______________|___________  \n" +
            "   Numpad 4   |   Numpad 5   |   Numpad 6  \n" +
            " 'Why Running'|   'Alert'    |   'Deja Vu' \n" +
            "______________|______________|____________ \n" +
            "   Numpad 1   |   Numpad 1   |   Numpad 1  \n" +
            "  'Run Away'  |   'Losing'   |   'Wining'  \n" +
            "______________|______________|___________  \n";

        private static Random random = new Random();

        private static void InjectMicrophone(string audioFileName)
        {
            //output 
            var outputDevice = new WaveOutEvent();
            outputDevice.DeviceNumber = 3;
            var outputDeviceMe = new WaveOutEvent();

            var audioFile = new AudioFileReader(audioFileName);
            var audioFileMe = new AudioFileReader(audioFileName);

            //using (outputDevice)
            //{
            //for (int n = -1; n < WaveOut.DeviceCount; n++)
            //{
            //    var caps = WaveOut.GetCapabilities(n);
            //    Console.WriteLine($"{n}: {caps.ProductName}");
            //}
            outputDeviceMe.Init(audioFileMe);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDeviceMe.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Done.\n");
        }

        private static void CheckForSound(string soundName)
        {
            switch (soundName.ToLower())
            {
                case "help":
                    Console.Clear();
                    Console.WriteLine(help);
                    break;
                case "fly":
                    Console.WriteLine("Playing 'Fly You Fools'");
                    InjectMicrophone(soundboardPath + "Fly_you_fools.wav");
                    break;
                case "deja vu":
                case "deja":
                    Console.WriteLine("Playing 'Deja Vu'");
                    InjectMicrophone(soundboardPath + "Deja_vu.wav");
                    break;
                case "alert":
                case "a":
                    Console.WriteLine("Playing 'Metal Gear alert'");
                    InjectMicrophone(soundboardPath + "Alert.mp3");
                    break;
                case "m":
                case "mission":
                case "mission failed":
                    Console.WriteLine("Playing 'Mission Failed'");
                    InjectMicrophone(soundboardPath + "mission_failed.wav");
                    break;
                case "r":
                case "running":
                    Console.WriteLine("Playing 'Why are you running'");
                    InjectMicrophone(soundboardPath + "running.wav");
                    break;
                case "mad":
                case "only game":
                    Console.WriteLine("Playing 'is only game'");
                    InjectMicrophone(soundboardPath + "mad.wav");
                    break;
                case "shroud":
                    Console.WriteLine("Playing 'The fuck i am'");
                    InjectMicrophone(soundboardPath + "the_fuck_i_am.mp3");
                    break;
                case "nani":
                    Console.WriteLine("Playing 'Nani!'");
                    InjectMicrophone(soundboardPath + "nani.wav");
                    break;
                case "omae":
                    Console.WriteLine("Playing 'Omae wa'");
                    InjectMicrophone(soundboardPath + "Omae_wa.wav");
                    break;
                case "oof":
                    Console.WriteLine("Playing 'Oof'");
                    InjectMicrophone(soundboardPath + "oof.mp3");
                    break;
                case "sir robin":
                case "brave":
                    Console.WriteLine("Playing 'Brave sir robin'");
                    InjectMicrophone(soundboardPath + "brave_sir_robin.wav");
                    break;
                case "law":
                    Console.WriteLine("Playing 'Law and Order'");
                    InjectMicrophone(soundboardPath + "Law_Order.mp3");
                    break;
                case "hello darkness":
                case "hello":
                    Console.WriteLine("Playing 'Hello Darkness my old friend'");
                    InjectMicrophone(soundboardPath + "HELLO_DARKNESS.mp3");
                    break;
                case "leeroy":
                    Console.WriteLine("Playing 'Leeroy Jenkins'");
                    InjectMicrophone(soundboardPath + "leeroy_jenkins.mp3");
                    break;
                case "losing horn":
                    Console.WriteLine("Playing 'losing horn'");
                    InjectMicrophone(soundboardPath + "losing_horn.wav");
                    break;
                case "wilhelm":
                    Console.WriteLine("Playing 'Wilhelm Scream'");
                    InjectMicrophone(soundboardPath + "Wilhelm_Scream.mp3");
                    break;
                case "pentakill":
                    Console.WriteLine("Playing 'Penta kill'");
                    InjectMicrophone(soundboardPath + "pentakill.wav");
                    break;
                case "sweet victory":
                    Console.WriteLine("Playing 'Sweet Victoy'");
                    InjectMicrophone(soundboardPath + "sweet_victory.wav");
                    break;
                case "champions":
                    Console.WriteLine("Playing 'We are the Champions'");
                    InjectMicrophone(soundboardPath + "champions.wav");
                    break;
                case "lol victory":
                    Console.WriteLine("Playing 'League of Legends Victory'");
                    InjectMicrophone(soundboardPath + "victory.mp3");
                    break;
                case "lol defeat":
                    Console.WriteLine("Playing 'League of Legends Defeat'");
                    InjectMicrophone(soundboardPath + "defeat.mp3");
                    break;
                case "sad":
                    Console.WriteLine("Playing 'Sad Violin'");
                    InjectMicrophone(soundboardPath + "sad_violin.wav");
                    break;
                case "disappointment":
                    Console.WriteLine("Playing 'Disappointment is Immeasurable'");
                    InjectMicrophone(soundboardPath + "disappointment.wav");
                    break;
                case "kronk":
                    Console.WriteLine("Playing 'It Doesn't Make Sense'");
                    InjectMicrophone(soundboardPath + "kronk.wav");
                    break;
                case "run away":
                    Console.WriteLine("Playing 'Run Away!'");
                    InjectMicrophone(soundboardPath + "run_away.wav");
                    break;
                default:
                    break;
            }
        }

        //public static void CheckForSongs(string soundName)
        //{
        //    switch (soundName.ToLower())
        //    {
        //        case "pumped":
        //            Console.WriteLine("Playing 'Pumped up Kicks'");
        //            InjectMicrophone(soundboardPath + "pumped_up_kicks.mp3");
        //            break;
        //    }
        //}

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
                case Keys.NumPad1://run away
                    CheckForSound(Pickrandom(new List<string> { "fly", "run away" }));
                    break;
                case Keys.NumPad2://sad/losing
                    CheckForSound(Pickrandom(new List<string> { "losing horn", "m", "lol defeat", "sad", "disappointment"}));
                    break;
                case Keys.NumPad3://winning
                    CheckForSound(Pickrandom(new List<string> { "sweet victory", "champions", "lol victoy" }));
                    break;
                case Keys.NumPad4:
                    CheckForSound("r");
                    break;
                case Keys.NumPad5:
                    CheckForSound("a");
                    break;
                case Keys.NumPad6:
                    CheckForSound("deja");
                    break;
                case Keys.NumPad7:
                    CheckForSound("kronk");
                    break;
                case Keys.NumPad8:
                    CheckForSound("mad");
                    break;
                case Keys.NumPad9:
                    CheckForSound("nani");
                    break;
                case Keys.NumLock:
                    Console.Clear();
                    Console.WriteLine("Type 'help' for commands.");
                    Application.Exit();
                    break;
            }
            Console.Clear();
            Console.WriteLine(hotkeyHelp);
        }    

        static void Main(string[] args) //build : dotnet build -r win10-x64
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);


            Console.WriteLine("Type 'help' for commands.");
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "exit") // exit check
                {
                    break;
                }else if (line.ToLower() == "hotkey") //hot key mode
                {
                    Console.Clear();
                    Console.WriteLine(hotkeyHelp);

                    LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
                    kbh.OnKeyPressed += kbh_OnKeyPressed;
                    kbh.HookKeyboard();

                    Application.Run();

                    kbh.UnHookKeyboard(); 
                }else if(line.ToLower().StartsWith("play ")) //youtube mode
                {
                    play_youtube_audio(line.Substring(5, line.Length-5));
                }
                else //console mode
                {
                    CheckForSound(line);
                }
            }
        }
    }
}
