using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITargetInteface
{

    public void TargetShot()
    {
        PlayAnimation();
        PlayAudio();
    }

    public void PlayAnimation()
    {
        // 애니메이션으로 넣어야 하나아~ 파티클 넣어야지
    }

    public void PlayAudio()
    {
        // 타겟 폭파 사운드
    }


}
 