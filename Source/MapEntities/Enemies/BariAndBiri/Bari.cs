﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZA6.Managers;
using ZA6.Models;
using TapsasEngine.Sprites;

namespace ZA6
{
    public class Bari : Enemy
    {
        public Hitbox DamageHitbox1;
        public Vector2 PushVelocity;
        
        public Bari()
        {
            Health = 2;
            Hittable = true;
            Moving = true;
            
            var texture = Img.EnemySprites;
            SAnimation.DefaultFrameWidth = SAnimation.DefaultFrameHeight = 34;

            Dictionary<string, SAnimation> animations = new Dictionary<string, SAnimation>()
            {
                { "Default", new SAnimation(texture, 2, 0.4f, true, 6) },
                { "Attacking", new SAnimation(texture, 2, 0.1f, true, 6, 1) },
                { "TakenDamage", new SAnimation(texture, 0, 6) }
            };

            AnimatedSprite = new AnimatedSprite(animations, "Default");
            Hitbox.Load(10, 10);
            DamageHitbox1 = new Hitbox();
            DamageHitbox1.Load(14, 14);
            SpriteOffset = new Vector2(-12, -16);

            Dictionary<string, State> states = new Dictionary<string, State>()
            {
                { "Default", new BariStateDefault(this) },
                { "Attacking", new BariStateAttacking(this) },
                { "TakenDamage", new BariStateTakenDamage(this) }
            };

            StateMachine = new StateMachine(states, "Default");
        }

        public override void DeterminePlayerDamage()
        {
            var playerRectangle = Static.Scene.Player.Hitbox.Rectangle;
            DamageHitbox1.Position = Position + new Vector2(-2, -2);

            if (DamageHitbox1.Rectangle.Intersects(playerRectangle))
            {
                Static.Scene.Player.TakeDamage(DamageHitbox1.Rectangle.Center);
            }
        }
    }
}
