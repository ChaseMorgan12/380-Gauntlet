using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private readonly List<Camera> cameras = new();
    private Vector3[] velocities = new Vector3[4];
    private bool camerasMerged = false;
    private Rect mainCamRect;
    //private List<GameObject> players = new();

    private void Start()
    {
        StartCoroutine(CheckPositions());
        PlayerManager.PlayerConnected += AddCamera;
    }

    private void LateUpdate()
    {
        for (int index = 0; index < cameras.Count; index++)
        {
            cameras[index].transform.position = Vector3.SmoothDamp(cameras[index].transform.position,
                                                                   cameras[index].transform.parent.GetChild(0).transform.position + (Vector3.up * 10),
                                                                   ref velocities[index],
                                                                   0.25f);
        }
    }

    public void AddCamera(Camera camera)
    {
        if (cameras.Count == 0 && Camera.main)
        {
            Camera.main.gameObject.SetActive(false);
        }
        cameras.Add(camera);

        mainCamRect = cameras[0].rect; //Reset the main camera rect
    }

    public void RemoveCamera(Camera camera)
    {
        cameras.Remove(camera);

        if (cameras.Count == 0)
        {
            Camera.main.gameObject.SetActive(true);
        }
    }

    private void AddCamera(GameObject player)
    {
        AddCamera(player.transform.parent.GetComponentInChildren<Camera>());
    }

    /// <summary>
    /// Merges cameras together, first camera will take priority
    /// </summary>
    private void MergeCameras()
    {
        if (camerasMerged) return;
        camerasMerged = true;

        Camera cam = cameras[0];
        cam.rect = new Rect(0, 0, 1, 1);

        foreach(Camera badCam in cameras)
        {
            if (badCam == cam) continue;
            badCam.gameObject.SetActive(false);
        }
    }

    private void UnMergeCameras()
    {
        if (!camerasMerged) return;
        camerasMerged = false;

        Camera cam = cameras[0];
        cam.rect = mainCamRect;

        foreach(Camera newCam in cameras)
        {
            //Debug.Log("Camera active?: " + newCam.transform.gameObject.activeInHierarchy + ", " + cameras.IndexOf(newCam));
            if (newCam == cam) continue;
            newCam.gameObject.SetActive(true);
        }
    }


    private IEnumerator CheckPositions()
    {
        yield return new WaitUntil(() => cameras.Count >= 1);

        Camera mainCam = cameras[0];

        bool canMerge = true;

        foreach (Camera cam in cameras)
        {
            if (mainCam == cam) continue;

            Debug.Log(Vector3.Distance(mainCam.transform.position, cam.transform.position));
            if (Vector3.Distance(mainCam.transform.position, cam.transform.position) > 10)
            {
                canMerge = false;
                break;
            }
        }

        if (canMerge)
            MergeCameras();
        else
            UnMergeCameras();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CheckPositions());
    }
}
