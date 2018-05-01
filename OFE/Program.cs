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
                        "off                - 'oof'" +
                        "\n\n___________________________________________________________\n\n";

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
                default:
                    return;
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
            if (System.IO.File.Exists(outputFile.Filename))
            {
                System.IO.File.Delete(outputFile.Filename);
            }

        }

        public static void kbh_OnKeyPressed(object sender, Keys e)
        {

            switch (e)
            {
                case Keys.NumPad1:
                    CheckForSound("fly");
                    break;
                case Keys.NumPad2:
                    CheckForSound("oof");
                    break;
                case Keys.NumPad3:
                    CheckForSound("oof");
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
                    CheckForSound("m");
                    break;
                case Keys.NumPad8:
                    CheckForSound("mad");
                    break;
                case Keys.NumPad9:
                    CheckForSound("nani");
                    break;
                case Keys.NumLock:
                    Application.Exit();
                    break;
            }
        }    

        static void Main(string[] args) //build : dotnet build -r win10-x64
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            Console.WriteLine(help);
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "exit") // exit check
                {
                    break;
                }else if (line.ToLower() == "hotkey") //hot key mode
                {
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
