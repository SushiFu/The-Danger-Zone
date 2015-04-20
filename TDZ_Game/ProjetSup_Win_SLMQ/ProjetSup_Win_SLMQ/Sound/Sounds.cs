using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace ProjetSup_Win_SLMQ
{
    public enum SoundsName
    {
        dangerzone,
        darkheroe,
        die,
        arme,
        banzai,
        blaster,
        clairon,
        epee,
        explosion,
        fouet,
        fusil,
        goupillesol,
        mitrailleuse,
        ole,
        pasboss,
        reload,
        ricochet,
        rocket,
        sonar,
        spacebackground,
        yehawww,
        sniper,
        silencieux,
        FlameThrower,
        laser,
        lazergun
    }

    public class Sound
    {
        public SoundEffect[] sounds;
        public SoundEffectInstance soundPlayer;
        public bool playMusic = true;
        public bool playEffects = true;

        public Sound(ContentManager Content)
        {
            String soundspath = "Sounds/";

            sounds = new SoundEffect[30];

            sounds[0] = Tools.LoadSoundEffect(soundspath + "dangerzone", Content);
            sounds[1] = Tools.LoadSoundEffect(soundspath + "darkheroe", Content);
            sounds[2] = Tools.LoadSoundEffect(soundspath + "criwilhem", Content);
            sounds[3] = Tools.LoadSoundEffect(soundspath + "arme", Content);
            sounds[4] = Tools.LoadSoundEffect(soundspath + "banzai", Content);
            sounds[5] = Tools.LoadSoundEffect(soundspath + "blaster", Content);
            sounds[6] = Tools.LoadSoundEffect(soundspath + "clairon", Content);
            sounds[7] = Tools.LoadSoundEffect(soundspath + "epee", Content);
            sounds[8] = Tools.LoadSoundEffect(soundspath + "explosion", Content);
            sounds[9] = Tools.LoadSoundEffect(soundspath + "fouet", Content);
            sounds[10] = Tools.LoadSoundEffect(soundspath + "fusil", Content);
            sounds[11] = Tools.LoadSoundEffect(soundspath + "goupillesol", Content);
            sounds[12] = Tools.LoadSoundEffect(soundspath + "mitrailleuse", Content);
            sounds[13] = Tools.LoadSoundEffect(soundspath + "ole", Content);
            sounds[14] = Tools.LoadSoundEffect(soundspath + "pasboss", Content);
            sounds[15] = Tools.LoadSoundEffect(soundspath + "reload", Content);
            sounds[16] = Tools.LoadSoundEffect(soundspath + "ricochet", Content);
            sounds[17] = Tools.LoadSoundEffect(soundspath + "rocket", Content);
            sounds[18] = Tools.LoadSoundEffect(soundspath + "sonar", Content);
            sounds[19] = Tools.LoadSoundEffect(soundspath + "spacebackground", Content);
            sounds[20] = Tools.LoadSoundEffect(soundspath + "yehawww", Content);
            sounds[21] = Tools.LoadSoundEffect(soundspath + "sniper", Content);
            sounds[22] = Tools.LoadSoundEffect(soundspath + "silencieux", Content);
            sounds[23] = Tools.LoadSoundEffect(soundspath + "flamethrower", Content);
            sounds[24] = Tools.LoadSoundEffect(soundspath + "laser", Content);
            sounds[25] = Tools.LoadSoundEffect(soundspath + "lazergun", Content);

            sounds[26] = Tools.LoadSoundEffect(soundspath + "ambiance1", Content);
            sounds[27] = Tools.LoadSoundEffect(soundspath + "ambiance2", Content);
            sounds[28] = Tools.LoadSoundEffect(soundspath + "ambiance3", Content);
            sounds[29] = Tools.LoadSoundEffect(soundspath + "ambiance4", Content);
            soundPlayer = sounds[0].CreateInstance();
        }

        public void Play(SoundsName sound)
        {
            if (playEffects)
            {
                try
                {
                    sounds[(int)sound].Play();
                }
                catch (Exception)
                {
                }
            }
        }

        public void UpdateMusic()
        {
            if (playEffects)
            {
                if (soundPlayer.State == SoundState.Stopped)
                {
                    soundPlayer = sounds[new Random().Next(26, 30)].CreateInstance();
                    soundPlayer.Play();
                }
            }
        }
    }
}