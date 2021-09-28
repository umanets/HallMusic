using HallAdPlayer.Models;
using HallAdPlayer.Public;
using NAudio.Wave;
using System;
using System.Threading;

namespace HallAdPlayer
{
    public class Player
    {
        private readonly WaveOutEvent _outputDevice;
        private readonly float volume;
        private string currentFile;
        public event EventHandler Stopped;

        public Player(float volume)
        {
            this.volume = volume;

            _outputDevice = new WaveOutEvent
            {
                Volume = volume
            };

            _outputDevice.PlaybackStopped += (o, e) => this.Stopped?.Invoke(this, EventArgs.Empty);
        }

        public void Play(MusicFile file) 
        {
            Console.WriteLine(file.FileName);
            currentFile = file.FileName;
            if (_outputDevice.PlaybackState == PlaybackState.Stopped)
            { 
                try 
                { 
                    _outputDevice.Init(new AudioFileReader(file.FileName));
                    _outputDevice.Play();
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"\n\r---- ERROR ---------------------");
                    Console.WriteLine($"Can't play {file.FileName}. \n\rError: {ex.Message}");
                    Console.WriteLine($"-------------------------------- \n\r");
                    this.Stopped?.Invoke(this, EventArgs.Empty);
                }
                
            }
        }

        public void Resume() 
        { 
            if (_outputDevice.PlaybackState == PlaybackState.Stopped)
            { 
                this.Stopped(this, EventArgs.Empty);
            } 
            else 
            { 
                Console.WriteLine($"Resume: {currentFile}");
                _outputDevice.Play();
            }
        }

        public void Stop() 
        { 
            _outputDevice.Stop();
        }

        public void Pause()
        {
            _outputDevice.Pause();
        }
        
        public void ResetVolume() 
        { 
            this._outputDevice.Volume = this.volume;
        }

        public void FadeIn()
        { 
            var volinc = _outputDevice.Volume / 10;
            while (_outputDevice.Volume > volinc) 
            { 
                _outputDevice.Volume += -volinc;
                Thread.Sleep(200);
            }
            _outputDevice.Volume = 0;
        }

        public void FadeOut()
        { 
            _outputDevice.Volume = 0;
            var volinc = this.volume / 10;
            while (_outputDevice.Volume < volume - volinc) 
            { 
                _outputDevice.Volume += volinc;
                Thread.Sleep(200);
            }
            _outputDevice.Volume = this.volume;
        }
    }
}
