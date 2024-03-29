using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZA6.Models
{
    public class SAnimation
    {
        public static int DefaultFrameWidth = 40;
        public static int DefaultFrameHeight = 50;
        public int CurrentFrame;
        public int FrameCount;
        public float FrameDuration;

        public bool IsLooping;

        public Texture2D Texture { get; private set; }

        public Rectangle[] FrameRectangles;
        public Vector2 Offset = Vector2.Zero;

        public SAnimation(Texture2D texture, int frameCount, float frameDuration, bool isLooping = true, int textureYPos = 0, int textureXPos = 0, Vector2 offset = new Vector2())
        {
            Texture = texture;
            IsLooping = isLooping;
            FrameDuration = frameDuration;
            FrameCount = frameCount;
            Offset = offset;
            FrameRectangles = CreateHorizontalFrameRectangles(frameCount, DefaultFrameWidth, DefaultFrameHeight, textureYPos, textureXPos);
        }

        public SAnimation(Texture2D texture, int frameCount, int frameWidth, int frameHeight, float frameDuration = 0.1f, int textureYPos = 0, int textureXPos = 0, Vector2 offset = new Vector2(), bool isLooping = false)
        {
            Texture = texture;
            IsLooping = isLooping;
            FrameDuration = frameDuration;
            FrameCount = frameCount;
            Offset = offset;
            FrameRectangles = CreateHorizontalFrameRectangles(frameCount, frameWidth, frameHeight, textureYPos, textureXPos);
        }

        public SAnimation(Texture2D texture, float frameDuration, Vector2 offset, Rectangle[] frameRectangles, bool isLooping = false)
        {
            Texture = texture;
            FrameCount = frameRectangles.Length;
            IsLooping = isLooping;
            FrameDuration = frameDuration;
            FrameRectangles = frameRectangles;
            Offset = offset;
        }

        public SAnimation(Texture2D texture, Rectangle frameRectangle)
        {
            Texture = texture;
            FrameCount = 1;
            IsLooping = true;
            FrameDuration = float.PositiveInfinity;
            FrameRectangles = new Rectangle[] { frameRectangle };
        }

        public SAnimation(Texture2D texture, int spriteX, int spriteY, int width = 0, int height = 0, Vector2 offset = new Vector2())
        {
            if (width == 0)
                width = DefaultFrameWidth;
            if (height == 0)
                height = DefaultFrameHeight;
            
            Texture = texture;
            FrameCount = 1;
            IsLooping = true;
            FrameDuration = float.PositiveInfinity;
            Offset = offset;
            FrameRectangles = new Rectangle[]
            {
                new Rectangle(width * spriteX, height * spriteY, width, height)
            };
        }

        private static Rectangle[] CreateHorizontalFrameRectangles(int frameCount, int frameWidth, int frameHeight, int textureYPos, int textureXPos)
        {
            var r = new Rectangle[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                r[i] = new Rectangle(
                    i * frameWidth + textureXPos * frameWidth,
                    textureYPos * frameHeight,
                    frameWidth,
                    frameHeight
                );
            }

            return r;
        }
    }
}