using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class EcranChoixNiveau : GameScreen
    {
        private Game1 _myGame;


        private Sprite _fondChoixNiveau;

        private Sprite _flecheRetour;

        private Sprite _boutonNiveau1;
        private Sprite _ecranCombat1;

        private Sprite _boutonNiveau2;
        private Sprite _ecranCombat2;




        public EcranChoixNiveau(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //FOND
            _fondChoixNiveau = new Sprite();
            _fondChoixNiveau.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2) - 300), new Vector2(0, 0), 0);

            //FLECH RETOUR
            _flecheRetour = new Sprite();
            _flecheRetour.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2 - 300)), new Vector2(0, 0), 0);

            //BOUTON ET IMAGE NIVEAU 1
            _boutonNiveau1 = new Sprite();
            _boutonNiveau1.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, (Game.GraphicsDevice.Viewport.Height / 2 + 50)), new Vector2(0, 0), 0);

            _ecranCombat1 = new Sprite();
            _ecranCombat1.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, (Game.GraphicsDevice.Viewport.Height / 2 - 120)), new Vector2(0, 0), 0);


            //BOUTON ET IMAGE NIVEAU 2
            _boutonNiveau2 = new Sprite();
            _boutonNiveau2.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 + 80, (Game.GraphicsDevice.Viewport.Height / 2 + 50)), new Vector2(0, 0), 0);

            _ecranCombat2 = new Sprite();
            _ecranCombat2.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 + 75, (Game.GraphicsDevice.Viewport.Height / 2 - 120)), new Vector2(0, 0), 0);



            base.Initialize();
        }


        public override void LoadContent()
        {

            //FOND
            _fondChoixNiveau.Texture = Content.Load<Texture2D>("pageChoixNiveau/FondChoixNiveau");

            //FLECHE RETOUR
            _flecheRetour.Texture = Content.Load<Texture2D>("pageChoixNiveau/FlecheRetour");

            //BOUTON ET IMAGE NIVEAU 1
            _boutonNiveau1.Texture = Content.Load<Texture2D>("pageChoixNiveau/boutonNiveau1");
            _ecranCombat1.Texture = Content.Load<Texture2D>("pageChoixNiveau/EcranCombat1");

            //BOUTON ET IMAGE NIVEAU 2
            _boutonNiveau2.Texture = Content.Load<Texture2D>("pageChoixNiveau/boutonNiveau2");
            _ecranCombat2.Texture = Content.Load<Texture2D>("pageChoixNiveau/EcranCombat2");






            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();


   
            if (mouseState.LeftButton == ButtonState.Pressed && _boutonNiveau1.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.COMBAT1;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && _boutonNiveau2.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.COMBAT2;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && _flecheRetour.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
            }


        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow);
            _myGame.SpriteBatch.Begin();

            //FOND
            _fondChoixNiveau.Draw(_myGame.SpriteBatch);

            //FLECHE RETOUR
            _flecheRetour.Draw(_myGame.SpriteBatch);

            //BOUTON ET IMAGE NIVEAU 1
            _boutonNiveau1.Draw(_myGame.SpriteBatch);
            _ecranCombat1.Draw(_myGame.SpriteBatch);

            //BOUTON ET IMAGE NIVEAU 2
            _boutonNiveau2.Draw(_myGame.SpriteBatch);
            _ecranCombat2.Draw(_myGame.SpriteBatch);


            _myGame.SpriteBatch.End();
        }
    }
}
