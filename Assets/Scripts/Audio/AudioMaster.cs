using UnityEngine;
using System.Collections.Generic;


public class AudioMaster : MonoBehaviour 
{
    //deze lijst is gevult vanuit de unity edtor en bevat alle geluiden
    public List<AudioClip> audioClips;

    //deze lijst bevat alle AudioSlaves
    private List<GameObject> audioSlaves = new List<GameObject>();

    void Start()
    {
        AudioManager.SetAudioMaster = this;
        audioSlaves.Add(CreateAudioSlave());
    }
    
    private GameObject CreateAudioSlave()
    {
        //creër een gameobject dat een geluid afspeelt als het daarvoor een opdracht krijgt; is een child van het AudioMaster GameObject
        GameObject audioSlave = new GameObject("Audio Slave");
        audioSlave.AddComponent<AudioSource>();
        audioSlave.transform.SetParent(transform);

        return audioSlave;
    }

    public void PlaySound(AudioManager.Sounds sound)
    {
        AudioSource slaveSource = null;
        foreach(GameObject obj in audioSlaves)
        {
            if (!obj.GetComponent<AudioSource>().isPlaying)
            {
                slaveSource = obj.GetComponent<AudioSource>();
                break;
            }
        }

        //als de eerste AudioSlave bezig is
        if (slaveSource == null)
        {
            //Creër een nieuwe AudioSlave en laat hem het geluid afspelen
            slaveSource = CreateAudioSlave().GetComponent<AudioSource>();
            slaveSource.PlayOneShot(audioClips[(int)sound]);

            //vernietig de AudioSlave
            Destroy(slaveSource.gameObject, audioClips[(int)sound].length);
        }
        else
        {
            // speel het audiobestand af met de vrije Audioslave
            audioSlaves[0].GetComponent<AudioSource>().PlayOneShot(audioClips[(int)sound]);
        }

    }

    public void PlayContinuous(AudioManager.Sounds sound)
    {
        //creëer een nieuwe AudioSlave specifiek voor het afspelen van het geluid;
        AudioSource aud = CreateAudioSlave().GetComponent<AudioSource>();
        aud.clip = audioClips[(int)sound];
        aud.Play();
    }

}

