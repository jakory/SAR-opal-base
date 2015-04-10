using System;
using UnityEngine;

namespace opal
{
    public class Sidekick : MonoBehaviour
    {
        AudioSource audioSource = null;
        Animator animator = null;
        bool checkAudio = false;
        bool checkAnim = false;
        string currAnim = Constants.ANIM_DEFAULT;
        bool playingAnim = false;
        
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
            
        }
        
        // Start
        void Start ()
        {
            // always start in an idle, no animations state
            foreach (string flag in Constants.ANIM_FLAGS.Values)
            {
                this.animator.SetBool(flag, false);
                Debug.Log("flag " + flag + " is ... " + this.animator.GetBool(flag));
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
                Debug.Log("done speaking");
                this.checkAudio = false;
                this.animator.SetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK],false);
            }
            
            // we started playing an animation and we're waiting for it to finish
            if (this.checkAnim && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.currAnim))
            {
                this.playingAnim = true;
            }
            else if (this.checkAnim && this.playingAnim)
            {
                Debug.Log("done playing animation " + this.currAnim);
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
                Debug.LogWarning("Sidekick was told to say an empty string!");
                return false;
            }
                
            // try loading a sound file to play
            try {
                // to load a sound file this way, the sound file needs to be in an existing 
                // Assets/Resources folder or subfolder 
                this.audioSource.clip = Resources.Load(Constants.AUDIO_FILE_PATH + 
                                                  utterance) as AudioClip;
            } catch(UnityException e) {
                Debug.LogError("ERROR could not load audio: " + utterance + "\n" + e);
                return false;
            }
            this.audioSource.loop = false;
            this.audioSource.playOnAwake = false;
            
            // then play sound if it's not playing
            if (!this.gameObject.audio.isPlaying)
            {
                // start the speaking animation
                Debug.Log("flag is ... " 
                    + this.animator.GetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK]));
                
                this.animator.SetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK],true);
                
                Debug.Log("going to speak ... " 
                    + this.animator.GetBool(Constants.ANIM_FLAGS[Constants.ANIM_SPEAK]));
                
                // play audio
                this.gameObject.audio.Play();
                this.checkAudio = true;
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
                Debug.LogWarning("Sidekick was told to do an empty string!");
                return false;
            }
        
            // now try playing animation
            try {
                // start the animation
                Debug.Log("flag is ... " + this.animator.GetBool(Constants.ANIM_FLAGS[action]));
                this.animator.SetBool(Constants.ANIM_FLAGS[action],true);
                this.currAnim = action;
                this.checkAnim = true;
                Debug.Log("going to do " + action + " ... " 
                    + this.animator.GetBool(Constants.ANIM_FLAGS[action]));
                
            }
            catch (Exception ex)
            {
                Debug.LogError("Could not play animation " + action + ": " + ex);
                return false;
            }
            return true;
        }
    }
}
