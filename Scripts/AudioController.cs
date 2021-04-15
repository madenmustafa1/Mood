using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public  Audio playerAudio;
    public bool bgMusic;
    public GameObject bgMusicGO;

    public GameObject musicBtn;
    public Sprite musicOn, musicOff;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (DataCtrl.instance.data.playMusic)
        {
            bgMusicGO.SetActive(true);
            musicBtn.GetComponent<Image>().sprite = musicOn;
            
        }
        else
        {
            bgMusicGO.SetActive(false);
            musicBtn.GetComponent<Image>().sprite = musicOff;
            
        }

    }


   public void JumpSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.jumpSound, player);
    }
    public void CoinSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.coinSound, player);
    }
    public void FireSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.fireSound, player);
    }
    public void EnemyDieSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.enemyDieSound, player);
    }
    public void KeySound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.keySound, player);
    }
    public void WaterSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.waterSound, player);
    }
    public void PlayerDieSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(playerAudio.playerDieSound, player);
    }
    public void MusicOnOff()
    {
      if(DataCtrl.instance.data.playMusic)
        {
            bgMusicGO.SetActive(false);
            musicBtn.GetComponent<Image>().sprite = musicOff;
            DataCtrl.instance.data.playMusic = false;
        }
      else
        {
            bgMusicGO.SetActive(true);
            musicBtn.GetComponent<Image>().sprite = musicOn;
            DataCtrl.instance.data.playMusic = true;
        }
    }
}
[System.Serializable]
public class Audio
{
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip fireSound;
    public AudioClip enemyDieSound;
    public AudioClip waterSound;
    public AudioClip keySound;
    public AudioClip playerDieSound;

}