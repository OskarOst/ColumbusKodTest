using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ColumbusKodTest
{
    class Card
    {
        private Texture2D sprite;
        private int cardValue;
        private int typeOfCard;
        private int rows = 4;
        private int columns = 14;
        private Rectangle mainRec;
        private int currentFrame; 
        public Card(Texture2D sprite, int cardValue, int typeOfCard)
        {
            this.sprite = sprite;
            //Vilken sorts kort det är. 0 = spader 1 = hjärter. 2 = Klöver. 3 = ruter. De kommer också ha dessa värdena i sorteringen. Spader är då högst och ruter lägst.
            this.typeOfCard = typeOfCard;
            //Värdet det har på kortet. ess är minst och värt 1, kung högst på 13. Lägger till 1 för att ge dem rätt värde då datorn börjar att räkna på 0. 
            this.cardValue = cardValue;
            currentFrame = cardValue + (typeOfCard * columns);
            int yPos= 0 + ((sprite.Height / rows) * typeOfCard);
            int xPoss = 0 + ((sprite.Width / columns) * cardValue);
            mainRec = new Rectangle(xPoss, yPos, sprite.Width / columns, sprite.Height/rows);
        }
        public void Repos(int x, int y)
        {
            //Funktion som kallas från Repos funktionen i GameController. Detta är om korten är vända uppåt.
            int yPos = 0 + ((sprite.Height / rows) * y);
            int xPoss = 0 + ((sprite.Width / columns) * x);
            mainRec = new Rectangle(xPoss, yPos, sprite.Width / columns, sprite.Height / rows);
        }
        public void ReposBack(int x, int y)
        {
            //Funktion som kallas från Repos funktionen i GameController. Detta är om korten är vända neråt.
            int yPos = 400 + 5 * y;
            int xPoss = 10 + 2*x;
            mainRec = new Rectangle(xPoss, yPos, sprite.Width / columns, sprite.Height / rows);
        }
        public int Type()
        {
            //Returnerar siffran som är bunden till om kortet är hjärter, klöver, ruter, eller spader.. 
            return typeOfCard;
        }
        public int CardValue()
        {
            //Returnerar kortets nummer värde; ess (1) - kung (13)
            return cardValue; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Ritar ut kortets framsida.
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;
            Rectangle sourceRec = new Rectangle(mainRec.Width * column, mainRec.Height * row, mainRec.Width, mainRec.Height);
            spriteBatch.Draw(sprite, mainRec, sourceRec, Color.White);
        }
        public void DrawBack(SpriteBatch spriteBatch)
        {
            //Ritar ut kortets baksida.
            int row = (int)((float)0 / (float)columns);
            int column = 0 % columns;
            Rectangle sourceRec = new Rectangle(mainRec.Width * column, mainRec.Height * row, mainRec.Width, mainRec.Height);
            spriteBatch.Draw(sprite, mainRec, sourceRec, Color.White);
        }
    }
}
