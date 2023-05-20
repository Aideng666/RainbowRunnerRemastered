using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> canvasList = new List<GameObject>();
    //[SerializeField] RectTransform faderImage;

    Tween fadeTween;
    public float startFadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeCanvas());
        //faderImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }


    //public void FadeTransitionImage()
    //{
    //    Sequence sequence = DOTween.Sequence();

    //    sequence.Append(faderImage.DOScale(1f, 1f).SetEase(Ease.InOutExpo))
    //        .AppendInterval(2f)
    //        .Append(faderImage.DOScale(0f, 1f).SetEase(Ease.InOutExpo))
    //        .OnComplete(() => ToggleActive(canvasList[1], canvasList[2]));        
    //}

    //public void ToggleActive(GameObject obj1, GameObject obj2)
    //{
    //    obj1.SetActive(false);
    //    obj2.SetActive(true);
    //}

    void Fade(CanvasGroup canvasGroup, float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }

    public void FadeIn(CanvasGroup canvasGroup, float duration)
    {
        Fade(canvasGroup, 1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(CanvasGroup canvasGroup, float duration)
    {
        Fade(canvasGroup, 0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private IEnumerator FadeCanvas()
    {
        yield return new WaitForSeconds(1f);
        FadeIn(canvasList[0].GetComponentInChildren<CanvasGroup>(), 1f);
        yield return new WaitForSeconds(3f);
        FadeOut(canvasList[0].GetComponentInChildren<CanvasGroup>(), 1f);
        yield return new WaitForSeconds(1.5f);
        canvasList[0].SetActive(false);
        canvasList[1].SetActive(true);
        FadeOut(canvasList[1].GetComponentInChildren<CanvasGroup>(), 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
