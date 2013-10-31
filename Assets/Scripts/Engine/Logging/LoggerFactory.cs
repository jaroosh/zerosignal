using UnityEngine;
using System.Collections;
using Type = System.Type;
using ZeroSignal.Engine.Interfaces.Logging;

namespace ZeroSignal.Engine.Logging {

// Instantiazes loggers.
public static class LoggerFactory  {

	// TODO: Use pools.
	// TODO: this will get parametrized...one day.
	public static ILogger GetLogger() {
		return  new ConsoleLogger();
	}

	public static ILogger GetLogger(Type typeInfo) {
		return new ConsoleLogger(typeInfo);
	}
}

}