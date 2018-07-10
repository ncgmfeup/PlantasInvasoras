using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioSourceManager : MonoBehaviour
{

	private class AudioSlot {
		public AudioSource source;
		public int priority;

		public AudioSlot(AudioSource source, int priority = 0){
			this.source = source;
			this.priority = priority;
		}
	}
  private AudioSlot[] audioSlots;

  void Start()
  {
    AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSlots = new AudioSlot[audioSources.Length];
		for(int i = 0; i < audioSources.Length; 	i++)
			audioSlots[i] = new AudioSlot(audioSources[i]);
  }

  public bool PlayClip(AudioClip clip, int priority = 0)
  {
		AudioSlot slot = GetFreeSlot(priority);
		if (slot != null){
			slot.priority = priority;
			slot.source.clip = clip;
			slot.source.Play();
			return true;
		}
		return false;
  }

	public bool PlayRandomClip(AudioClip[] clips, int priority = 0)
  {
		if(clips.Length == 0)
			return false;
		int chosenClipIndex = Random.Range(0, clips.Length);
    return PlayClip(clips[chosenClipIndex], priority);
  }

	private bool IsFree(AudioSource source){
		return !source.isPlaying;
	}

	private AudioSlot GetFreeSlot(int priority = 0){
		foreach(AudioSlot slot in audioSlots)
			if (IsFree(slot.source) || slot.priority < priority)
				return slot;
		return null;
	}

	protected void SetPitch(float pitch, int slotId = 0){
		audioSlots[slotId].source.pitch = pitch;
	}
}
