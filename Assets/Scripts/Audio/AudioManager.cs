using UnityEngine;

public static class AudioManager {

    public static AudioMaster SetAudioMaster { set { audioMaster = value; } }
    private static AudioMaster audioMaster;

    // globale functie voor het afspelen van geluiden
    public static void PlaySound(Sounds sound)
    {
        //als er geen audiomaster wordt gevonden
        if (!audioMaster)
        {
            //geef een waarschuwing en sluit de functie af
            Debug.LogWarning("Warning: no Audio Master object found");
            return;
        }
        //speel een geluid af
        audioMaster.PlaySound(sound);
    }

    //een publieke enumerator met alle geluiden
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
        Jump
    }

}
