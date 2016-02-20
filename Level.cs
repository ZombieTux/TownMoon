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
    public class Level
    {
        public Point Position;
        public int block_size;
        public int size_x;
        public int size_y;
        public Player player;
        Color Background;
        Layer Ground;
        Layer Objects;
        Layer Foreground;

        public Level()
        {

        }

        public void generate(Game g)
        {
            this.size_x = 50;
            this.size_y = 50;
            this.block_size = 64;
            this.Ground = new Layer(this.size_x, this.size_y);

            Random rnd = new Random();
            for (int i = 0; i <= size_x - 1; i++)
            {
                for (int j = 0; j <= size_y - 1; j++)
                {
                    int a = rnd.Next(0,2);
                    Ground.blocks[i, j] = new StaticBlock(g.textures[a], "grass", new Vector2(block_size, block_size), new Point(i * block_size, j * block_size), Color.White);
                }
            }
            this.Objects = new Layer(this.size_x, this.size_y);

            rnd = new Random();
            for (int i = 0; i <= size_x - 1; i++)
            {
                for (int j = 0; j <= size_y - 1; j++)
                {
                    int a = rnd.Next(0, 10);
                    if (a == 1)
                    {
                        Objects.blocks[i, j] = new StaticBlock(g.textures[2], "grass", new Vector2(block_size, block_size), new Point(i * block_size, j * block_size), Color.White); ;
                    }
                    else
                    {
                        Objects.blocks[i, j] = null;
                    }
                }
            }
            player = new Player(g.Content.Load<Texture2D>("server-icon"),"GRNK",new Vector2(block_size,block_size),new Point(block_size,block_size),Color.White, true);

        }

        public void load()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D hbtxt)
        {
            for(int i=0; i<= size_x-1; i++){
                for(int j=0; j<= size_y-1; j++){
                    StaticBlock blk = Ground.blocks[i,j];
                    spriteBatch.Draw(blk.texture, new Rectangle((i * block_size) - Convert.ToInt32(this.Position.X), (j * block_size) - Convert.ToInt32(this.Position.Y), block_size, block_size), blk.tint);
                }
            }
            for (int i = 0; i <= size_x - 1; i++)
            {
                for (int j = 0; j <= size_y - 1; j++)
                {
                    StaticBlock blk = Objects.blocks[i, j];
                    if (blk != null)
                    {
                        spriteBatch.Draw(blk.texture, new Rectangle((i * block_size) - Convert.ToInt32(this.Position.X), (j * block_size) - Convert.ToInt32(this.Position.Y), block_size, block_size), blk.tint);
                        spriteBatch.Draw(hbtxt, blk.collisionBox, Color.Blue);
                    }
                }
            }
            spriteBatch.Draw(player.texture, new Rectangle(Convert.ToInt32(player.position.X - this.Position.X), Convert.ToInt32(player.position.Y - this.Position.Y), Convert.ToInt32(player.size.X), Convert.ToInt32(player.size.Y)), player.tint);
            spriteBatch.Draw(hbtxt, player.collisionBox, Color.Red);
        }

        public void Update(GameTime gt, Game g)
        {
            float playsX = player.position.X + player.size.X;
            float playsY = player.position.Y + player.size.Y;
            float scrennXright = this.Position.X + (g.graphics.PreferredBackBufferWidth * 0.66f);
            float scrennXleft = (g.graphics.PreferredBackBufferWidth * 0.33f) + this.Position.X;
            float scrennYdown = this.Position.Y + (g.graphics.PreferredBackBufferHeight * 0.66f);
            float scrennYup = this.Position.Y + (g.graphics.PreferredBackBufferHeight * 0.33f);

            if (playsX > scrennXright)
            {
                this.Position.X = Convert.ToInt32(playsX - (g.graphics.PreferredBackBufferWidth * 0.66f));
                //this.Position = this.player.Position;
            }
            else if (playsX < scrennXleft)
            {
                this.Position.X = Convert.ToInt32(playsX - (g.graphics.PreferredBackBufferWidth * 0.33f));
            }
            if (playsY > scrennYdown)
            {
                this.Position.Y = Convert.ToInt32(playsY - (g.graphics.PreferredBackBufferHeight * 0.66f));
            }
            else if (playsY < scrennYup)
            {
                this.Position.Y = Convert.ToInt32(playsY - (g.graphics.PreferredBackBufferHeight * 0.33f));
            }
            if (this.Position.X < 0)
            {
                this.Position.X = 0;
            }
            else if (this.Position.X + g.graphics.PreferredBackBufferWidth > this.size_x * this.block_size)
            {
                this.Position.X = (this.size_x * this.block_size) - g.graphics.PreferredBackBufferWidth;
            }
            if (this.Position.Y < 0)
            {
                this.Position.Y = 0;
            }
            else if (this.Position.Y + g.graphics.PreferredBackBufferHeight > this.size_y * this.block_size)
            {
                this.Position.Y = (this.size_y * this.block_size) - g.graphics.PreferredBackBufferHeight;
            }
        }

        public void moveplayer(KeyboardState state)
        {
            player.move();

            int pX = Convert.ToInt32(Math.Floor(Convert.ToDouble(player.position.X) / Convert.ToDouble(block_size)));
            int pY = Convert.ToInt32(Math.Floor(Convert.ToDouble(player.position.Y) / Convert.ToDouble(block_size)));

            List<StaticBlock> right = new List<StaticBlock>();
            List<StaticBlock> left = new List<StaticBlock>();
            List<StaticBlock> up = new List<StaticBlock>();
            List<StaticBlock> down = new List<StaticBlock>();

            for (int i = -1; i <= 1; i++)
            {
                if (pX + 1 > 0 && pY + i > 0 && pX + 1 < size_x && pY + i < size_y)
                {
                    if (Objects.blocks[pX + 1, pY + i] != null)
                    {
                        right.Add(Objects.blocks[pX + 1, pY + i]);
                    }
                }

                if (pX > 0 && pY + i > 0 && pX < size_x && pY + i < size_y)
                {
                    if (Objects.blocks[pX, pY + i] != null)
                    {
                        left.Add(Objects.blocks[pX, pY + i]);
                    }
                }

                if (pX + i > 0 && pY > 0 && pX + i < size_x && pY < size_y)
                {
                    if (Objects.blocks[pX + i, pY] != null)
                    {
                        up.Add(Objects.blocks[pX + i, pY]);
                    }
                }

                if (pX + i > 0 && pY + 1 > 0 && pX + i < size_x && pY + 1 < size_y)
                {
                    if (Objects.blocks[pX + i, pY + 1] != null)
                    {
                        down.Add(Objects.blocks[pX + i, pY + 1]);
                    }
                }
            }


            int speed = 3;

            if (state.IsKeyDown(Keys.LeftShift))
            {
                speed += 3;
            }

            if (state.IsKeyDown(Keys.D))
            {
                player.Velocity.X = speed;
            }
            if (state.IsKeyDown(Keys.A))
            {
                player.Velocity.X = -speed;
            }
            if (state.IsKeyDown(Keys.W))
            {
                player.Velocity.Y = -speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                player.Velocity.Y = speed;
            }

            if (player.Velocity.X < 0 && player.collisionDetection(left))
            {
                player.position.X += speed;
                player.Velocity.X = 0;
            }
            else if (player.Velocity.X > 0 && player.collisionDetection(right))
            {
                player.position.X -= speed;
                player.Velocity.X = 0;
            }
            if (player.Velocity.Y > 0 && player.collisionDetection(down))
            {
                player.Velocity.Y = 0;
                player.position.Y -= speed;
            }
            else if (player.Velocity.Y < 0 && player.collisionDetection(up))
            {
                player.Velocity.Y = 0;
                player.position.Y += speed;
            }

            Console.WriteLine("[" + pX + ";" + pY + "]");

        }

        }
}
