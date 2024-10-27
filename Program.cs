using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics;
using NAudio.Wave;
namespace MediaPlayerFramework
{
    internal class Program
    {

        static void Main(string[] args)
        {


           


            AudioClip sound1 = new AudioClip(name: "Sample 1", path: "C:\\Users\\adminstrator\\Downloads\\sample1.mp3", duration: "00:07");
            AudioClip sound2 = new AudioClip(name: "Sample 2", path: "C:\\Users\\adminstrator\\Downloads\\sample2.mp3", duration: "00:10");
            AudioClip sound3 = new AudioClip(name: "Sample 3", path: "C:\\Users\\adminstrator\\Downloads\\sample3.mp3", duration: "00:13");

         

            sound2.PlayStream();

            sound1.PlayStream();

            sound3.PlayStream();

            Console.ReadLine();


          
        }

        public static void PlayMusic(string FilePath)
        {
            SoundPlayer MusicPlayer = new SoundPlayer();
            MusicPlayer.SoundLocation = FilePath;
            MusicPlayer.Play();
        
            

        }
        public static void StopMusic(string FilePath)
        {
            SoundPlayer MusicPlayer = new SoundPlayer();
            MusicPlayer.SoundLocation = FilePath;
            MusicPlayer.Stop();
            

        }

        public static void print_on_cursor(int x, int y, string word)
        {
            //Console.Clear();
            Console.SetCursorPosition(x,y);
            Console.WriteLine(word);
        } 

        public static string seconds_to_time(int seconds)
        {
            string min_str;
            string sec_str;
            int min = seconds / 60;
            int sec = seconds % 60;

            //Console.WriteLine($"minutes : {min}, Seconds : {sec}");
            if(min < 10)
            {
                min_str = $"0{min}";
            }
            else
            {
                min_str = min.ToString();
            }

            if (sec < 10)
            {
                sec_str = $"0{sec}";
            }
            else
            {
                sec_str= sec.ToString();
            }

            return $"{min_str}:{sec_str}";

        }

    }
}



