using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class EcranCombat1 : GameScreen
    {
        private Game1 _myGame;

      
        //GESTION PAUSE 

        private int _etatPause;
        private Sprite _boutonQuitter;
        private Sprite _boutonContinuer;

        //MAP
        private TiledMap _map;
        private TiledMapRenderer _mapRendu;

        //SOL
        private TiledMapTileLayer _sol;

        //PHYSIQUE 
        private Vector2 _physique;
        private int _vitessePhysique;

        //SAUT
        public enum SAUT { PEUT_SAUTER = 0, A_SAUTER = 1, SAUTE = 2 }


        //BOSS
        private SpriteAnime _bossCyberRobot;
        private string _sensBoss;
        private int _etatBoss;
        private int _prochaineAttaque;
        private int _etatBossAttaque1;
        private int _etatBossAttaque2;
        private bool _bossAction;
        public enum ETAT_BOOS { ATTENTE = 0, ATTAQUE1 = 1, ATTAQUE2 = 2 }
        public enum ATTAQUE1 { INACTIF = 0, DEBUT_ATTAQUE = 1, ATTAQUE = 2, FIN = 3 }
        public enum ATTAQUE2 { INACTIF = 0, DEBUT_ATTAQUE = 1, REJOINS_POSITION = 2, ATTAQUE = 3, REVIENS = 4, FIN = 5 }
        private Sprite _nomBoss;

        //BARRE DE VIE BOSS
        private SpriteAnime _vieBoss;


        // PERSO
        private SpriteAnime _perso;
        private string _sensPerso;
        private int _etatSautPerso;
        private float _positionYDebutSaut;
        //SON PERSO
        private SoundEffect _sonTirFleche;
        private SoundEffect _sonSaut;

        //Barre DE VIE JOUEUR
        private SpriteAnime _vie;

        // FLECHE
        private Sprite[] _fleches = new Sprite[5];
        private bool[] _etatTirs = new bool[5];
        private bool _etatIntermediare;
        private bool _etatSouris;
        private int _flecheATirer;

        //LASER
        private SpriteAnime _laser;

        public EcranCombat1(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {


            //PAUSE 
            _etatPause = (int)ETAT_PAUSE.CONTINUE;
            _boutonQuitter = new Sprite();
            _boutonQuitter.Initialize(new Vector2(1000, 1000), new Vector2(0, 0), 0);
            _boutonContinuer = new Sprite();
            _boutonContinuer.Initialize(new Vector2(1000, 1000), new Vector2(0, 0), 0);

            //PHYSIQUE
            _physique = new Vector2(0, 1);
            _vitessePhysique = 2;


            //BOSS
            _bossCyberRobot = new SpriteAnime();
            _bossCyberRobot.Initialize(new Vector2(700, 470), 100, 20, "immobileGauche", new Vector2(0, 0), true);
            _sensBoss = "Gauche";
            _etatBoss = (int)ETAT_BOOS.ATTENTE;
            _etatBossAttaque1 = (int)ATTAQUE1.INACTIF;
            _etatBossAttaque2 = (int)ATTAQUE2.INACTIF;
            _bossAction = false;
            _prochaineAttaque = 0;
            _nomBoss = new Sprite();
            _nomBoss.Initialize(new Vector2(Game.GraphicsDevice.Viewport.Width - 60, 11), new Vector2(0, 0), 0);


            //BARRE DE VIE BOSS
            _vieBoss = new SpriteAnime();
            _vieBoss.Initialize(new Vector2(Game.GraphicsDevice.Viewport.Width - 200, 45), 0, 0, "vieBoss20", new Vector2(0, 0), false);


            //PERSO
            _perso = new SpriteAnime();
            _perso.Initialize(new Vector2(50, 500), 200, 12, "immobileDroite", new Vector2(0, 0), true);
            _sensPerso = "Droite";
            _etatSautPerso = (int)SAUT.PEUT_SAUTER;
            _positionYDebutSaut = 0;

            //BARRE DE VIE
            _vie = new SpriteAnime();
            _vie.Initialize(new Vector2(51, 17), 0, 0, "vie12", new Vector2(0, 0), false);


            // FLECHE
            for (int i = 0; i < _fleches.Length; i++)
            {
                _fleches[i] = new Sprite();
                _fleches[i].Initialize(new Vector2(1000, 1000), new Vector2(0, 0), 600);
            }
            _flecheATirer = 0;
            _etatSouris = false;
            _etatIntermediare = false;

            //LASER
            _laser = new SpriteAnime();
            _laser.Initialize(new Vector2(1000, -1000), 0, 0, "laserGauche", new Vector2(0, 0), false);

            base.Initialize();
        }


        public override void LoadContent()
        {
            //BOUTON QUITTER 
            _boutonQuitter.Texture = Content.Load<Texture2D>("Interface/boutonQuitter");
            _boutonContinuer.Texture = Content.Load<Texture2D>("Interface/boutonContinuer");

            //MAP
            _map = Content.Load<TiledMap>("EcranCombat1/map");
            _mapRendu = new TiledMapRenderer(GraphicsDevice, _map);

            //SOL
            _sol = _map.GetLayer<TiledMapTileLayer>("Sol");


            //BOSS
            _bossCyberRobot.TexturePerso = Content.Load<SpriteSheet>("EcranCombat1/BossCyberRobot.sf", new JsonContentLoader());
            _bossCyberRobot.Perso = new AnimatedSprite(_bossCyberRobot.TexturePerso);
            _nomBoss.Texture = Content.Load<Texture2D>("EcranCombat1/bossName");

            //BARRE DE VIE BOSS
            _vieBoss.TexturePerso = Content.Load<SpriteSheet>("EcranCombat1/BarreVieBoss.sf", new JsonContentLoader());
            _vieBoss.Perso = new AnimatedSprite(_vieBoss.TexturePerso);


            //PERSO
            _perso.TexturePerso = Content.Load<SpriteSheet>("Player/perso.sf", new JsonContentLoader());
            _perso.Perso = new AnimatedSprite(_perso.TexturePerso);
            //SON PERSO
            _sonTirFleche = Content.Load<SoundEffect>("joueur/tirArc");
            _sonSaut = Content.Load<SoundEffect>("joueur/saut");

            //BARRE DE VIE
            _vie.TexturePerso = Content.Load<SpriteSheet>("Player/Vie.sf", new JsonContentLoader());
            _vie.Perso = new AnimatedSprite(_vie.TexturePerso);



            //FLECHE
            foreach (Sprite fleche in _fleches)
            {
                fleche.Texture = Content.Load<Texture2D>("Player/projectileDroite");
            }

            //LASER
            _laser.TexturePerso = Content.Load<SpriteSheet>("EcranCombat1/laser.sf", new JsonContentLoader());
            _laser.Perso = new AnimatedSprite(_laser.TexturePerso);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //VARIABLE GLOBALE                          
            ushort txPerso = (ushort)(_perso.PositionPerso.X / _map.TileWidth);// Converti la posittion en pixel en case 
            ushort tyPerso = (ushort)(_perso.PositionPerso.Y / _map.TileHeight);

            ushort tyDessousPerso = (ushort)(tyPerso + 1);
            ushort txGauchePerso = (ushort)(txPerso - 1);
            ushort txDroitPerso = (ushort)(txPerso + 1);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState souris = Mouse.GetState();


            if (_etatPause == (int)ETAT_PAUSE.CONTINUE)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    _etatPause = (int)ETAT_PAUSE.PAUSE_DEMANDER;
                    
                }


                //SENS JOUEUR & BOSS
                if (_perso.PositionPerso.X > _bossCyberRobot.PositionPerso.X)
                {
                    _sensPerso = "Gauche";
                    _sensBoss = "Droite";
                }
                else
                {
                    _sensPerso = "Droite";
                    _sensBoss = "Gauche";
                }
                _bossCyberRobot.Animation = $"immobile{_sensBoss}";
                _perso.Animation = $"immobile{_sensPerso}";



                //SAUT
                if (keyboardState.IsKeyDown(Keys.Space) && IsCollision(txPerso, tyDessousPerso))
                {
                    _positionYDebutSaut = _perso.PositionPerso.Y;
                    _sonSaut.Play();
                    _etatSautPerso = (int)SAUT.SAUTE;

                }

                if (_etatSautPerso == (int)SAUT.SAUTE)
                {
                    _perso.DirectionPerso = new Vector2(0, -1) * 2;
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_etatSautPerso == (int)SAUT.SAUTE && _perso.PositionPerso.Y < _positionYDebutSaut - 160)
                {
                    _perso.DirectionPerso = new Vector2(0, 0);
                    _etatSautPerso = (int)SAUT.PEUT_SAUTER;
                }
                //FIN SAUT
                //DEPLACEMENT JOUEUR
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    _perso.Animation = "allerAGauche";
                    _perso.DirectionPerso = new Vector2(-1, 0);
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    _perso.Animation = "allerADroite";
                    _perso.DirectionPerso = new Vector2(1, 0);
                    _perso.ActualiseDeplacement(gameTime);
                }

                //A SUPPRIMER
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    _perso.DirectionPerso = new Vector2(0, -1);
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    _perso.DirectionPerso = new Vector2(0, 1);
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (keyboardState.IsKeyDown(Keys.H))
                {
                    _perso.PositionPerso = new Vector2(400, 430);//475
                    _perso.ActualiseDeplacement(gameTime);
                }

                //GESTION DE LA PHYSIQUE 
                if (!IsCollision(txPerso, tyDessousPerso))
                {
                    _perso.PositionPerso += _physique * _vitessePhysique;
                    _perso.ActualiseDeplacement(gameTime);
                }

                if (IsCollision(txGauchePerso, tyPerso))
                {
                    _perso.VitessePerso = -_perso.VitessePerso;
                    _perso.ActualiseDeplacement(gameTime);

                }
                if (IsCollision(txDroitPerso, tyPerso))
                {
                    _perso.VitessePerso = -_perso.VitessePerso;
                    _perso.ActualiseDeplacement(gameTime);
                }

                if (_perso.PositionPerso.X > GraphicsDevice.Viewport.Width - _perso.Perso.TextureRegion.Width / 4)
                {
                    _perso.PositionPerso = new Vector2(GraphicsDevice.Viewport.Width - _perso.Perso.TextureRegion.Width / 4, _perso.PositionPerso.Y);
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.X < _perso.Perso.TextureRegion.Width / 4)
                {
                    _perso.PositionPerso = new Vector2(_perso.Perso.TextureRegion.Width / 4, _perso.PositionPerso.Y);
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.Y < 0)
                {
                    _perso.VitessePerso = -_perso.VitessePerso;
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.Y > GraphicsDevice.Viewport.Height)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.MORT;
                }
                //FIN GESTION PHYSIQUE 


                //GESTION DES ECRANS 
                if (_bossCyberRobot.BarreDeVie <= 0)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.GAGNER;
                }
                if (_perso.BarreDeVie <= 0)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.MORT;
                }

                // TIR FLECHE (JOUEUR)
                if (souris.LeftButton == ButtonState.Pressed && _etatIntermediare == false)
                {
                    _flecheATirer++;
                    _etatSouris = true;
                }

                if (_flecheATirer > 4)
                {
                    _flecheATirer = 0;
                }

                if (souris.LeftButton == ButtonState.Released && _etatIntermediare == true)
                {
                    _etatIntermediare = false;
                }

                if (_etatSouris == true) //QUAND LE BOUTON EST CLIQUER ET QUE L'ANMATION EST PAS FINI
                {
                    _etatIntermediare = true;
                    _perso.Animation = $"tireA{_sensPerso}";
                    _fleches[_flecheATirer].Temp += deltaTime;

                    if (_fleches[_flecheATirer].Temp > 1.5) //QUAND L'ANIMATION EST FINI 
                    {
                        _etatTirs[_flecheATirer] = true;
                        _etatSouris = false;
                        _fleches[_flecheATirer].Position = new Vector2(_perso.PositionPerso.X, _perso.PositionPerso.Y - _fleches[_flecheATirer].Texture.Height);
                        _sonTirFleche.Play();
                    }
                }

                for (int i = 0; i < _etatTirs.Length; i++)
                {
                    if (_etatTirs[i] == true)
                    {
                        _fleches[i].Temp = 0;
                        if (_sensPerso == "Gauche")
                        {
                            _fleches[i].Direction = new Vector2(-1, 0);
                        }
                        else
                        {
                            _fleches[i].Direction = new Vector2(1, 0);
                        }
                        _fleches[i].Update(gameTime);

                        if (_fleches[i].Intersects(_bossCyberRobot, 0, 0))
                        {
                            _bossCyberRobot.Animation = $"toucher{_sensBoss}";
                            _bossCyberRobot.GetDamage(_vieBoss, gameTime);
                            _fleches[i].Position = new Vector2(1000, 1000);
                        }

                        if (_fleches[i].Position.X > GraphicsDevice.Viewport.Width || _fleches[i].Position.X < 0)
                        {
                            _etatTirs[i] = false;

                        }
                    }
                }

                //ATTAQUES BOSS

                if (_etatBoss == (int)ETAT_BOOS.ATTENTE)
                {
                    _bossCyberRobot.Temp += deltaTime;
                    if (_perso.PositionPerso.X >= 500)
                    {
                        _prochaineAttaque = (int)ETAT_BOOS.ATTAQUE2;
                        _etatBoss = _prochaineAttaque;
                        _bossCyberRobot.Temp = 0;
                    }
                    if (_bossCyberRobot.Temp > 3)
                    {
                        _bossCyberRobot.Temp = 0;
                        _prochaineAttaque++;
                        if (_prochaineAttaque > (int)ETAT_BOOS.ATTAQUE2)
                        {
                            _prochaineAttaque = (int)ETAT_BOOS.ATTAQUE1;
                        }
                        _etatBoss = _prochaineAttaque;
                    }
                }



                // ATTAQUE1: LASER
                if (_etatBoss == (int)ETAT_BOOS.ATTAQUE1 && _bossAction == false)
                {
                    _etatBossAttaque1 = (int)ATTAQUE1.DEBUT_ATTAQUE;
                    _bossAction = true;
                }

                if (_etatBossAttaque1 == (int)ATTAQUE1.DEBUT_ATTAQUE)
                {
                    _bossCyberRobot.Animation = $"tirA{_sensBoss}";
                    _laser.Temp += deltaTime;
                    if (_laser.Temp > 1.5) //QUAND L'ANIMATION EST FINI
                    {
                        _laser.Temp = 0;
                        if (_sensBoss == "Gauche")
                        {
                            _laser.PositionPerso = new Vector2(_bossCyberRobot.PositionPerso.X - 480, _bossCyberRobot.PositionPerso.Y + 25);
                        }
                        if (_sensBoss == "Droite")
                        {
                            _laser.PositionPerso = new Vector2(_bossCyberRobot.PositionPerso.X + 500, _bossCyberRobot.PositionPerso.Y + 25);
                        }
                        _etatBossAttaque1 = (int)ATTAQUE1.ATTAQUE;
                    }
                }

                if (_etatBossAttaque1 == (int)ATTAQUE1.ATTAQUE)
                {
                    _laser.Temp += deltaTime;
                    _laser.Animation = $"laser{_sensBoss}";
                    _laser.Actualise(gameTime);

                    if (_laser.Temp > 0.5) //QUAND L'ANIMATION EST FINI
                    {
                        _laser.PositionPerso = new Vector2(1000, -1000);
                        _laser.Temp = 0;
                        _etatBossAttaque1 = (int)ATTAQUE1.FIN;
                    }
                }

                if (_etatBossAttaque1 == (int)ATTAQUE1.FIN)
                {
                    _bossAction = false;
                    _etatBossAttaque1 = (int)ATTAQUE1.INACTIF;
                    _etatBoss = (int)ETAT_BOOS.ATTENTE;
                }


                //ATTAQUE2 : CORPS A CORPS
                if (_etatBoss == (int)ETAT_BOOS.ATTAQUE2 && _bossAction == false)
                {

                    _etatBossAttaque2 = (int)ATTAQUE2.REJOINS_POSITION;
                    _bossAction = true;
                }

                if (_etatBossAttaque2 == (int)ATTAQUE2.REJOINS_POSITION)
                {
                    _bossCyberRobot.Animation = $"allerA{_sensBoss}";
                    _bossCyberRobot.DirectionPerso = new Vector2(-1, 0);
                    _bossCyberRobot.ActualiseDeplacement(gameTime);
                    if (_bossCyberRobot.PositionPerso.X <= _perso.PositionPerso.X || _bossCyberRobot.Intersects(_perso, 5, 80))
                    {
                        _bossCyberRobot.DirectionPerso = new Vector2(0, 0);
                        _etatBossAttaque2 = (int)ATTAQUE2.REVIENS;
                    }
                }

                if (_etatBossAttaque2 == (int)ATTAQUE2.REVIENS)
                {
                    _bossCyberRobot.Animation = "allerADroite";
                    _bossCyberRobot.DirectionPerso = new Vector2(1, 0);
                    _bossCyberRobot.ActualiseDeplacement(gameTime);
                    if (_bossCyberRobot.PositionPerso.X >= GraphicsDevice.Viewport.Width - _bossCyberRobot.Perso.TextureRegion.Width / 2)
                    {
                        _etatBossAttaque2 = (int)ATTAQUE2.FIN;
                    }
                }

                if (_etatBossAttaque2 == (int)ATTAQUE2.FIN)
                {
                    _bossCyberRobot.DirectionPerso = new Vector2(0, 0);
                    _etatBossAttaque2 = (int)ATTAQUE2.INACTIF;
                    _etatBoss = (int)ETAT_BOOS.ATTENTE;
                    _bossAction = false;
                }


                // Gestion barre de vie perso
                if (_laser.Intersects(_perso, -100, 80))
                {
                    if (_perso.Touchable == true)
                    {
                        _perso.GetDamage(_vie, gameTime);
                    }
                }
                if (_perso.Touchable == false)
                {
                    _perso.Temp += deltaTime;
                    _vie.Animation = $"vieInvincible{_perso.BarreDeVie}";
                    if (_perso.Temp > 1)
                    {
                        _perso.Touchable = true;
                        _vie.Animation = $"vie{_perso.BarreDeVie}";
                        _perso.Temp = 0;
                    }
                    _vie.Actualise(gameTime);
                }

                if (_bossCyberRobot.Intersects(_perso, 0, 80) && _perso.Touchable == true)
                {
                    _bossCyberRobot.Animation = $"attaqueCaC{_sensBoss}";
                    _perso.GetDamage(_vie, gameTime);
                }


              

            }
            else
            {
                if (keyboardState.IsKeyUp(Keys.Escape) && _etatPause == (int)ETAT_PAUSE.PAUSE_DEMANDER)
                {

                    _etatPause = (int)ETAT_PAUSE.PAUSE;

                }
                if (_etatPause == (int)ETAT_PAUSE.PAUSE)
                {
                    _boutonQuitter.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - _boutonQuitter.Texture.Width / 2, GraphicsDevice.Viewport.Height / 2 - _boutonQuitter.Texture.Height / 2 + 200);
                    _boutonContinuer.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - _boutonQuitter.Texture.Width / 2, GraphicsDevice.Viewport.Height / 2 - _boutonQuitter.Texture.Height / 2 + 100);

                    if (souris.LeftButton == ButtonState.Pressed && _boutonQuitter.Contains(souris.Position))
                    {
                        _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
                    }
                    if (keyboardState.IsKeyDown(Keys.Escape) || (souris.LeftButton == ButtonState.Pressed && _boutonContinuer.Contains(souris.Position)))
                    {
                        _boutonQuitter.Position = new Vector2(1000, 1000);
                        _boutonContinuer.Position = new Vector2(1000, 1000);
                        _etatPause = (int)ETAT_PAUSE.DEMANDE_REPRENDRE;
                    }
                }

                if (_etatPause == (int)ETAT_PAUSE.DEMANDE_REPRENDRE)
                {


                    _etatPause = (int)ETAT_PAUSE.REPRENDRE;

                }
                if (keyboardState.IsKeyUp(Keys.Escape) && _etatPause == (int)ETAT_PAUSE.REPRENDRE)
                {

                    _etatPause = (int)ETAT_PAUSE.CONTINUE;

                }
            }

            _bossCyberRobot.Actualise(gameTime);
            _perso.Actualise(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Orange);
            _myGame.SpriteBatch.Begin();

            //ECRIRE ICI
            _mapRendu.Draw();
            foreach (Sprite fleche in _fleches)
            {
                fleche.Draw(_myGame.SpriteBatch);
            }
            _nomBoss.Draw(_myGame.SpriteBatch);
            _vie.Draw(_myGame.SpriteBatch);
            _vieBoss.Draw(_myGame.SpriteBatch);
            _laser.Draw(_myGame.SpriteBatch);
            _myGame.SpriteBatch.Draw(_bossCyberRobot.Perso, _bossCyberRobot.PositionPerso);
            _myGame.SpriteBatch.Draw(_perso.Perso, _perso.PositionPerso);
            _boutonQuitter.Draw(_myGame.SpriteBatch);
            _boutonContinuer.Draw(_myGame.SpriteBatch);
            _myGame.SpriteBatch.End();
        }

        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (_sol.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}
