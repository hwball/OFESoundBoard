using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OFE
{
    class Program
    {
        static string soundboardPath = "C:\\Users\\Hamish\\Music\\Soundboard\\";

        static string help =
                        "___________________________\n\n" +
                        "fly    - 'Fly You Fools'\n" +
                        "deja   - 'Deja Vu' \n" +
                        "a      - 'Metal Gear alert' \n" +
                        "m      - 'Mission Failed' \n" +
                        "r      - 'Why are you running' \n" +
                        "mad    - 'Is only game' \n" +
                        "shroud - 'The fuck i am' \n" +
                        "nani   - 'Nani!'\n" +
                        "omae   - 'Omae wa'\n" +
                        "off    - 'oof'" +
                        "\n\n___________________________\n\n";

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
            }
        }

        static void Main(string[] args) //build : dotnet build -r win10-x64
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool run = true;
            Console.WriteLine(help);

            LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
            kbh.OnKeyPressed += kbh_OnKeyPressed;
            kbh.HookKeyboard();

            Application.Run();

            kbh.UnHookKeyboard();

            while (run)
            {
                string line = Console.ReadLine();
                if (line == "exit") // Check string
                {
                    run = false;
                    break;
                }
                else
                {
                    CheckForSound(line);
                }
            }
        }
    }
}
