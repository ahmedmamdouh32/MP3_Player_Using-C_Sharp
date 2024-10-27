using System;
using System.Media;
using System.Timers;
using System.Diagnostics;
using NAudio.Wave;
using System.Diagnostics;
namespace MediaPlayerFramework
{
    internal class AudioClip
    {
        public string name { set; get; }
        public string path {  set; get; }
        public string duration {  set; get; }
        public bool finished { get; private set; }

        private  int min;
        private  int sec;

        private int duration_seconds;
        private int SecondsCounter = 0;

        private int ConsoleX;
        private int ConsoleY;
        Timer timer;

        private AudioFileReader audioFile;
        private WaveOutEvent outputDevice;

        Stopwatch stopwatch = new Stopwatch();
        private double previous_time = 0;

        public AudioClip(string name,string path,string duration ) 
        { 
            this.name = name;
            this.path = path;
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(path);
            this.duration = duration;
            string[] parts = duration.Split(':');
            int min = int.Parse(parts[0]);
            int sec = int.Parse(parts[1]);
            duration_seconds = min * 60 + sec;
        }

        public void Play()
        {
            timer = new Timer(1000); // 1000 ms = 1 second
            timer.Elapsed += OnTimedEvent;  // Event handler for each tick
            timer.AutoReset = true;         // Resets after each interval
            timer.Enabled = true;           // Start the timer
            stopwatch.Start();
            previous_time = stopwatch.Elapsed.TotalSeconds;
            finished = false;
            outputDevice.Init(audioFile);
            Console.WriteLine($"Now playing {name}");
            ConsoleX = Console.CursorLeft;  //to get initial position of the console
            ConsoleY = Console.CursorTop;
            Console.WriteLine($"{name}: {min:D2}:{sec:D2}       {(float)((float)SecondsCounter / duration_seconds) * 100:f2}%");
            outputDevice.Play();
        }

        public void Stop()
        {
            outputDevice.Stop();
            Console.WriteLine($"{name} has stopped");
            timer.Stop();
            timer.Dispose();
            finished = true;
            sec = 0;
            min = 0;
            SecondsCounter = 0;
            stopwatch.Reset();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            double elapsed_time = stopwatch.Elapsed.TotalSeconds;
            double passed_time = (stopwatch.Elapsed.TotalSeconds - previous_time);
            if (stopwatch.Elapsed.TotalSeconds - previous_time >= 1)
            {
                previous_time = stopwatch.Elapsed.TotalSeconds;
                if(passed_time > 1)
                {
                    sec += (int)passed_time;
                    SecondsCounter += (int)passed_time;
                }
                else
                {
                    sec++;
                    SecondsCounter++;
                }               

                if (sec >= 60) // Increment minutes if 60 seconds pass
                {
                    sec = 0;
                    min++;
                }
                Console.SetCursorPosition(ConsoleX, ConsoleY);
                Console.WriteLine($"{name}: {min:D2}:{sec:D2}       {(float)((float)SecondsCounter / duration_seconds) * 100:f2}%");
                if (duration == $"{min:D2}:{sec:D2}")
                {
                    timer.Stop();
                    timer.Dispose();
                    finished = true;
                    Console.WriteLine($"{name} finished");
                }
            }
        }

        public void PlayStream()
        {
            Play();
            while (finished == false)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true); // Read the key without displaying it

                    if (keyPressed.Key == ConsoleKey.Spacebar) //puase button
                    {
                        if (outputDevice.PlaybackState == PlaybackState.Paused)
                        {
                            outputDevice.Play();
                            timer.Enabled = true;           // Start the timer
                            stopwatch.Start();
                        }
                        else
                        {
                            outputDevice.Pause();
                            timer.Stop();
                            stopwatch.Stop();
                        }
                    }
                }
            }
            //the audio is finished
            min = 0;
            sec = 0;
            SecondsCounter = 0;
            outputDevice.Dispose();
            timer.Dispose();
            stopwatch.Reset();
        }
    }
}
