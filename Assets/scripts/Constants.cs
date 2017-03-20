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
    // log message event -- fire when you want to log something
    // so others who do logging can listen for the messages
    public delegate void LogEventHandler(object sender,LogEvent logme);

    // object to be moved
    public struct MoveObject
    {
        public string name;
        public Vector3 destination;
    }

    // objects to set correct/incorrect flags for social stories answer options
    public struct SetCorrectObject
    {
        public string[] correct;
        public string[] incorrect;
    }
    
    // object with info for setting up a social stories scene
    public struct SetupStorySceneObject
    {
        public int numScenes;
        public int numAnswers;
        public bool scenesInOrder;

    }
    
    // configuration
    public struct GameConfig
    {
        public string server;
        public string port;
        public bool sidekick;
        public bool logDebugToROS;
        public string opalCommandTopic;
        public string opalActionTopic;
        public string opalLogTopic;
        public string opalSceneTopic;
        public string opalAudioTopic;


    }

	public struct StoryInfo
	{
		public string StoryName;
		public bool reload;
	}



    public static class Constants
    {
		
        /** where images to load as sprites are located in Resources */
        public const string GRAPHICS_FILE_PATH = "graphics/base-images/";
        public const string AUDIO_FILE_PATH = "audio/";
		public const string STORY_FILE_PATH="graphics/stories/";
		public const string STORY_NAME = "story1";
        public const string FROG_FILE_PATH = "stories/story1";
        public const string SOCIAL_STORY_FILE_PATH = "socialstories/";
        public const string SS_SCENESLOT_PATH = "scene-slots/";
        public const string SS_ANSWER_SLOT_PATH = "answer-slots/";
        public const string SS_SCENES_PATH = "scenes/";
        public const string SS_SLOT_NAME = "slot";
        public const string SS_FEEDBACK_PATH = "feedback/";
        public const string SS_CORRECT_FEEDBACK_NAME = "pos-feedback2";
        public const string SS_INCORRECT_FEEDBACK_NAME = "neg-feedback2";
        
        /// <summary>
        /// tags applied to game objects 
        /// </summary>
        public const string TAG_PLAY_OBJECT = "PlayObject";
        public const string TAG_LIGHT = "Light";
        public const string TAG_GESTURE_MAN = "GestureManager";
        public const string TAG_BACKGROUND = "Background";
        public const string TAG_FOREGROUND = "Foreground";
        public const string TAG_SIDEKICK = "Sidekick";
        public const string TAG_SIDEKICK_LIGHT = "SidekickLight";
        public const string TAG_BACK = "GoBack";
        public const string TAG_FADER = "Fader";
        public const string TAG_FADER_ALL = "FaderAll";
        public const string TAG_DIRECTOR = "Director";
        public const string TAG_CAMERA = "MainCamera";
        public const string TAG_GO_NEXT = "GoNext";
        public const string TAG_CORRECT_FEEDBACK = "CorrectFeedback";
        public const string TAG_INCORRECT_FEEDBACK = "IncorrectFeedback";
        public const string TAG_ANSWER_SLOT = "AnswerSlot";
    
        // DEMO - scene numbers (by index -- see list of scenes in build settings)
        public const int SCENE_DEMO_INTRO = 0;
        public const int SCENE_1_PACK = 1;
        public const int SCENE_2_ZOO = 2;
        public const int SCENE_3_PICNIC = 3;
        public const int SCENE_4_PARK = 4;
        public const int SCENE_5_ROOM = 5;
        public const int SCENE_6_BATH = 6;
        public const int SCENE_7_PARTY = 7;
        public const int SCENE_8_BYE = 8;
        
        // DEMO - names for scenes
        public const string NAME_1_PACK = "Session1";
        public const string NAME_2_ZOO = "Session2";
        public const string NAME_3_PICNIC = "Session3";
        public const string NAME_4_PARK = "Session4";
        public const string NAME_5_ROOM = "Session5";
        public const string NAME_6_BATH = "Session6";
        public const string NAME_7_PARTY = "Session7";
        public const string NAME_8_BYE = "Session8";
    
        // layers
        public const int LAYER_MOVEABLES = 10;
        public const int LAYER_STATICS = 8;
    
        // z positions
        public const int Z_BACKGROUND = 3;
        public const int Z_FOREGROUND = -4;
        public const int Z_PLAY_OBJECT = 0;
        public const int Z_FEEDBACK = -1;
        public const int Z_SLOT = 1;
        public const int Z_COLLIDE_SLOT = 2;
    
        // for social story game, slot names
        public const string SCENE_SLOT = "scene-slot";
        public const string ANSWER_SLOT = "answer-slot";
        public const string SCENE_COLLIDE_SLOT = "scene-collide-slot";
    
        /** messages we can receive */
        public const int DISABLE_TOUCH = 1;
        public const int ENABLE_TOUCH = 2;
        public const int RESET = 0;
        public const int SIDEKICK_SAY = 4;
        public const int SIDEKICK_DO = 3;
        public const int LOAD_OBJECT = 5;
        public const int CLEAR = 6;
        public const int MOVE_OBJECT = 7;
        public const int HIGHLIGHT_OBJECT = 8;
        public const int REQUEST_KEYFRAME = 9;
        public const int FADE_SCREEN = 10;
        public const int UNFADE_SCREEN = 11;
        public const int NEXT_PAGE = 12;
        public const int PREV_PAGE = 13;
        public const int EXIT = 14;
        public const int SET_CORRECT = 15;
        public const int SHOW_CORRECT = 16;
        public const int HIDE_CORRECT = 17;
        public const int SETUP_STORY_SCENE = 18;
		public const int STORY_SELECTION = 19;
        
        /** next page and previous page */
        public const bool NEXT = true;
        public const bool PREVIOUS = false;
    
        /** sidekick animations */
        // name of each animation, from unity editor
        // we can receive rosmsgs with name of animation to play
        public const string ANIM_DEFAULT = "Default";
        public const string ANIM_SPEAK = "BeakOpenClose";
        public const string ANIM_FLAP = "FlapWings";
        public const string ANIM_FLAP_BEAKOPEN = "FlapBeakOpen";
        // flags for playing each animation (animator parameters)
        public static readonly Dictionary<string, string> ANIM_FLAGS = new Dictionary<string, string>
        {
            { ANIM_SPEAK, "Speak" },
            { ANIM_FLAP, "Fly" },
            { ANIM_FLAP_BEAKOPEN, "FlyBeakOpen"}
        };
        
        // DEMO sidekick speech
        public static string[] DEMO_SIDEKICK_SPEECH = new string[] { "ImAToucan", 
            "ImFromSpain", "AdiosSeeYouNext", ""};
        
        /** config file path */
        // if playing in unity on desktop:
        public const string OPAL_CONFIG = "opal_config.txt";
        public const string CONFIG_PATH_OSX = @"/Resources/";
        // if playing on tablet:
        public const string CONFIG_PATH_ANDROID = "mnt/sdcard/edu.mit.media.prg.sar.opal.base/";
        // if a linux game:
        public const string CONFIG_PATH_LINUX = "/Resources/";
    
    
        /** Default ROS-related constants: topics and message types */
        // general string log messages (e.g., "started up", "error", whatever)
        public static string LOG_ROSTOPIC = "/opal_tablet";
        public const string DEFAULT_LOG_ROSTOPIC = "/opal_tablet";
        public const string LOG_ROSMSG_TYPE = "std_msgs/String";
        // messages about actions taken on tablet (e.g., tap occurred on object x at xyz)
        // contains: 
        //  string object: name
        //  string action_type: tap
        //  float[] position: xyz
        public static string ACTION_ROSTOPIC = "/opal_tablet_action";
        public const string DEFAULT_ACTION_ROSTOPIC = "/opal_tablet_action";
        public const string ACTION_ROSMSG_TYPE = "/sar_opal_msgs/OpalAction";
        // messages logging the entire current scene
        // contains:
        //  string background
        //  objects[] { name posn tag }
        public static string SCENE_ROSTOPIC = "/opal_tablet_scene";
        public const string DEFAULT_SCENE_ROSTOPIC = "/opal_tablet_scene";
        public const string SCENE_ROSMSG_TYPE = "/sar_opal_msgs/OpalScene";
        // commands from elsewhere that we should deal with
        public static string CMD_ROSTOPIC = "/opal_tablet_command";
        public const string DEFAULT_CMD_ROSTOPIC = "/opal_tablet_command";
        public const string CMD_ROSMSG_TYPE = "/sar_opal_msgs/OpalCommand";   
        // messages to tell the game node when we're done playing audio
        // contains:
        //   bool done playing
        public static string AUDIO_ROSTOPIC = "/opal_tablet_audio";
        public const string DEFAULT_AUDIO_ROSTOPIC = "/opal_tablet_audio";
        public const string AUDIO_ROSMSG_TYPE = "/std_msgs/Bool";     
    }
}