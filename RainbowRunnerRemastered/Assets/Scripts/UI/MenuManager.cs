using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> canvasList = new List<GameObject>();
    [SerializeField] RectTransform faderImage;

    Tween fadeTween;
    public float startFadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // FOR PROTOTYPING ONLY
        // ONLY THE LOGO CANVAS SHOULD BE ACTIVE AT RUNTIME

        canvasList[0].SetActive(true);

        for (int i = 1; i < canvasList.Count; ++i)
        {
            canvasList[i].SetActive(false);
        }

        StartCoroutine(FadeCanvas());     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeTransitionImage()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(faderImage.DOScale(1f, 1f).SetEase(Ease.InOutExpo))
            .Append(faderImage.DOScale(0f, 1f).SetEase(Ease.InOutExpo));                                
    }

    public void FadeTransition()
    {
        Tween startTransition = faderImage.DOScale(1f, 1f).SetEase(Ease.InOutExpo).OnComplete(() => ToggleActive(canvasList[1], canvasList[2]));
        Tween endTransition = faderImage.DOScale(0f, 1f).SetEase(Ease.InOutExpo);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(startTransition)
            .AppendInterval(1f)
            .Append(endTransition)
            ;
    }

    public void ToggleActive(GameObject obj1, GameObject obj2)
    {
        obj1.SetActive(false);
        obj2.SetActive(true);
    }

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
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        // Fade in the logo screen canvas
        FadeIn(canvasList[0].GetComponentInChildren<CanvasGroup>(), 1f);
        // Wait
        yield return new WaitForSeconds(3f);
        // Fade out the logo screen canvas
        FadeOut(canvasList[0].GetComponentInChildren<CanvasGroup>(), 1f);
        // Wait
        yield return new WaitForSeconds(1.5f);
        // Turn off the logo screen canvas, turn on the start screen canvas
        canvasList[0].SetActive(false);
        canvasList[1].SetActive(true);
        // Fade out the black screen to create the fade in effect when going to the start screen 
        FadeOut(canvasList[1].GetComponentInChildren<CanvasGroup>(), 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
