﻿using Microsoft.Xna.Framework;

namespace ZA6
{
    public class TouchEventTrigger : MapEntity
    {
        public TouchEventTrigger(Vector2 position, int width, int height)
        {
            Position = position;
            Hitbox.Load(width, height);
            Hitbox.Color = Color.Pink;
            Interactable = false;
            TriggeredOnTouch = true;
        }
    }
}
