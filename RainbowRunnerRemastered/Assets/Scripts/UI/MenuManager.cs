using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> canvasList = new List<GameObject>();

    Tween fadeTween;
    public float startFadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeCanvas());
    }

    // Update is called once per frame
    void Update()
    {
        // if (InputManager.Instance.GetInput())
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
        yield return new WaitForSeconds(1f);
        FadeIn(canvasList[0].GetComponent<CanvasGroup>(), 1f);
        yield return new WaitForSeconds(3f);
        FadeOut(canvasList[0].GetComponent<CanvasGroup>(), 1f);
    }
}
