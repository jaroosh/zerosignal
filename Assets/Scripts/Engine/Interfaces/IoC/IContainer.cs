using UnityEngine;
using System.Collections;

namespace ZeroSignal.Engine.Interfaces.IoC {

	public interface IContainer  {
		T Resolve<T>() where T : class;
		T Resolve<T>(object arg) where T : class;
	}

}