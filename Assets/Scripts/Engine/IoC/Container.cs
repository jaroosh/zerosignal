using System;	
using UnityEngine;
using System.Collections.Generic;
using ZeroSignal.Engine.Interfaces.Logging;
using ZeroSignal.Engine.Logging;
using ZeroSignal.Engine;
using ZeroSignal.Engine.Interfaces.IoC;
using ZeroSignal.Engine.Exceptions;

namespace ZeroSignal.Engine.IoC {

// IoC Container.
public class Container : Singleton<Container>, IContainer  {

	private Dictionary<Type, Func<object, object>> _generators;

	public Container() {
		_generators = new Dictionary<Type, Func<object, object>>();
		_generators.Add(typeof(ILogger), (par) => par != null ? LoggerFactory.GetLogger((Type)par) : LoggerFactory.GetLogger());
	}

	  public T Resolve<T>() where T : class
        {
            var type = typeof(T);
            if (_generators.ContainsKey(type))
                return _generators[type](null) as T;
            throw new UnregisteredTypeException(type);
        }

        public T Resolve<T>(object arg) where T : class
        {
            var type = typeof(T);
            if (_generators.ContainsKey(type))
                return _generators[type](arg) as T;
            throw new UnregisteredTypeException(type);
        }

}

}
