﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] private LayerMask solidObjectsLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask grassLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask fovLayer;
    [SerializeField] private LayerMask portalLayer;
    [SerializeField] private LayerMask triggersLayer;

    public static GameLayers i { get; set; }
    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
    }

    public LayerMask SolidLayer
    {
        get => solidObjectsLayer;
    }

    public LayerMask InteractableLayer
    {
        get => interactableLayer;
    }

    public LayerMask GrassLayer
    {
        get => grassLayer;
    }

    public LayerMask PlayerLayer
    {
        get => playerLayer;
    }

    public LayerMask FovLayer
    {
        get => fovLayer;
    }

    public LayerMask PortalLayer
    {
        get => portalLayer;
    }

    public LayerMask TriggerableLayers
    {
        get => grassLayer | fovLayer | portalLayer | triggersLayer;
    }
}
