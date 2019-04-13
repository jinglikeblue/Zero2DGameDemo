using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jing
{
    /// <summary>
    /// 贝塞尔移动
    /// </summary>
    public class BezierMove
    {
        Vector3 _startPoint;
        Vector3 _midPoint;
        Vector3 _endPoint;

        float _flyTime = 1f;
        float _pastTime = 0f;

        float _progress = 0f;

        public float progress
        {
            get { return _progress; }
        }

        public BezierMove(Vector3 startPos, Vector3 midPos, Vector3 endPos, float t)
        {
            _startPoint = startPos;
            _midPoint = midPos;
            _endPoint = endPos;
            _flyTime = t;
            _pastTime = 0f;
        }

        public Vector3 Step(float dt)
        {
            _pastTime += dt;
            _progress = _pastTime / _flyTime;
            if (_progress < 0)
            {
                _progress = 0;
            }
            else if (_progress > 1)
            {
                _progress = 1;
            }

            return Bezier.BezierCurve(_startPoint, _midPoint, _endPoint, _progress);
        }
    }
}
