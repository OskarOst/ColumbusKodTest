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
    class GameController
    {
        List<Card> cardsInPlay;
        List<Card> cardsInDeck; 
        Random random;
        KeyboardState keyboard;
        KeyboardState oldkeyboard;
        MergeSort mergeSort;
        SpriteFont font;
        public GameController(Texture2D sprite, SpriteFont font)
        {
            random = new Random();
            cardsInPlay = new List<Card>();
            mergeSort = new MergeSort();
            this.font = font;
            //Skapar 13*4 kort i vår deck.
            cardsInDeck = new List<Card>();
            for (int kortfärg = 0; kortfärg <= 3; kortfärg++)
            {
                for (int kortvärde = 1; kortvärde <= 13; kortvärde++)
                {
                    cardsInDeck.Add(new Card(sprite, kortvärde, kortfärg));
                }
            }
            ReposCards();
        }
        public void Update()
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space) && oldkeyboard.IsKeyUp(Keys.Space))
            {
                //Trycker du space så läggs korten i spel tills i huvud decken. Den ändrar också kortens positioner. 
                cardsInDeck.AddRange(cardsInPlay);
                cardsInPlay.Clear();
                cardsInDeck = Shuffel(cardsInDeck);
                ReposCards();
            }
            else if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                //Trycker du ner Enter så töms läggs korten ute på bordet in i vårt huvud deck och de ute på korten töms. Cards in deck 
                //överskrivs också av en funktion i mergesort där man returnerar samma lista som har blivit sorterade med mergesort. 
                //Den ändrar också kortens position
                cardsInDeck.AddRange(cardsInPlay);
                cardsInPlay.Clear();
                cardsInDeck = mergeSort.Sort(cardsInDeck);
                ReposCards();
            }
            else if (keyboard.IsKeyDown(Keys.Up) && oldkeyboard.IsKeyUp(Keys.Up) && cardsInDeck.Count > 0)
            {
                //Tar bort kort 0 från decken vilket ritas ut som det översta, detta kortet läggs till på 
                //skärmen som om det har spelsidan upp. Annars ritas baksidan ut.  
                cardsInPlay.Add(cardsInDeck[0]);
                cardsInDeck.RemoveAt(0);
                ReposCards();
            }
            oldkeyboard = keyboard;
        }
        private List<Card> Shuffel(List<Card> input)
        {
            //blandar korten, i en while loop tar den bort ett kort i taget på random ifrån input och lägger till den
            //i output. När input är tömd returnerar funktionen output. 
            List<Card> output = new List<Card>();
            while (input.Count > 0)
            {
                int removeAt = random.Next(0, input.Count - 1);
                output.Add(input[removeAt]);
                input.RemoveAt(removeAt);
            }
            return output;
        }
        private void ReposCards()
        { 
            //Algoritm för att rita ut korten och var. Går först igenom korten vända uppåt och sedan de vända 
            //neråt.
            int x = 1;
            int y = 0;
            for (int i = 0; i < cardsInPlay.Count; i++)
            {
                cardsInPlay[i].Repos(x, y);
                x++;
                if (x > 13)
                {
                    x = 1;
                    y++;
                }
            }
            y = 0;
            x = 0;
            for (int i = cardsInDeck.Count - 1; i >= 0; i--)
            {
                cardsInDeck[i].ReposBack(x, y);
                y++;
                x++;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Ritar ut korten. 
            for (int i = 0; i < cardsInPlay.Count; i++)
            {
                cardsInPlay[i].Draw(spriteBatch);
            }
            for (int i = cardsInDeck.Count - 1; i >= 0; i--)
            {
                cardsInDeck[i].DrawBack(spriteBatch);
            }
            spriteBatch.DrawString(font, "1: Press the up arrow to play one card from the deck." + Environment.NewLine + "2: Press Space to shuffle the cards into the deck. You can then play them again with the up key." + Environment.NewLine + "3: press Enter to sort them by numbers. You can then press up key to play them again.", new Vector2(200,500), Color.Black);
        }
    }
}
