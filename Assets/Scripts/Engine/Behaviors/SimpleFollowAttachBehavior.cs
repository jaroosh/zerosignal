using UnityEngine;
using System.Collections;
using ZeroSignal.Engine.Interfaces.Behaviors;

namespace ZeroSignal.Engine.Behaviors {

	public class SimpleFollowAttachBehavior : IBehavior {

		public int NextState(int currentState) { 
			
			return currentState;

		}

	}

}
