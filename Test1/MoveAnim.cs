// ================================================
//描 述:
//作 者:庄优
//创建时间:2024-10-28 20-56-03
//版 本:1.0 
// ===============================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interview
{
    public enum EaseType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
    }

    public class MoveAnim : MonoBehaviour
    {
        /// <summary>
        /// 移动物体动画基本效果
        /// </summary>
        /// <param name="gameObject">目标物体</param>
        /// <param name="begin">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="time">总时长</param>
        /// <param name="pingpong">是否是pingpong循环动画</param>
        /// <returns></returns>
        public IEnumerator Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
        {
            return MoveWithEase(gameObject, begin, end, time, pingpong, EaseType.Linear);
        }

        /// <summary>
        /// 支持ease方式的移动动画实现
        /// </summary>
        /// <param name="gameObject">目标物体</param>
        /// <param name="begin">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="time">总时长</param>
        /// <param name="pingpong">是否是pingpong循环动画</param>
        /// <param name="easeType">缓动方式</param>
        /// <returns></returns>
        public IEnumerator MoveWithEase(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, Interview.EaseType easeType)
        {
            float elapsedTime = 0f;

            while (true)
            {
                yield return StartCoroutine(MoveToPosition(gameObject, begin, end, time, easeType));
                if (pingpong)
                {
                    yield return StartCoroutine(MoveToPosition(gameObject, end, begin, time, easeType));
                }
                else
                {
                    break;
                }
            }
        }
        
        /// <summary>
        /// 核心移动逻辑
        /// </summary>
        /// <param name="gameObject">目标物体</param>
        /// <param name="begin">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="time">总时长</param>
        /// <param name="easeType">缓动方式</param>
        /// <returns></returns>
        private IEnumerator MoveToPosition(GameObject gameObject, Vector3 start, Vector3 target, float time, Interview.EaseType easeType)
        {
            float elapsedTime = 0f;

            System.Func<float, float> easingFunction = easeType switch
            {
                EaseType.Linear => Linear,
                EaseType.EaseIn => EaseIn,
                EaseType.EaseOut => EaseOut,
                EaseType.EaseInOut => EaseInOut,
                _ => Linear
            };
            
            while (elapsedTime < time)
            {
                float t = elapsedTime / time;
                float easedT = easingFunction(t);
                gameObject.transform.position = Vector3.Lerp(start, target, easedT);
                elapsedTime += Time.deltaTime;
                yield return null; // 等待下一帧
            }

            gameObject.transform.position = target; // 确保最终位置
        }
        
        private float Linear(float t)
        {
            return t;
        }
        
        private float EaseIn(float t)
        {
            return t * t; // 平方缓动
        }

        private float EaseOut(float t)
        {
            return t * (2 - t); // 反向平方缓动
        }

        private float EaseInOut(float t)
        {
            return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t; // 中间缓动
        }
        
        // =======================================TEST=========================================
        
        [SerializeField]
        private GameObject testMoveGo;
        
        [SerializeField]
        private GameObject testMoveWithEaseGo;

        private void Start()
        {
            TestDefaultMove();
            // TestPingpongMove();
            TestMoveWithEase();
        }

        private void TestDefaultMove()
        {
            if (testMoveGo == null)
            {
                return;
            }
            Vector3 startPos = Vector3.zero;
            Vector3 endPos = Vector3.one;
            StartCoroutine(Move(testMoveGo, startPos, endPos, 1.0f, false));
        }
        
        private void TestPingpongMove()
        {
            if (testMoveGo == null)
            {
                return;
            }
            Vector3 startPos = Vector3.zero;
            Vector3 endPos = Vector3.one;
            StartCoroutine(Move(testMoveGo, startPos, endPos, 1.0f, true));
        }

        private void TestMoveWithEase()
        {
            if (testMoveWithEaseGo == null)
            {
                return;
            }
            Vector3 startPos = Vector3.zero + Vector3.right;
            Vector3 endPos = Vector3.one + Vector3.right;
            StartCoroutine(MoveWithEase(testMoveWithEaseGo, startPos, endPos, 1.0f, true, Interview.EaseType.EaseInOut));
        }
    }

}
