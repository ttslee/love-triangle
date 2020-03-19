﻿using UnityEngine.EventSystems;
using UnityEngine;

public class MultiEventSystem : EventSystem
{

    protected override void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        base.OnEnable();
    }

    protected override void Update()
    {
        EventSystem originalCurrent = EventSystem.current;
        current = this;
        base.Update();
        current = originalCurrent;
    }
}