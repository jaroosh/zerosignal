using System;	
using UnityEngine;
using System.Collections.Generic;
using ZeroSignal.Logging;
using ZeroSignal.Exceptions;

namespace ZeroSignal.IoC {

// IoC Container.
public static class Container  {

	private static Dictionary<Type, Func<object, object>> _generators;

	static Container() {
		_generators = new Dictionary<Type, Func<object, object>>();
		_generators.Add(typeof(ILogger), (par) => par != null ? LoggerFactory.GetLogger((Type)par) : LoggerFactory.GetLogger());
	}

	  public static T Resolve<T>() where T : class
        {
            var type = typeof(T);
            if (_generators.ContainsKey(type))
                return _generators[type](null) as T;
            throw new UnregisteredTypeException(type);
        }

        public static T Resolve<T>(object arg) where T : class
        {
            var type = typeof(T);
            if (_generators.ContainsKey(type))
                return _generators[type](arg) as T;
            throw new UnregisteredTypeException(type);
        }

}

}
