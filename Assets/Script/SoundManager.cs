using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound{
        playerMovement,
        enemyMovement,
        playerAttack,
        enemyMeleeAttack,
        enemyRangedAttack,
        playerSpellCast,
        explosiveAttack,
        battleMusic,
        environtmentAudioEffect,
        healingEffect,
        hitEffect,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneshotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void initialize(){
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.playerMovement]=0f;
    }
    public static void PlaySound(Sound sound,bool multiUse){
        if(!multiUse){
            if(CanPlaySound(sound)){
                if(oneshotGameObject==null){
                    oneshotGameObject = new GameObject("One Shot Sound");
                    oneShotAudioSource = oneshotGameObject.AddComponent<AudioSource>();
                }
                oneShotAudioSource.priority =250;
                oneShotAudioSource.volume = 0.1f;
                oneShotAudioSource.spatialBlend = 0.6f;
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }else{
            if(oneshotGameObject==null){
                    oneshotGameObject = new GameObject("One Shot Sound");
                    oneShotAudioSource = oneshotGameObject.AddComponent<AudioSource>();
                }
                oneShotAudioSource.priority =250;
                oneShotAudioSource.spatialBlend = 0.6f;
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }


    private static bool CanPlaySound(Sound sound){
        switch (sound){
            default:
                return true;
        case Sound.playerMovement:
            if(soundTimerDictionary.ContainsKey(sound)){
                float lastTimePlayed = soundTimerDictionary[sound];
                float playerMoveTimerMax = .05f;
                if(lastTimePlayed + playerMoveTimerMax<Time.time){
                    soundTimerDictionary[sound]=Time.time;
                    return true;
                }else{
                    return false;
                }
            }else{
                return true;
            }
        }
    }

    private static AudioClip GetAudioClip(Sound sound){
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray){
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound "+sound+" Not Found");
        return null;
    }
}
