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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace opal
{

/**
 * properties of the play object, such as initial position and
 * whether the object is draggable
 */
    public class PlayObjectProperties : SceneObjectProperties
    {
        /** the object is draggable (true) or stationary (false) */
        public bool draggable = true;

        /** sound file to play on tap */
        private string audioFile = null;

        /** scale of object */
        private Vector3 scale =  new Vector3(1,1,1);
        
        /// <summary>
        /// For social stories, which scene or answer slot to load this object into.
        /// This value is -1 if no slot is assigned. 
        /// Slots are 1-indexed.
        /// </summary>
        private int slot = -1;
        
        /// <summary>
        /// For social stories, sometimes the story scenes are presented out of order.
        /// If so, this value indicates which scene number is correct for this object
        /// (e.g., is it scene 1, scene 2, etc?)
        /// </summary>
        private int correctSlot = -1;
        
        /// <summary>
        /// Is this a correct response? (social stories)
        /// Note that there are separate flags for correct and incorrect because
        /// it is possible for an object to be neither.
        /// </summary>
        public bool isCorrect = false;
        
        /// <summary>
        /// Is this an incorrect response? (social stories)
        /// Note that there are separate flags for correct and incorrect because
        /// it is possible for an object to be neither.
        /// </summary>
        public bool isIncorrect = false;
        
        /// <summary>
        /// for social stories, whether the slot is an answer or scene
        /// true if answer, false if scene
        /// </summary>
        public bool isAnswerSlot = false;

        /** constructor */
        public PlayObjectProperties()
        {
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="opal.PlayObjectProperties"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="initPosn">Init posn.</param>
        /// <param name="scale">Scale.</param>
        public PlayObjectProperties(string name, string tag, bool draggable, 
                                    string audioFile, Vector3 initPosn, 
                                    Vector3 scale)
        {
            this.SetName(name);
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetInitPosition(initPosn);
            this.scale = scale;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="opal.PlayObjectProperties"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="initPosn">Init posn.</param>
        /// <param name="scale">Scale.</param>
        /// <param name="slot">Slot.</param>
        /// <param name="answerSlot">If <c>true</c> answer slot; if <c>false</c> scene slot.</param>
        public PlayObjectProperties(string name, string tag, bool draggable, 
                                string audioFile, Vector3 initPosn, 
                                Vector3 scale, int slot, bool answerSlot,
                                bool isCorrect, bool isIncorrect)
        {
            this.SetName(name);
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetInitPosition(initPosn);
            this.scale = scale;
            this.SetSlot(slot);
            this.isAnswerSlot = answerSlot;
            this.isCorrect = isCorrect;
            this.isIncorrect = isIncorrect;
            
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="opal.PlayObjectProperties"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="initPosn">Init posn.</param>
        /// <param name="scale">Scale.</param>
        /// <param name="slot">Slot.</param>
        /// <param name="answerSlot">If <c>true</c> answer slot; if <c>false</c> scene slot.</param>
        /// <param name="correctSlot">Correct slot.</param>
        public PlayObjectProperties(string name, string tag, bool draggable, 
                                    string audioFile, Vector3 initPosn, 
                                    Vector3 scale, int slot, bool answerSlot,
                                    int correctSlot, bool isCorrect, bool isIncorrect)
        {
            this.SetName(name);
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetInitPosition(initPosn);
            this.scale = scale;
            this.SetSlot(slot);
            this.isAnswerSlot = answerSlot;
            this.SetCorrectSlot(correctSlot);
            this.isCorrect = isCorrect;
            this.isIncorrect = isIncorrect;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="opal.PlayObjectProperties"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="slot">Slot.</param>
        /// <param name="answerSlot">If <c>true</c> answer slot; if <c>false</c> scene slot.</param>
        public PlayObjectProperties(string name, string tag, bool draggable,
                                    string audioFile, int slot, bool answerSlot, 
                                    bool isCorrect, bool isIncorrect)
        {
            this.SetName(name);
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetSlot(slot);
            this.isAnswerSlot = answerSlot;
            this.isCorrect = isCorrect;
            this.isIncorrect = isIncorrect;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="opal.PlayObjectProperties"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="slot">Slot.</param>
        /// <param name="answerSlot">If <c>true</c> answer slot; if <c>false</c> scene slot.</param>
        /// <param name="correctSlot">Correct slot.</param>
        public PlayObjectProperties(string name, string tag, bool draggable,
                                    string audioFile, int slot, bool answerSlot,
                                    int correctSlot, bool isCorrect, bool isIncorrect)
        {
            this.SetName(name);
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetSlot(slot);
            this.isAnswerSlot = answerSlot;
            this.SetCorrectSlot(correctSlot);
            this.isCorrect = isCorrect;
            this.isIncorrect = isIncorrect;
        }

        /// <summary>
        /// Set all properties - name of object, whether it is a draggable
        ///   object, audio file to attach or null if none, initial position,
        ///   end positions or null if none (stationary object or doesn't matter)
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="draggable">If set to <c>true</c> draggable.</param>
        /// <param name="audioFile">Audio file.</param>
        /// <param name="initPosn">Init posn.</param>
        /// <param name="scale">Scale.</param>
        /// <param name="slot">Slot.</param>
        /// <param name="answerSlot">If <c>true</c> answer slot; if <c>false</c> scene slot.</param>
        public void setAll (string name, string tag, bool draggable, 
                            string audioFile, Vector3 initPosn, Vector3 scale,
                            int slot, bool answerSlot, int correctSlot,
                            bool isCorrect, bool isIncorrect)
        {
            this.SetName(name);       
            this.SetTag(tag);
            this.draggable = draggable;
            this.audioFile = audioFile;
            this.SetInitPosition(initPosn);
            this.scale = scale;
            this.SetSlot(slot);
            this.isAnswerSlot = answerSlot;
            this.SetCorrectSlot(correctSlot);
            this.isCorrect = isCorrect;
            this.isIncorrect = isIncorrect;
        }
    
         /// <summary>
         /// Set scale of object
         /// </summary>
         /// <param name="scale">Scale.</param>
        public void SetScale (Vector3 scale)
        {
            // TODO check if scale is reasonable?
            if (scale.x > 0 && scale.y > 0 && scale.z > 0)
                this.scale = scale;
        }
   
        /** get end positions */
        public Vector3 Scale ()
        {
            return this.scale;
        }

        /** set audio file */
        public void SetAudioFile (string audioFile)
        {
            // TODO audio file - do some kind of error check on this?
            this.audioFile = audioFile;
        }
    
        /** get audio file */
        public string AudioFile ()
        {
            return this.audioFile;
        }

        /// <summary>
        /// Sets the initial position - checks to make sure play objects
        /// are in front of the background plane
        /// </summary>
        /// <param name="posn">position</param>
        public new void SetInitPosition (Vector3 posn)
        {
            // zero and negative numbers are toward the camera, in front of the background
            // so we want to make sure play objects are in front of the background plane
            base.SetInitPosition(new Vector3(posn.x, posn.y, 
                    // if the z position provided is not one of our allowed constants, set it to
                    // the default.
                    ((posn.z != Constants.Z_PLAY_OBJECT &&
                    posn.z != Constants.Z_SLOT &&
                    posn.z != Constants.Z_COLLIDE_SLOT &&
                    posn.z != Constants.Z_FEEDBACK) ? Constants.Z_PLAY_OBJECT : posn.z)));
        }
        
        /// <summary>
        /// Set the slot number to load this object into.
        /// </summary>
        /// <param name="slot">Slot.</param>
        public void SetSlot(int slot)
        {
            if (slot > 0) 
                this.slot = slot;
            else
                this.slot = -1;
        }
        
        /// <summary>
        /// get slot
        /// </summary>
        public int Slot()
        {
            return this.slot;
        }
        
        /// <summary>
        /// Set the slot number that's the correct position for this
        /// object in the story
        /// </summary>
        /// <param name="slot">Correct slot.</param>
        public void SetCorrectSlot(int slot)
        {
            // slots are positive integers
            // but we store it 0-indexed instead of 1-indexed
            if (slot > 0) 
                this.correctSlot = slot-1;
            else
                this.correctSlot = -1;
        }
        
        /// <summary>
        /// get slot
        /// </summary>
        public int CorrectSlot()
        {
            return this.correctSlot;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current
        ///  <see cref="opal.PlayObjectProperties"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current
        /// <see cref="opal.PlayObjectProperties"/>.</returns>
        public override string ToString ()
        {
            return "Properties:" 
                + "\n\tName: " + this.Name()
                + "\n\tTag: " + this.Tag()
                + "\n\tDraggable: " + this.draggable
                + "\n\tAudio: " + this.audioFile
                + "\n\tInitPosn: " + this.initPosn
                + "\n\tScale: " + this.scale
                + "\n\tSlot: " + this.slot
                + "\n\tCorrectSlot: " + this.correctSlot
                + "\n\tIsAnswerSlot: " + this.isAnswerSlot
                + "\n\tIsCorrect: " + this.isCorrect
                + "\n\tIsIncorrect: " + this.isIncorrect;

        }
    }
}