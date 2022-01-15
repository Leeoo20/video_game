using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework.Media;

using System;
using Microsoft.Xna.Framework.Audio;

namespace jeuxVideo
{
    public enum EnumEcranAfficher { ACCEUIL =0, CHOIX_NIVEAU =1, COMBAT1 =2, COMBAT2 =3, COMMANDE =4, MORT =5, GAGNER =6}
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _ecranAfficher;
        private bool _changerEcran;
        private int _memoireEcran;


        private Song _sonFond;
        private MediaState _etatMusique;

        private SoundEffect _sonBoutonSelection;





        private readonly ScreenManager _gestionnaireEcran; //gestionnaireEcran
        public SpriteBatch SpriteBatch { get; set; }

        public int EcranAfficher
        {
            get
            {
                return this._ecranAfficher;
            }

            set
            {
                this._ecranAfficher = value;
            }
        }

       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _gestionnaireEcran = new ScreenManager();
            Components.Add(_gestionnaireEcran);

           



        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

          
           
            EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;

            _memoireEcran = 1;//Valeur OSEF JUSTE DIFFERENT DE 0


            _etatMusique = MediaState.Playing;

           


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteBatch = _spriteBatch;
            // TODO: use this.Content to load your game content here

            _sonFond = Content.Load<Song>("musiqueFond");

            _sonBoutonSelection = Content.Load<SoundEffect>("boutonSelectionner");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();



            //ACCES AU DIFFERENTE PARTI
            //A = écranCommance //rouge
            //B = écranChoixNiveau//jaune
            //C = ecranCombat1//orange
            //D = écranCombat2//rose
            //E = ecranMort//Marron
            //R = ecranGagner//Saumon
            //M = ecranAcceuil //Blanc



            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _etatMusique = MediaState.Paused;
            }
          

            if (MediaPlayer.State != _etatMusique)
            {
                MediaPlayer.Play(_sonFond);
                MediaPlayer.Volume = (float)0.1;
                MediaPlayer.IsRepeating = true;
              
            }
            

            //DETECTE QUAND DEMANDE CHANGEMENT ECRAN 
            _changerEcran = false;
           
            if (_ecranAfficher != _memoireEcran)
            {
                _changerEcran = true;
                _sonBoutonSelection.Play();
            }
           

            _memoireEcran = _ecranAfficher;

            //PERMET DE NE PAS ETRE BLOQUER DANS LA BOUCLE
            if (_changerEcran == true)
            {
                
                if (_ecranAfficher == (int)EnumEcranAfficher.ACCEUIL)
                {
                    AllerEcranAcceuil();
                }
                if (_ecranAfficher == (int)EnumEcranAfficher.COMMANDE)
                {
                    AllerEcranCommande();
                }
                if (_ecranAfficher == (int)EnumEcranAfficher.CHOIX_NIVEAU)
                {
                    AllerEcranChoixNiveau();
                }

                if (_ecranAfficher == (int)EnumEcranAfficher.COMBAT1)
                {
                    AllerEcranCombat1();
                }
                if (_ecranAfficher == (int)EnumEcranAfficher.COMBAT2)
                {
                    AllerEcranCombat2();
                }
                if (_ecranAfficher == (int)EnumEcranAfficher.GAGNER)
                {
                    AllerEcranGagner();
                }
                if (_ecranAfficher == (int)EnumEcranAfficher.MORT)
                {
                    AllerEcranMort();
                }
            }




         

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            //ECRIRE ICI
        
            _spriteBatch.End();
       
           


            base.Draw(gameTime);
        }


        private void AllerEcranCommande()
        {
            _gestionnaireEcran.LoadScreen(new EcranCommande(this));


        }

        private void AllerEcranChoixNiveau()
        {
            _gestionnaireEcran.LoadScreen(new EcranChoixNiveau(this));
        }

        private void AllerEcranCombat1()
        {
            _gestionnaireEcran.LoadScreen(new EcranCombat1(this));
        }
        private void AllerEcranCombat2()
        {
            _gestionnaireEcran.LoadScreen(new EcranCombat2(this));
        }

        private void AllerEcranMort()
        {
            _gestionnaireEcran.LoadScreen(new EcranMort(this));
        }

        private void AllerEcranGagner()
        {
            _gestionnaireEcran.LoadScreen(new EcranGagner(this));
        }
        private void AllerEcranAcceuil()
        {
            _gestionnaireEcran.LoadScreen(new EcranAcceuil(this));
        }
    }





}
