using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceTriggers : MonoBehaviour
{
    public VoiceTriggers[] voiceTriggers;

    private void Awake()
    {
        voiceTriggers = FindObjectsOfType<VoiceTriggers>();
        foreach (VoiceTriggers voiceTrigger in voiceTriggers)
        {
            voiceTrigger.meshRenderer.forceRenderingOff = true;
            voiceTrigger.meshRenderer2.forceRenderingOff = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GuardOn(other));
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(GuardOff(other));
    }

    private IEnumerator GuardOn(Collider other)
    {
        List<VoiceTriggers> activeTriggers = new List<VoiceTriggers>();

        foreach (VoiceTriggers voiceTrigger in voiceTriggers)
        {
            if (other.gameObject.CompareTag("SoundTrigger"))
            {
                if (Random.value < 0.5f)
                {
                    activeTriggers.Add(voiceTrigger);
                    voiceTrigger.audioSource.Play();
                    voiceTrigger.meshRenderer.forceRenderingOff = false;
                    voiceTrigger.meshRenderer2.forceRenderingOff = false;
                }
            }
        }

        yield return new WaitForSeconds(3);

        foreach (VoiceTriggers voiceTrigger in activeTriggers)
        {
            voiceTrigger.audioSource.Stop();
            voiceTrigger.meshRenderer.forceRenderingOff = true;
            voiceTrigger.meshRenderer2.forceRenderingOff = true;
        }
    }

    private IEnumerator GuardOff(Collider other)
    {
        foreach (VoiceTriggers voiceTrigger in voiceTriggers)
        {
            if (other.gameObject.CompareTag("SoundTrigger"))
            {
                Random.Range(0, voiceTriggers.Length);
                voiceTrigger.meshRenderer.forceRenderingOff = true;
                voiceTrigger.meshRenderer2.forceRenderingOff = true;
                yield return new WaitForSeconds(3);
                voiceTrigger.audioSource.Stop();

            }
        }
    }
}
