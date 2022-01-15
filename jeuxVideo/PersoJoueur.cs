using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class PersoJoueur
    {

        private SpriteAnime _perso;
        private bool _sensPerso;
        private int _etatSaut;
        private float _positionYDebutSaut;
        private SoundEffect _sonTirFleche;
        private SoundEffect _sonSaut;
        private SpriteAnime _vie;
        private GameTime _gameTime;
        private bool _sensDefini;


        public bool SensPerso
        {
            get
            {
                return _sensPerso;
            }

            set
            {
                _sensPerso = value;
            }
        }

        public int EtatSaut
        {
            get
            {
                return _etatSaut;
            }

            set
            {
                _etatSaut = value;
            }
        }

        public float PositionYDebutSaut
        {
            get
            {
                return _positionYDebutSaut;
            }

            set
            {
                _positionYDebutSaut = value;
            }
        }

        public SoundEffect SonTirFleche
        {
            get
            {
                return _sonTirFleche;
            }

            set
            {
                _sonTirFleche = value;
            }
        }

        public SoundEffect SonSaut
        {
            get
            {
                return _sonSaut;
            }

            set
            {
                _sonSaut = value;
            }
        }

        public GameTime GameTime
        {
            get
            {
                return _gameTime;
            }

            set
            {
                _gameTime = value;
            }
        }

        public bool SensDefini
        {
            get
            {
                return _sensDefini;
            }

            set
            {
                _sensDefini = value;
            }
        }

        internal SpriteAnime Perso
        {
            get
            {
                return _perso;
            }

            set
            {
                _perso = value;
            }
        }

        internal SpriteAnime Vie
        {
            get
            {
                return _vie;
            }

            set
            {
                _vie = value;
            }
        }


     

    
        }



    

}

