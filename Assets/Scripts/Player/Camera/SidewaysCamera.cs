using UnityEngine;
using Tzaik.General;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Tzaik.Player.Cameras
{
    public class SidewaysCamera
    {
        public void SidewaysRight(ref float rot, float rotMax, float rate)
        {
            if (rot >= -rotMax)
                rot -= (Time.deltaTime * rate);
            else
                rot = -rotMax;
        } 

        public void WallRunSideways(ref float rot, float rotMax, bool isRight, float rate)
             => rot += isRight && rot <= rotMax ? (Time.deltaTime * rate) :
                !isRight && rot >= -rotMax ? -(Time.deltaTime * rate) : 0;
         
        public void CenterCamera(ref float rot, float rate)
        { 
            if (rot >= 0)
                rot -= Time.deltaTime * rate;
            else
                rot += Time.deltaTime * rate; 
        }

        public void SidewaysLeft(ref float rot, float rotMax, float rate)
        {
            if (rot <= rotMax)
                rot += (Time.deltaTime * rate);
            else
                rot = rotMax;
        }
    }
    }