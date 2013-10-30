using UnityEngine;
using System.Collections;

public interface IContainer  {
  	T Resolve<T>() where T : class;
	T Resolve<T>(object arg) where T : class;
}
