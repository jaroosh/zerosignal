using UnityEngine;
using Exception = System.Exception;
using Type = System.Type;
using System.Collections;

namespace ZeroSignal.Engine.Exceptions {

 public class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException(Type t) : base(t.Name)
        {
        }
    }
}