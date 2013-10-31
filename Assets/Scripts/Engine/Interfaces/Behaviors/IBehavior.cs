using UnityEngine;
using System.Collections;

namespace ZeroSignal.Engine.Interfaces.Behaviors {

public interface IBehavior  {

	int NextState(int currentState);
	
}

}
