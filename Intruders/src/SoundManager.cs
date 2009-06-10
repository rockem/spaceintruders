using GameCommon.service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Intruders
{
    class SoundManager : GameService 
    {
        private AudioEngine m_AudioEngine;
        private Cue m_Music;
        private SoundBank m_SoundBank;
        private WaveBank m_WaveBank;
        private AudioCategory m_MusicCategory;
        private AudioCategory m_EffectsCategory;
        private float m_MusicVolume = 1.0f;
        private float m_EffectsVolume = 1.0f;
        private bool m_SoundEnabled = true;

        public SoundManager(Game i_Game) : base(i_Game)
        {
            
        }

        public int SoundVolume
        {
            get { return (int)(m_EffectsVolume * 100); }
            set
            {
                m_EffectsVolume = MathHelper.Clamp(value / 100.0f, 0.0f, 1.0f);
                m_EffectsCategory.SetVolume(m_EffectsVolume);
            }
        }

        public int MusicVolume
        {
            get { return (int)(m_MusicVolume * 100); }
            set
            {
                m_MusicVolume = MathHelper.Clamp(value / 100.0f, 0.0f, 1.0f);
                m_MusicCategory.SetVolume(m_MusicVolume);
            }
        }

        public bool SoundEnabled
        {
            get { return m_SoundEnabled;  }
        }

        public void ToggleSound()
        {
            if(m_SoundEnabled)
            {
                m_SoundEnabled = false;
                m_MusicCategory.SetVolume(0f);
                m_EffectsCategory.SetVolume(0f);
            }
            else
            {
                m_SoundEnabled = true;
                m_MusicCategory.SetVolume(m_MusicVolume);
                m_EffectsCategory.SetVolume(m_EffectsVolume);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            m_AudioEngine = new AudioEngine(@"Content\Audio\GameSounds.xgs");
            m_WaveBank = new WaveBank(m_AudioEngine, @"Content\Audio\Wave Bank.xwb");
            m_SoundBank = new SoundBank(m_AudioEngine, @"Content\Audio\Sound Bank.xsb");
            m_MusicCategory = m_AudioEngine.GetCategory("Music");
            m_EffectsCategory = m_AudioEngine.GetCategory("Default");
            // r_Factory.SetSoundBank(m_SoundBank);

            m_Music = m_SoundBank.GetCue("BGMusic");

            m_Music.Play();

        }

        protected override void Dispose(bool disposing)
        {
            m_AudioEngine.Dispose();
            m_WaveBank.Dispose();
            m_SoundBank.Dispose();
            base.Dispose(disposing);
        }

        public void PlayCue(string cue)
        {
            m_SoundBank.GetCue(cue).Play();
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(SoundManager), this);
        }
    }
}