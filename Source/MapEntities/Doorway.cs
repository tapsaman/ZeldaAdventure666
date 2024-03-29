using Microsoft.Xna.Framework;
using ZA6.Managers;
using ZA6.Models;
using TapsasEngine.Enums;

namespace ZA6
{
    public class Doorway : TouchEventTrigger
    {
        public string ToMap;

        public Doorway(Vector2 position, string toMap)
            : base(position, 16, 10)
        {
            ToMap = toMap;
            Trigger += GoTo; 
        }

        private void GoTo(Character character)
        {
            if (character is Player == false)
                return;

            Static.Player.NoClip = true;
            Static.Player.FaceTowards(Position);
            var distance = Static.Player.Facing.ToVector() * 14 *
                (Static.Player.Facing == Direction.Down ? 2.5f : 1f);
            
            Static.EventSystem.Load(new Event[]
                {
                    new AnimateEvent(
                        new Animations.Walk.Timed(Static.Player, distance, 1f)
                    ) { Wait = false },
                    new TeleportEvent(ToMap),
                    new RunEvent(() => Static.Player.NoClip = false)
                },
                EventSystem.Settings.SustainSceneChange
            );
        }
    }
}
