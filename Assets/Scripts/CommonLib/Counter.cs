using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter
{
    int lim, nowCount;
    public int Limit { get { return lim; } }
    public int Now { get { return nowCount; } set { nowCount = value; } }

    public Counter(int lim, bool max = false)
    {
        Initialize(lim, max);
    }

    public bool Count(int increment = 1)
    {
        nowCount += increment;
        nowCount = nowCount > lim ? lim : nowCount;
        return OnLimit();
    }

    public bool Next(int increment = 1)
    {
        if (nowCount < lim)
        {
            nowCount += increment;
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool OnLimit()
    {
        return nowCount == lim;
    }

    public void Initialize(int newLim = -1, bool max = false)
    {
        lim = newLim == -1 ? lim : newLim;
        nowCount = max ? lim : 0;
    }
}
