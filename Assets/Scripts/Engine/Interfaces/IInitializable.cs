using UnityEngine;
using System.Collections;

namespace ZeroSignal.Engine.Interfaces {

// Used for all components that need to be initialized.
	public interface IInitializable  {
		void Initialize();
	}

}