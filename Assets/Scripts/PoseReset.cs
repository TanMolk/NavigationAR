using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
public class PoseReset : DevicePoseBehaviour
{
    public Button poseReset;

    public void Start()
    {
        poseReset.onClick.AddListener(ButtonPoseReset);
    }
    public void ButtonPoseReset()
    {
        Reset();
    }
}