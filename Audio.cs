using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SquareEngine
{
     /*
      * Class AudioEngine
      * Responsible for playing songs and sound effects
      * Implemented - 
      *     Can play songs, or a single sound effect
      * 
      * To Implement -
      *     Play/Pause songs
      *     play multiple instances of a sound effect
      *     Audio manipulation: Louder Left Channel etc.
      *     
      * How to use:
      * This class is initialized in Engine during SetupEngine().
      * Call Engine.Audio.<function> from game
      * 
      * Content must be loaded in loadContent.
      * 
      * Global fields-
      * soundlist - List of soundeffects that have been added to the engine
      * soundeffectlist - List of soundefffect instances that are currently in use
      * songlist - list of songs that have been added to the engine
      * lookup - contains all songs/soundeffects.
      * 
      * Function List
      * public:
      * public AudioEngine()
      * public void Add(string,string,Type)
      * public void Play(string)
      * public void Stop(string)
      * public void Pause(string)
      * public void getStatus(string,type)
      * private:
      */
    public class AudioEngine
    {
        //public List<Song> soundlist = new List<Song>();
        public Dictionary<string, SoundEffect> soundlist = new Dictionary<string,SoundEffect>();
        public Dictionary<string, SoundEffectInstance> soundeffectlist = new Dictionary<string,SoundEffectInstance>();
        public Dictionary<string, Song> songlist = new Dictionary<string, Song>();
        public Dictionary<string, object> lookup = new Dictionary<string, object>();
        public AudioEngine()
        {
        }
        public enum AudioState
        {
            Playing,
            Paused,
            Stopped
        }

        /*
         * public void Add
         * args: String, String, Type
         * String- path in content directory
         * String- internal reference
         * Type- {Song, Soundeffect}
         * 
         * This function is to be called only in the loadContent method of the game.
         * Adds sound or soundeffect to the audio engine register
         */
        public void Add(string loadpath, string ident, Type T)
        {
            if (lookup.ContainsKey(ident))
            {
                return;
            }
            if (T == typeof(Song))
            { 
                Song sng = Engine.Content.Load<Song>(loadpath);
                lookup.Add(ident, sng);
            }
            else if (T == typeof(SoundEffect))
            {
                SoundEffect sng = Engine.Content.Load<SoundEffect>(loadpath);
                lookup.Add(ident, sng);
            }
           
        }

        /*
         * public void Play
         * args: String
         * String- internal name of song/soundeffect
         * 
         * Plays a loaded song or sound effect.
         * 
         * Todo - Clean up Sound Effect playback - instance store is garbage.
         */
        public void Play(string ident)
        {
            if (lookup.ContainsKey(ident))
            {
                if (lookup[ident] is SoundEffect)
                {
                    soundeffectlist.Add(ident, ((SoundEffect)lookup[ident]).CreateInstance());
                    soundeffectlist[ident].Play();                    return;
                }
                else
                {
                    MediaPlayer.Play((Song)lookup[ident]);
                }
            }
        }
        /*
         * public void Stop
         * args: String
         * String - internal name of song/soundeffect
         * 
         * Stops playback
         * 
         * Todo: Clean up soundeffect. Storage is garbage
         */
        public void Stop(string ident)
        {
            if (lookup.ContainsKey(ident))
            {
                if (lookup[ident] is SoundEffect)
                {
                    if (soundeffectlist.ContainsKey(ident))
                        soundeffectlist[ident].Stop();
                    return;
                }
                else
                {
                    MediaPlayer.Stop();
                }
            }
        }

        /*
         * public void Pause
         * args: String
         * String - internal name of song/soundeffect
         * 
         * Pauses playback unless already paused
         */
        public void Pause(string  ident)
        {
            if (lookup.ContainsKey(ident))
            {
                if (lookup[ident] is SoundEffect)
                {
                    if (soundeffectlist.ContainsKey(ident))
                        soundeffectlist[ident].Pause();
                    return;
                }
                else
                {
                    if (MediaPlayer.State == MediaState.Playing)
                        MediaPlayer.Pause();
                    else
                        MediaPlayer.Play((Song)lookup[ident]);
                }
            }
        }

       /*
       * public AudioState getStatus
       * args: string, type
       * String - internal name of song/soundeffect
       * Type - Type of Song/Soundeffect
       * 
       * Gets state of song/sounsdeffect
        * probably a useless function
       */
 
        public AudioState getStatus(string ident, Type T)
        {
            if (T == typeof(SoundEffectInstance))
            {
                if (soundeffectlist.ContainsKey(ident))
                    return (AudioState)soundeffectlist[ident].State;
                else
                    return AudioState.Stopped;
            }
            else if (T == typeof(Song))
            {
                return (AudioState)MediaPlayer.State;
            }
            return AudioState.Stopped;
        }
    }
}

