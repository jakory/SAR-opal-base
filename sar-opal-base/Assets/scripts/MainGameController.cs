﻿using UnityEngine;
using System;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Hit;

/**
 * The SAR-opal-base game main controller. Orchestrates everything: 
 * sets up to receive input via ROS, initializes scenes and creates 
 * game objecgs based on that input, deals with touch events and
 * other tablet-specific things.
 */
public class MainGameController : MonoBehaviour
{
    // gesture manager
    private GestureManager gestureManager = null;
    
    // rosbridge websocket client
    private RosbridgeWebSocketClient clientSocket = null;

    /** Called on start, use to initialize stuff  */
    void Start ()
    {
        // find gesture manager
        FindGestureManager(); 
       
        // Create a new game object programmatically as a test
        PlayObjectProperties pops = new PlayObjectProperties();
        pops.setAll("ball2", Constants.TAG_PLAY_OBJECT, false, "chimes", 
                    new Vector3 (-200, 50, 2), null);
        this.InstantiatePlayObject (pops);
        
        // Create a new background programmatically as a test
        BackgroundObjectProperties bops = new BackgroundObjectProperties();
        bops.setAll("playground", Constants.TAG_BACKGROUND, 
                    new Vector3(0,0,0));
        this.InstantiateBackground(bops);
        
		// set up rosbridge websocket client
		// note: does not attempt to reconnect if connection fails
		if (this.clientSocket == null)
		{
            // load websocket config from file
            string server = "";
            string port = "";
            RosbridgeUtilities.DecodeWebsocketJSONConfig(Application.dataPath +
                "/Resources/websocket_config.txt",
                out server, out port);
        
			this.clientSocket = new RosbridgeWebSocketClient(
                server, // can pass hostname or IP address
                port);
			
			this.clientSocket.SetupSocket();
			this.clientSocket.receivedMsgEvent += 
				new ReceivedMessageEventHandler(HandleClientSocketReceivedMsgEvent);
				
			this.clientSocket.SendMessage(RosbridgeUtilities.GetROSJsonAdvertiseMsg(
                Constants.OUR_ROSTOPIC, Constants.OUR_ROSMSG_TYPE));
            this.clientSocket.SendMessage(RosbridgeUtilities.GetROSJsonSubscribeMsg(
                Constants.CMD_ROSTOPIC, Constants.CMD_ROSMSG_TYPE));
            this.clientSocket.SendMessage(RosbridgeUtilities.GetROSJsonPublishMsg(
                Constants.OUR_ROSTOPIC, "Opal tablet checking in!"));
		}
    }

    /** On enable, initialize stuff */
    private void OnEnable ()
    {
        
    }

    /** On disable, disable some stuff */
    private void OnDestroy ()
    {
		// close websocket
		if (this.clientSocket != null)
		{
			this.clientSocket.CloseSocket();
    
			// unsubscribe from received message events
			this.clientSocket.receivedMsgEvent -= HandleClientSocketReceivedMsgEvent;
		}
		
		Debug.Log("destroyed main game controller");
    }
    
    /** 
     * Update is called once per frame 
     */
    void Update ()
    {
        // if user presses escape or 'back' button on android, exit program
        if (Input.GetKeyDown (KeyCode.Escape))
            Application.Quit ();
    }


    /**
     * Instantiate a new game object with the specified properties
     */
    void InstantiatePlayObject (PlayObjectProperties pops)
    {
        GameObject go = new GameObject ();

        // set object name
        go.name = (pops.Name () != "") ? pops.Name () : UnityEngine.Random.value.ToString ();
        Debug.Log ("Creating new play object: " + pops.Name ());

        // set tag
        go.tag = Constants.TAG_PLAY_OBJECT;

        // move object to initial position 
        go.transform.position = pops.InitPosition();//pops.initPosn.x, pops.initPosn.y, pops.initPosn.z);

        // load audio - add an audio source component to the object if there
        // is an audio file to load
        if (pops.AudioFile() != null) {
            AudioSource audioSource = go.AddComponent<AudioSource>();
            try {
                // to load a sound file this way, the sound file needs to be in an existing 
                // Assets/Resources folder or subfolder 
                audioSource.clip = Resources.Load(Constants.AUDIO_FILE_PATH + 
                                                  pops.AudioFile()) as AudioClip;
            } catch (UnityException e) {
                Debug.Log("ERROR could not load audio: " + pops.AudioFile() + "\n" + e);
            }
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

        // load sprite/image for object
        SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>(Constants.GRAPHICS_FILE_PATH + pops.Name());
        if (sprite == null)
            Debug.Log ("ERROR could not load sprite: " 
                + Constants.GRAPHICS_FILE_PATH + pops.Name());
        spriteRenderer.sprite = sprite; 

        // TODO should this be a parameter too?
        go.transform.localScale = new Vector3 (100, 100, 100);

        // add rigidbody
        Rigidbody2D rb2d = go.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0; // don't want gravity, otherwise objects will fall

        // add polygon collider
        go.AddComponent<CircleCollider2D>();

        // add and subscribe to gestures
        if (this.gestureManager == null ) {
            Debug.Log ("ERROR no gesture manager");
            FindGestureManager();
        }
        
        // add gestures and register to get event notifications
        this.gestureManager.AddAndSubscribeToGestures(go, pops.draggable);
        
        // add pulsing behavior (draws attention to actionable objects)
        go.AddComponent<GrowShrinkBehavior>();
        
    }
    
    /// <summary>
    /// Instantiates a background image object
    /// </summary>
    /// <param name="bops">properties of the background image object to load</param>
    private void InstantiateBackground(BackgroundObjectProperties bops)
    {
        // remove previous background if there was one
        this.DestroyObjectsByTag(new string[] {Constants.TAG_BACKGROUND});
    
        // now make a new background
        GameObject go = new GameObject();
        
        // set object name
        go.name = (bops.Name() != "") ? bops.Name() : UnityEngine.Random.value.ToString ();
        Debug.Log ("Creating new background: " + bops.Name ());
        
        // set tag
        go.tag = Constants.TAG_BACKGROUND;
        
        // move object to initial position 
        go.transform.position = new Vector3(0,0,0);
        
        // load sprite/image for object
        SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>(Constants.GRAPHICS_FILE_PATH + bops.Name());
        if (sprite == null)
            Debug.Log ("ERROR could not load sprite: " 
                       + Constants.GRAPHICS_FILE_PATH + bops.Name());
        spriteRenderer.sprite = sprite; 
        
        // TODO should this be a parameter too?
        go.transform.localScale = new Vector3 (100, 100, 100);
        
        
    }
    
    /** Find the gesture manager */ 
    private void FindGestureManager()
    {
        // find gesture manager
        this.gestureManager = (GestureManager) GameObject.FindGameObjectWithTag(
            Constants.TAG_GESTURE_MAN).GetComponent<GestureManager>();
        if (this.gestureManager == null) {
            Debug.Log("ERROR: Could not find gesture manager!");
        }
        else {
            Debug.Log("Got gesture manager");
        }
    }
    
    /**
     * Received message from remote controller - process and deal with message
     * */
    void HandleClientSocketReceivedMsgEvent (object sender, int cmd, object props)
    {
        Debug.Log ("!! MSG received from remote: " + cmd);
                
        // process first token to determine which message type this is
        // if there is a second token, this is the message argument
        switch (cmd)
        {
            case Constants.DISABLE_TOUCH:
                // disable touch events from user
                this.gestureManager.allowTouch = false; 
                break;
                
            case Constants.ENABLE_TOUCH:
                // enable touch events from user
                this.gestureManager.allowTouch = true;
                break;
                
            case Constants.RESET:
                // reload the current level
                // e.g., when the robot's turn starts, want all characters back in their
                // starting configuration for use with automatic playbacks
                this.ReloadScene();
                break;
            case Constants.SIDEKICK_DO:
                // TODO trigger animation for sidekick character  
                break;
                
            case Constants.SIDEKICK_SAY:
                // TODO trigger playback of speech for sidekick character
                break;
                
            case Constants.LOAD_OBJECT:
                // TODO instantiate new playobject with the specified properties
                // TODO load new background image with the specified properties
                break;
            
            // TODO what other messages?
        }
    }
    
    
    /**
     * Reload the current scene by moving all objects back to
     * their initial positions and resetting any other relevant
     * things
     */
    void ReloadScene()
    {
        Debug.Log("Reloading current scene...");

        // TODO move all play objects back to their initial positions
        // TODO need to save initial positions for objects for reloading
        
    }
    
    /// <summary>
    /// Destroy objects with the specified tags
    /// </summary>
    /// <param name="tags">tags of objects to destory</param>
    void DestroyObjectsByTag(string[] tags)
    {
        // destroy objects with the specified tags
        foreach (string tag in tags)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            if (objs.Length == 0) return;
            foreach (GameObject go in objs)
            {
                Debug.Log ("destroying " + go.name);
                Destroy(go);
            }
        }
    }

}
