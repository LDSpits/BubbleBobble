using UnityEngine;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour 
{
    private static AudioManager instance = null;
    private static AudioSlave bgMusic;
    private bool isPaused = false;

    //deze lijst is gevult vanuit de unity edtor en bevat alle geluiden
    public List<AudioClip> audioClips;

    //deze lijst bevat alle AudioSlaves
    private List<AudioSlave> audioSlaves = new List<AudioSlave>();

    void Start()
    {
        instance = this;
        bgMusic = new AudioSlave(this);
        audioSlaves.Add(new AudioSlave(this));
    } 
    

    public static void PlaySound(Sounds sound)
    {
        //doorzoek de lijst met AudioSlaves of er eentje vrij is
        AudioSlave slaveSource = null;
        foreach(AudioSlave obj in instance.audioSlaves)
        {
            if (!obj.isPlaying)
            {
                slaveSource = obj;
                break;
            }
        }

        //als er GEEN vrije slave gevonden is
        if (slaveSource == null)
        {
            //Creër een nieuwe AudioSlave en laat hem het geluid afspelen
            slaveSource = new AudioSlave(instance);
            slaveSource.PlayOnce(instance.audioClips[(int)sound]);

            //vernietig de AudioSlave
            Destroy(slaveSource.gameObject, instance.audioClips[(int)sound].length);
        }
        else
        {
            // speel het audiobestand af met de vrije Audioslave
            instance.audioSlaves[0].PlayOnce(instance.audioClips[(int)sound]);
        }

    }

    public static void PlayContinuous(Sounds sound)
    {
        //creëer een nieuwe AudioSlave specifiek voor het afspelen van het geluid;
        AudioSlave slave = new AudioSlave(instance); 
        slave.PlayContinuous(instance.audioClips[(int)sound]);
        
    }

    public static void PlayBackgroundMusic()
    {
        if(!instance.isPaused)
            bgMusic.PlayContinuous(instance.audioClips[(int)Sounds.BackgroundMusic]);
        else
        {
            bgMusic.UnPause();
            instance.isPaused = false;
        }

    }

    public static void PauseBackgroundMusic()
    {
        bgMusic.Pause();
        instance.isPaused = true;
    }

    public enum Sounds
    {
        BigExplosion,
        BubbleCapture,
        BubbleShot,
        Chimes,
        CollectItem1,
        CollectItem2,
        Crash,
        Death,
        ElectricBounce,
        ElectricZap,
        FireBall,
        HammerAttack,
        LaserFire,
        LetterBubblePop,
        PickupFood,
        PickupItem,
        Unknown,
        WaterFlow,
        Jump,
        BackgroundMusic
    }

    internal class AudioSlave : Object
    {
        private GameObject slave = null;
        private AudioSource source = null;

        public AudioSlave(Component thisComponent)
        {
            slave = new GameObject("audio slave");
            source = slave.AddComponent<AudioSource>();
            slave.transform.SetParent(thisComponent.gameObject.transform);
        }

        public void PlayOnce(AudioClip clip)
        {
            source.PlayOneShot(clip);
        }

        public void PlayContinuous(AudioClip clip)
        {
            source.loop = true;
            source.clip = clip;
            source.volume = 0.7f;
            source.Play();
        }

        public void Pause()
        {
            source.Pause();
        }

        public void UnPause()
        {
            source.UnPause();
        }

        public bool isPlaying { get { return source.isPlaying; } }

        public GameObject gameObject { get { return slave; } }

    }
}