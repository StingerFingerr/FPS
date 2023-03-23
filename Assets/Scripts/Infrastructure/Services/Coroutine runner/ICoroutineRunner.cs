using System.Collections;
using UnityEngine;

namespace Coroutine_runner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}