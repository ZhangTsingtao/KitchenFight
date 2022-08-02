using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparentScript : MonoBehaviour
{
    [SerializeField] private List<IamInTheWay> currentlyInTheWay;
    [SerializeField] private List<IamInTheWay> alredyTransparent;
    [SerializeField] private Transform Player;
    private Transform Camera;


    private void Awake()
    {
        currentlyInTheWay = new List<IamInTheWay>();
        alredyTransparent = new List<IamInTheWay>();
    }
    private void Update()
    {
        GetAllObjectsInTheWay();

        MakeObjectsSolid();
        MakeObjectsTransparent();

    }

    private void GetAllObjectsInTheWay()
    {
        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(Camera.position - Player.position);

        Ray ray1_Forward = new Ray(Camera.position, Player.position - Camera.position);
        Ray ray1_Backward = new Ray(Player.position, Camera.position - Player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backward, cameraPlayerDistance);

        foreach (var hit in hits1_Forward)
        {
            if (hit.collider.gameObject.TryGetComponent(out IamInTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }

        foreach (var hit in hits1_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out IamInTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }

        }
    }
    private void MakeObjectsTransparent()
    {
        for(int i = 0; i < currentlyInTheWay.Count; i++)
        {
            IamInTheWay inTheWay = currentlyInTheWay[i];

            if (!alredyTransparent.Contains(inTheWay))
            {
                inTheWay.ShowTransparent();
                alredyTransparent.Add(inTheWay);
            }
        }
    }
    private void MakeObjectsSolid()
    {
        for(int i = alredyTransparent.Count -1; i >= 0; i--)
        {
            IamInTheWay wasInTheWay = alredyTransparent[i];

            if (!currentlyInTheWay .Contains (wasInTheWay ))
            {
                wasInTheWay.ShowSolid();
                alredyTransparent.Remove(wasInTheWay);
            }
        }
    }

}