# MonoGameTestGame

log:
* 9.7.22 - init with walking, hitting character (link), animations with animation manager, player states with state machine + quit button
* 13.7. - tiled map, hitboxes init, map collision
* 15.7 - dialogs, static input class, sounds/music, better sprites
* 15.7 - player inherits map entity, new map, using upscaled snes resolution
* 16.7 - bitmap fonts coz spritefonts suck (blurry in small scale)
* 16.7 - event system init
* 17.7 - split map entities to characters and event triggers, zelda style bitmap font and dialog box, nicer tilemap rendering
* 18.7. - scene management and new map
* 19.7. - moving "camera" to player, float rectangles to fix collision bugs (xna rectangles use int)
* 20.7. - condition and save value events, event data storage (only for bools atm), MapObject class (for bushes) and using Tiled map object layer
* 21.7. - hitting bushes, animation effects, creating sign objects from map, forcing small wait time on scene change (to hide draw lag when creating a lot of map objects)
* 21.7. - scene manager uses SceneTransition class, implemented new transition type FadeToBlack
* 24.7. - guard init, creating text events from Tiled map, dialog lines shift up, sectioned dialog box for parametrized size
* 25.7. - dialog can be skipped again, wood shadow overlay, direction methods, guard states, player takes damage
* 29.7. - shaders!!!, new rendering utility class, enemy class, new enemy bat, enemy death animation, heart display, inputcontroller class
* 31.7. - static music class, mild noise shader, characters collidingx/y fields update on entity colliding, new map c1, touch trigger events, gamepad support, game menu init
* 1.8. - global game states, menu state, using global game object renamed ZeldaAdventure666, states with draw method "render states"
* 5.8. - slider input, sfx/music volume sliders, select input, resolution setting, wait event, game states cutscene, start over and game over, animation class and game over animations, dialog questions, static img class, global event manager replaced with event system, spotlight shader, interfaces init, renamed StaticData to Static and Animation(Manager) to SAnimation(Manager)
* 5.8. - moogle, c1 start event init, animation events, jump and walk animations
* 10.8. - event manager not waiting and waiting for id, enemies bubble bari biri, moogle start event done, dialog questions and ask event, new link sprites, game over animations finished, state args for state enter

roadmap:
* animation events (falling to hole)
* fuck shit up -> every death from falling to hole should break something in the game Static

small stuff todo:
* sys timing methods
* use arrays for tilemap tiles? compare performance with timing methods
* minigame, couple enemies
* question event with answer option switch
* better text highlight shader
* abstract class UIInput from Button
* event manager not waiting and waiting for id

could do but prob won't:
* async loading, maybe enough for scene load methods (task lists or IEnumerator "yield" methods?)
* parametrizable sprite animation speed, e.g. to bind with walk speed
* forwards/backwards looping sprite animation 
* Character's direction prop should be named Facing?
* action fields/"callbacks" could be named uniformly (OnThing or WhenThing)
* could use milliseconds instead of seconds for updates because ints take less space than floats
* circle/polygon collision shapes (???)
* ~~there's shitload of managers + manageables (animations, dialog, ui, events), could maybe have Manager + Manageable interfaces to enforce uniformity~~
e.g.
    IManager<TKey, IManageable>  ->
        Dictionary<TKey, IManageable> Lookup/Children/Collection/Stages?
        float ElapsedStageTime
        void Update
        void GoTo(TKey)
        void Draw?
        void SetToRemove
    IManageable ->
        IManager Manager
        bool IsDone
        bool CanReEnter
        void Update
        bool Paused?
        void Draw?
        bool DisableDrawing?
* ok nah all mmanagers work too differently to force anything except bare base interfaces, like IManageablw with `bool IsDone { get; }` property