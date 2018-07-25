using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : AudioSourceManager
{
  public AudioClip[] explosionClips, popClips;
  public AudioClip fireClip, slashClip, dryClip, fishOutClip;

  private int popCounter = 0;

  public bool PlayExplosion()
  {
    return PlayRandomClip(explosionClips, 7);
  }

  public bool PlayFishOut()
  {
    return PlayClip(fishOutClip, 2);
  }

  public bool PlayDry()
  {
    return PlayClip(dryClip, 6);
  }

  public bool PlayFire()
  {
    return PlayClip(fireClip);
  }

  public bool PlaySlash()
  {
    return PlayClip(slashClip,2);
  }


  public bool PlayPop()
  {
    AudioClip clip;

    switch (popCounter)
    {
      case 0:
        return false;
      case 1:
        clip = popClips[0];
				break;
      case 2:
        clip = popClips[1];
				break;
      case 3:
       	clip = popClips[2];
				 break;
      case 4:
        clip = popClips[3];
				break;
      default:
        clip = popClips[4];
				break;
    }

		popCounter = 0;
		return PlayClip(clip, 5);
  }

  public void SchedulePop()
  {
    popCounter++;
  }

  void Update()
  {
    PlayPop();
  }

}
