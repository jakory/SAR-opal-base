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
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace opal
{
   /**
    * Social Stories
    *
    */
    public class SocialStories : MonoBehaviour
    {
        MainGameController mgc;
        
        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start ()
        {
            // find maingamecontroller
            this.mgc = (MainGameController)GameObject.FindGameObjectWithTag(
                Constants.TAG_DIRECTOR).GetComponent<MainGameController>();
            if(this.mgc == null) {
                Logger.Log("ERROR: Could not find main game controller!");
            } else {
                Logger.Log("Got main game controller");
            }
            
            // TODO setup demo game using this?
            // load background, story scene slots, and answer slots
            //this.mgc.SetupSocialStoryScene(4, false, 5);
        }
       
        void OnEnable ()
        {  
        }
        
        void OnDisable ()
        {   
        }
        
        void Update ()
        {
        }
        
    }
}
