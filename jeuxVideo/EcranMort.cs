using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class EcranMort : GameScreen
    {

        private Game1 _myGame;


        private Sprite _fondMort;

        private Sprite _GameOver;


        private Sprite _boutonRejouer;

        private Sprite _boutonQuitter;



        public EcranMort(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //FOND
            _fondMort = new Sprite();
            _fondMort.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2) - 300), new Vector2(0, 0), 0);

            //LOGO GAME OVER
            _GameOver = new Sprite();
            _GameOver.Initialize(new Vector2((GraphicsDevice.Viewport.Width / 2 - 100), (Game.GraphicsDevice.Viewport.Height / 2 - 250)), new Vector2(0, 0), 0);


            //BOUTON REJOUER ET QUITTER
            _boutonRejouer = new Sprite();
            _boutonRejouer.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 400), new Vector2(0, 0), 0);

            _boutonQuitter = new Sprite();
            _boutonQuitter.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 480), new Vector2(0, 0), 0);





            base.Initialize();
        }


        public override void LoadContent()
        {
            //FOND
            _fondMort.Texture = Content.Load<Texture2D>("EcranMort/FontMort2");

            //LOGO GAME OVER
            _GameOver.Texture = Content.Load<Texture2D>("EcranMort/GameOver");

            //BOUTON REJOUER ET QUITTER
            _boutonRejouer.Texture = Content.Load<Texture2D>("EcranMort/boutonRejouer");
            _boutonQuitter.Texture = Content.Load<Texture2D>("EcranMort/boutonQuitter");







            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();


            //TOUCHE DEPLACEMENT 
            //COMMANDE = N
            //ChoixNiveau = M
            if (mouseState.LeftButton == ButtonState.Pressed && _boutonRejouer.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.CHOIX_NIVEAU;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && _boutonQuitter.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
            }

        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Maroon);
            _myGame.SpriteBatch.Begin();
            //ECRIRE ICI

            //FOND 
            _fondMort.Draw(_myGame.SpriteBatch);

            //LOGO GAME OVER
            _GameOver.Draw(_myGame.SpriteBatch);

            //BOUTON REJOUER ET QUITTER
            _boutonRejouer.Draw(_myGame.SpriteBatch);
            _boutonQuitter.Draw(_myGame.SpriteBatch);

            _myGame.SpriteBatch.End();
        }
    }
}

