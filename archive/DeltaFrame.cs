using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaFrame {

    public int currentFrame;
    public int totalFrames;

    public DeltaFrame()
    {
        this.currentFrame = 0;
        this.totalFrames = 60;
    }

    public void AddFrame()
    {
        currentFrame += 1;
    }

    public void CurrentFrame()
    {
        if(currentFrame > totalFrames)
        {
            currentFrame = currentFrame % totalFrames;
        }
    }

    public void ClearFrames()
    {
        currentFrame = 0;
    }



}
