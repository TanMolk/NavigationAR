using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class MultiAreaTarget : MonoBehaviour
{
    /// <summary>
    /// Trackable poses relative to the MultiArea root
    /// </summary>
    readonly Dictionary<string, Matrix4x4> mPoses = new Dictionary<string, Matrix4x4>();
    bool mTracked = false;

    // Start is called before the first frame update
    void Start()
    {
        var areaTargets = GetComponentsInChildren<AreaTargetBehaviour>(includeInactive: true);
        foreach (var at in areaTargets)
        {
            // Remember the relative pose of each AT to the group root node
            var matrix = GetFromToMatrix(at.transform, transform);
            mPoses[at.TargetName] = matrix;
            Debug.Log("Original pose: " + at.TargetName + "\n" + matrix.ToString(""));

            // Detach augmentation and re-parent it under the group root node
            for (int i = at.transform.childCount - 1; i >= 0; i--)
            {
                var child = at.transform.GetChild(i);
                child.SetParent(transform, worldPositionStays: true);
            }

            ShowAugmentations(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!VuforiaApplication.Instance.IsRunning)
        {
            return;
        }

        // Find current "best tracked" Area Target
        var at = GetBestTrackedAreaTarget();
        if (at == null)
        {
            if (mTracked)
            {
                mTracked = false;
                ShowAugmentations(false);
            }
            return;
        }

        if (!mTracked)
        {
            mTracked = true;
            ShowAugmentations(true);
        }

        if (GetGroupPoseFromAreaTarget(at, out Matrix4x4 groupPose))
        {
            // Set new group pose
            transform.position = groupPose.GetColumn(3);
            transform.rotation = Quaternion.LookRotation(groupPose.GetColumn(2), groupPose.GetColumn(1));
        }
    }

    void ShowAugmentations(bool show)
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var rnd in renderers)
        {
            rnd.enabled = show;
        }
    }

    AreaTargetBehaviour GetBestTrackedAreaTarget()
    {
        var trackedAreaTargets = GetTrackedAreaTargets(includeLimited: true);
        if (trackedAreaTargets.Count == 0)
        {
            return null;
        }

        // Look for EXTENDED_TRACKED targets
        // Note: EXTENDED_TRACKED status indicates normal tracking
        // for Area Targets. Area Targets are never TRACKED.
        foreach (var at in trackedAreaTargets)
        {
            if (at.TargetStatus.Status == Status.EXTENDED_TRACKED)
            {
                return at;
            }
        }

        // If no target in EXTENDED_TRACKED was found, then fallback
        // to any other target, i.e. including LIMITED ones; just
        // report the first in the list.
        return trackedAreaTargets[0];
    }

    List<AreaTargetBehaviour> GetTrackedAreaTargets(bool includeLimited = false)
    {
        var trackedTargets = new List<AreaTargetBehaviour>();
        var activeAreaTargets = FindObjectsOfType<AreaTargetBehaviour>();
        foreach (var target in activeAreaTargets)
        {
            // Note: EXTENDED_TRACKED status indicates normal tracking
            // for Area Targets. Area Targets are never TRACKED.
            if (target.enabled &&
                (target.TargetStatus.Status == Status.EXTENDED_TRACKED ||
                (includeLimited && target.TargetStatus.Status == Status.LIMITED)))
            {
                trackedTargets.Add(target);
            }
        }
        return trackedTargets;
    }

    bool GetGroupPoseFromAreaTarget(AreaTargetBehaviour atb, out Matrix4x4 groupPose)
    {
        groupPose = Matrix4x4.identity;
        if (mPoses.TryGetValue(atb.TargetName, out Matrix4x4 areaTargetToGroup))
        {
            // Matrix of group root node w.r.t. AT
            var groupToAreaTarget = areaTargetToGroup.inverse;

            // Current atb matrix
            var areaTargetToWorld = atb.transform.localToWorldMatrix;
            groupPose = areaTargetToWorld * groupToAreaTarget;
            return true;
        }
        return false;
    }

    static Matrix4x4 GetFromToMatrix(Transform from, Transform to)
    {
        var m1 = from ? from.localToWorldMatrix : Matrix4x4.identity;
        var m2 = to ? to.worldToLocalMatrix : Matrix4x4.identity;
        return m2 * m1;
    }
}