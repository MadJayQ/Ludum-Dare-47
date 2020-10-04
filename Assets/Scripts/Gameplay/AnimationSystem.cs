using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SingletonTag(rootObject: "Gameplay Objects")]
public class AnimationSystem : MonoSingleton<AnimationSystem>
{
    [SerializeField] private Animator postFXAnimator;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    public bool CastAnimationFinished = false;

    //Our clip is the first second of the 3 second clip
    public void PlayerCloneCreateWindup(float progress)
    {
        float remappedProgress = (progress / 3f);
        postFXAnimator.SetFloat("progress", Mathf.Clamp(remappedProgress, 0f, 0.3f));
    }

    public void PlayerCastCloneCreate()
    {
        StartCoroutine(PlayCastCloneVFX());
        CastAnimationFinished = false;
    }

    public void WindDown()
    {
        StartCoroutine(AnimateWindDown());
        Time.timeScale = 1f;
    }

    private IEnumerator PlayCastCloneVFX()
    {
        float startingProgress = (1f / 3f);
        float timer = 0f;

        while (timer < 1.2f)
        {
            timer += Time.unscaledDeltaTime;
            float remappedProgress = startingProgress + (timer / 3);
            postFXAnimator.SetFloat("progress", remappedProgress);

            float timeScalePercentage = (timer / 1.2f);
            float minTimeScale = 0.25f;
            Time.timeScale = Mathf.Lerp(1f, minTimeScale, timeScalePercentage);

            yield return null;
        }

        CastAnimationFinished = true;
        yield break;
    }

    private IEnumerator AnimateWindDown()
    {
        float startingProgress = (2.2f / 3f);
        float timer = 0f;
        while(timer < 0.8f)
        {
            timer += Time.unscaledDeltaTime;
            float remappedProgress = startingProgress + (timer / 3);
            postFXAnimator.SetFloat("progress", remappedProgress);
            yield return null;
        }
        yield break;
    }
}
