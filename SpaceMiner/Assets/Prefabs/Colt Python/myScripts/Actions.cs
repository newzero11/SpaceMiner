using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action onTrashCollected;
    public static Action onPickup;
    public static Action<float, bool> onReload;
    public static Action<float, bool> onCockHammer;
    public static Action<bool> onShoot;
    public static Action<float> setBlend;
}
