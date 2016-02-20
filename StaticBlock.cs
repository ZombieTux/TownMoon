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
    public class StaticBlock : Block
    {
        public Texture2D texture;
        public Rectangle collisionBox;

        public StaticBlock(Texture2D texture, string name, Vector2 size, Point position, Color tint, bool collision=false) : base(name, size, position, tint, collision)
        {
            this.texture = texture;
            this.collisionBox = new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), Convert.ToInt32(this.size.X), Convert.ToInt32(this.size.Y));
        
        }
    }
}
