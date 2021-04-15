using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LevelController : MonoBehaviour
{
    public Button nextButton;
    public Sprite goldStar;
    public Image star1, star2, star3;
    public Text txtScore;

    public int levelnum;
    public int score;
    public int score3Star;
    public int score2Star;
    public int score1Star;
    public int nextLevelScore;
    public float startDelayAnim;
    public float delayAnim;

    private bool show2Star, show3Star;

    void Start()
    {
        //score = GameController.instance.GetScore();
        txtScore.text = "" + score; 
        if(score >= score3Star)
        {
            show3Star = true;
            Invoke("GoldStarAnim", startDelayAnim);
        }
        if( score >= score2Star && score < score3Star)
        {
            show2Star = true;
            Invoke("GoldStarAnim", startDelayAnim);
        }
        if(score !=0 && score <=score1Star)
        {
            Invoke("GoldStarAnim", startDelayAnim);
        }
    }

    private void GoldStarAnim()
    {
        StartCoroutine("FirstStarAnim", star1);
    }
    IEnumerator FirstStarAnim(Image starImg)
    {

        ShowAnim(starImg);

        yield return new WaitForSeconds(delayAnim);

        if (show2Star || show3Star)
        {
            StartCoroutine("SecondStarAnim", star2);
        }
        else
            Invoke("CheckStatus", 2f);

    }
    IEnumerator SecondStarAnim(Image starImg)
    {
        ShowAnim(starImg);
        show2Star = false;

        yield return new WaitForSeconds(delayAnim);
        if(show3Star)
        {
            StartCoroutine("ThirdStarAnim", star3);
        }
        else
            Invoke("CheckStatus", 2f);
    }
    IEnumerator  ThirdStarAnim(Image starImg)
    {
        ShowAnim(starImg);
        yield return new WaitForSeconds(delayAnim);
        show3Star = false;     
        Invoke("CheckStatus", 2f);
    }

    private void ShowAnim(Image starImg)
    {
        starImg.rectTransform.sizeDelta = new Vector2(120f, 120f);
        starImg.sprite = goldStar;

        RectTransform temp = starImg.rectTransform;

        //temp.sizeDelta(new Vector2(100f, 100f),0.5f ,false);

        EffectController.instance.ShowCoinEffect(starImg.transform.position);
        AudioController.instance.KeySound(starImg.transform.position);
      
    }

    private void CheckStatus()
    {
        if(score >= nextLevelScore)
        {
            nextButton.interactable = true;

            EffectController.instance.ShowCoinEffect(nextButton.transform.position);
            AudioController.instance.KeySound(nextButton.transform.position);

            GameController.instance.UnlockLevel(levelnum);

        }       
        else
        {
            nextButton.interactable = false;
        }
    }

    void Update()
    {
        
    }
}
