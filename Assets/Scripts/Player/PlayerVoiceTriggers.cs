using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVoiceTriggers : MonoBehaviour
{
    private VoiceTriggers[] voiceTriggers;

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
        int triggersToActivate = 2;
        float triggerRadius = 5f;
        float deactivationDelay = 3f;


        Dictionary<VoiceTriggers, float> triggerDistances = new Dictionary<VoiceTriggers, float>();
        foreach (VoiceTriggers voiceTrigger in voiceTriggers)
        {
            float distance = Vector3.Distance(transform.position, voiceTrigger.transform.position);
            triggerDistances.Add(voiceTrigger, distance);
        }

        List<VoiceTriggers> sortedTriggers = triggerDistances.Keys.OrderBy(t => triggerDistances[t]).ToList();

        int activatedTriggers = 0;
        List<VoiceTriggers> activeTriggers = new List<VoiceTriggers>();
        foreach (VoiceTriggers voiceTrigger in sortedTriggers)
        {
            float distanceToTrigger = Vector3.Distance(transform.position, voiceTrigger.transform.position);
            if (distanceToTrigger <= triggerRadius && activatedTriggers < triggersToActivate)
            {
                voiceTrigger.audioSource.Play();
                voiceTrigger.meshRenderer.forceRenderingOff = false;
                voiceTrigger.meshRenderer2.forceRenderingOff = false;
                activatedTriggers++;
                activeTriggers.Add(voiceTrigger);
            }
        }

        while (activeTriggers.Count > 0)
        {
            for (int i = activeTriggers.Count - 1; i >= 0; i--)
            {
                VoiceTriggers voiceTrigger = activeTriggers[i];
                float distanceToTrigger = Vector3.Distance(transform.position, voiceTrigger.transform.position);
                if (distanceToTrigger > triggerRadius)
                {
                    yield return new WaitForSeconds(deactivationDelay);
                    voiceTrigger.audioSource.Stop();
                    voiceTrigger.meshRenderer.forceRenderingOff = true;
                    voiceTrigger.meshRenderer2.forceRenderingOff = true;
                    activeTriggers.RemoveAt(i);
                }
            }
            yield return null;
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
