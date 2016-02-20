using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TownMoon
{
    public class Player : Block
    {
        public Texture2D texture;
        public Rectangle collisionBox;
        public Vector2 Velocity;


        public Player(Texture2D texture, string name, Vector2 size, Point position, Color tint, bool collision = true)
            : base(name, size, position, tint, collision)
        {
            this.texture = texture;
            this.collisionBox = new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), Convert.ToInt32(this.size.X), Convert.ToInt32(this.size.Y));
        }

        public bool collisionDetection(List<StaticBlock> blocks)
        {
            this.collisionBox = new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), Convert.ToInt32(this.size.X), Convert.ToInt32(this.size.Y));
            foreach (StaticBlock sb in blocks)
            {
                if(sb.collisionBox.Intersects(this.collisionBox)){
                    return true;
                }
            }
            return false;
        }

        public void move()
        {
            this.position.X += (int)this.Velocity.X;
            this.position.Y += (int)this.Velocity.Y;
            this.Velocity.X = 0;
            this.Velocity.Y = 0;
        }
    }
}
