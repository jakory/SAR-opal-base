// Jacqueline Kory Westlund
// June 2016
//
// The MIT License (MIT)
// Copyright (c) 2016 Personal Robots Group
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using UnityEngine;

namespace opal
{

    // done playing sidekick audio event -- fire when we're done playing
    // an audio file so others can know when we're done
    public delegate void DonePlayingEventHandler(object sender);
    
    public class Sidekick : MonoBehaviour
    {
        AudioSource audioSource = null;
        Animator animator = null;
        bool checkAudio = false;
        bool checkAnim = false;
        string currAnim = Constants.ANIM_DEFAULT;
        bool playingAnim = false;
        
        public event DonePlayingEventHandler donePlayingEvent;
        
        // fader for fading out the screen
        //private GameObject fader = null; 
        
        
        /// <summary>
        /// On starting, do some setup
        /// </summary>
        void Awake()
        {
            // get the sidekick's audio source once
            this.audioSource = this.gameObject.GetComponent<AudioSource>();
            
            // if we didn't find a source, create once
            if (this.audioSource == null)
            {
                this.audioSource = this.gameObject.AddComponent<AudioSource>();
            }
            
            // TODO load all audio in Resources/Sidekick folder ahead of time?
            
            // get the sidekick's animator source once
            this.animator = this.gameObject.GetComponent<Animator>();
            
            // if we didn't find a source, create once
            if (this.animator == null)
            {
                this.animator = this.gameObject.AddComponent<Animator>();
            }
            
            
            // set up fader
            /*this.fader = GameObject.FindGameObjectWithTag(Constants.TAG_FADER);
            if(this.fader != null) {
                this.fader.SetActive(false);
                Logger.Log("Got fader: " + this.fader.name);
            } else {
                Logger.LogError("ERROR: No fader found");
            }*/
            
        }
        
        // Start
        void Start ()
        {
            // always start in an idle, no animations state
            foreach (string flag in Constants.ANIM_FLAGS.Values)
            {
                this.animator.SetBool(flag, false);
            }
        }
        
        void OnEnable ()
        {
        }
        
        void OnDisable ()
        {   
        }
        
        // Update is called once per frame
        void Update ()
        {
            // we started playing audio and we're waiting for it to finish
            if (this.checkAudio && !this.audioSource.isPlaying)
            {
                // we're done playing audio, tell sidekick to stop playing
                // the speaking animation
                Logger.Log("done speaking");
                this.checkAudio = false;
                // NOTE right now we're just fading screen when touch is diabled
                // but we could easily just fade screen when toucan speaks, here
                //this.fader.SetActive(false);
                this.animator.SetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK],false);
                // fire event to say we're done playing audio
                if(this.donePlayingEvent != null) {
                    this.donePlayingEvent(this);
                }
            }
            
            // we started playing an animation and we're waiting for it to finish
            if (this.checkAnim && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.currAnim))
            {
                this.playingAnim = true;
            }
            else if (this.checkAnim && this.playingAnim)
            {
                Logger.Log("done playing animation " + this.currAnim);
                this.playingAnim = false;
                this.checkAnim = false;
                this.animator.SetBool(Constants.ANIM_FLAGS[this.currAnim], false);
                this.currAnim = Constants.ANIM_DEFAULT;
            }
        }
        
        /// <summary>
        /// Loads and  the  sound attached to the object, if one exists
        /// </summary>
        /// <returns><c>true</c>, if audio is played <c>false</c> otherwise.</returns>
        /// <param name="utterance">Utterance to say.</param>
        public bool SidekickSay (string utterance)
        { 
            if (utterance.Equals(""))
            {
                Logger.LogWarning("Sidekick was told to say an empty string!");
                return false;
            }
                
            // try loading a sound file to play
            try {
                // to load a sound file this way, the sound file needs to be in an existing 
                // Assets/Resources folder or subfolder 
                this.audioSource.clip = Resources.Load(Constants.AUDIO_FILE_PATH + 
                                                  utterance) as AudioClip;
            } catch(UnityException e) {
                Logger.LogError("ERROR could not load audio: " + utterance + "\n" + e);
                return false;
            }
            this.audioSource.loop = false;
            this.audioSource.playOnAwake = false;
            
            // then play sound if it's not playing
            if (!this.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                // start the speaking animation
                //Logger.Log("flag is ... " 
                //    + this.animator.GetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK]));
                
                this.animator.SetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK],true);
                
                //Logger.Log("going to speak ... " 
                //    + this.animator.GetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK]));
                
                // play audio
                this.gameObject.GetComponent<AudioSource>().Play();
                this.checkAudio = true;
                // NOTE right now we're just fading screen when touch is diabled
                // but we could easily just fade screen when toucan speaks, here
                //this.fader.SetActive(true);
            }
           
           return true;
        }
    
        /// <summary>
        /// Sidekick play an animation
        /// </summary>
        /// <returns><c>true</c>, if successful <c>false</c> otherwise.</returns>
        /// <param name="props">thing to do</param>
        public bool SidekickDo (string action)
        {
            if (action.Equals(""))
            {
                Logger.LogWarning("Sidekick was told to do an empty string!");
                return false;
            }
        
            // now try playing animation
            try {
                // start the animation
                Logger.Log("flag is ... " + this.animator.GetBool(Constants.ANIM_FLAGS[action]));
                this.animator.SetBool(Constants.ANIM_FLAGS[action],true);
                this.currAnim = action;
                this.checkAnim = true;
                Logger.Log("going to do " + action + " ... " 
                    + this.animator.GetBool(Constants.ANIM_FLAGS[action]));
                
            }
            catch (Exception ex)
            {
                Logger.LogError("Could not play animation " + action + ": " + ex);
                return false;
            }
            return true;
        }
    }
}

