#pragma strict

// This script is used for simple animating without the use of statemachines
// Such as blood splats, attack animation, and perfec/messy popups 

// An array with the sprites used for animation
var animSprites: Sprite[];

// Controls how fast to change the sprites when
// animation is running
var framesPerSecond: float;

var loop: boolean = false;
var destroyOnFinish: boolean = false;

// Reference to the renderer of the sprite
// game object
private var animRenderer: SpriteRenderer;

// Time passed since the start of animatin
private var timeAtAnimStart: float;

// Indicates whether animation is running or not
private var animRunning: boolean = false;
private var completed: boolean = false;

// When object loads...
function Start () {
   // Get a reference to game object renderer and
   // cast it to a Sprite Rendere
   animRenderer = renderer as SpriteRenderer;
}

// At fixed time intervals...
function FixedUpdate () {
   if(!animRunning) {
         animRunning = true;
         
 	     // Record time at animation start
 	     timeAtAnimStart = Time.timeSinceLevelLoad;
   }
}

// Before rendering next frame...
function Update() {   
   
   if(animRunning && !completed) {
      // Animation is running, so we need to 
      // figure out what frame to use at this point
      // in time
      
      // Compute number of seconds since animation started playing
      var timeSinceAnimStart: float;
      timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

      // Compute the index of the next frame    
      var frameIndex: int;
      frameIndex = timeSinceAnimStart * framesPerSecond;

      if(frameIndex < animSprites.Length) {
         // Let the renderer know which sprite to
         // use next      
         animRenderer.sprite = animSprites[ frameIndex ];
      } else {
         // Animation finished, set the render
         // with the first sprite and stop the 
         // animation
         if (!loop) {
         	completed = true;
         } else {
         	animRenderer.sprite = animSprites[0];
         	animRunning = false;
         }
         if (!loop && destroyOnFinish) {
         	// Destroy the object
         	Destroy(gameObject);
         	if (transform.parent != null && transform.parent.name == "slashAnim(Clone)") {
         		Destroy(transform.parent.gameObject);
         	}
         }
      }
   }
}