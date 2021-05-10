using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCCacheNuker {
	public class CacheNuker : MelonMod {

		/// <summary>
		/// Calls ClearCache when the application starts.
		/// </summary>
		public override void OnApplicationStart() {
			MelonLogger.Msg(ConsoleColor.Green, "The cache has been scheduled for deletion after this play session.");
			VRC.Core.ApiCache.ClearCache();
		}

		/// <summary>
		/// Post a message to the console to let people know that it's gonna look like VRC crashed.
		/// </summary>
		public override void OnApplicationQuit() {
			MelonLogger.Msg(ConsoleColor.Yellow, "Friendly reminder that VRChat may freeze and appear to have crashed while attempting to close. The cache is being cleared. It is advised that you do not terminate VRC during this process, which may take upwards of a couple minutes to complete for longer play sessions.");
		}

	}
}
