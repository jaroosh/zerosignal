using UnityEngine;
using System.Collections;
using Type = System.Type;

namespace ZeroSignal.Logging {

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