using UnityEngine;

namespace Jing
{
    public class Bezier
    {
        /// <summary>  
        /// 线性贝赛尔曲线  
        /// </summary>  
        /// <param name="P0"></param>  
        /// <param name="P1"></param>  
        /// <param name="t"> 0.0 >= t <= 1.0 </param>  
        /// <returns></returns>  
        public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, float t)
        {
            if (t <= 0)
            {
                return P0;
            }
            else if (t >= 1)
            {
                return P1;
            }

            Vector3 B = Vector3.zero;
            float t1 = (1 - t);
            B = t1 * P0 + P1 * t;
            //B.y = t1*P0.y + P1.y*t;  
            //B.z = t1*P0.z + P1.z*t;  
            return B;
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="P0"></param>  
        /// <param name="P1"></param>  
        /// <param name="P2"></param>  
        /// <param name="t">0.0 >= t <= 1.0 </param>  
        /// <returns></returns>  
        public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, float t)
        {
            if (t <= 0)
            {
                return P0;
            }
            else if (t >= 1)
            {
                return P2;
            }

            Vector3 B = Vector3.zero;
            float t1 = (1 - t) * (1 - t);
            float t2 = t * (1 - t);
            float t3 = t * t;
            B = P0 * t1 + 2 * t2 * P1 + t3 * P2;
            //B.y = P0.y*t1 + 2*t2*P1.y + t3*P2.y;  
            //B.z = P0.z*t1 + 2*t2*P1.z + t3*P2.z;  
            return B;
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="P0"></param>  
        /// <param name="P1"></param>  
        /// <param name="P2"></param>  
        /// <param name="P3"></param>  
        /// <param name="t">0.0 >= t <= 1.0 </param>  
        /// <returns></returns>  
        public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
        {
            if (t <= 0)
            {
                return P0;
            }
            else if (t >= 1)
            {
                return P3;
            }

            Vector3 B = Vector3.zero;
            float t1 = (1 - t) * (1 - t) * (1 - t);
            float t2 = (1 - t) * (1 - t) * t;
            float t3 = t * t * (1 - t);
            float t4 = t * t * t;
            B = P0 * t1 + 3 * t2 * P1 + 3 * t3 * P2 + P3 * t4;
            //B.y = P0.y*t1 + 3*t2*P1.y + 3*t3*P2.y + P3.y*t4;  
            //B.z = P0.z*t1 + 3*t2*P1.z + 3*t3*P2.z + P3.z*t4;  
            return B;
        }
    }
}