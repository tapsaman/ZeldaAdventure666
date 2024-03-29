using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZA6.Models;
using TapsasEngine.Sprites;
using TapsasEngine.Enums;
using TapsasEngine;

namespace ZA6
{
    public class Klaus : Character
    {       
        public Klaus()
        {
            var texture = Img.NPCSprites;

            Dictionary<string, SAnimation> animations = new Dictionary<string, SAnimation>()
            {
                { "IdleLeft",       new SAnimation(texture, 4, 1, 20, 30) },
                { "IdleDown",       new SAnimation(texture, 5, 1, 20, 30) },
                { "IdleRight",       new SAnimation(texture, 6, 1, 20, 30) }
            };

            AnimatedSprite = new AnimatedSprite(animations, "IdleDown");
            Hitbox.Load(16, 16);
            SpriteOffset = new Vector2(-3, -12);
            Interactable = true;
            Hittable = false;
            Facing = Direction.Down;
            Trigger += TalkTo;
            Moving = true;
        }

        private void TalkTo(Character _)
        {
            Static.EventSystem.Load(new Event[]
            {
                new FaceEvent(this, Static.Player),
                new TextEvent(new Dialog("This is the pain room."), this),
                new AnimateEvent(new Animations.FadeSprite(Sprite)),
                new RemoveEvent(this),
                new SaveValueEvent(DataStoreType.Session, "spoken to klaus", true),
            });
        }
    }
}
