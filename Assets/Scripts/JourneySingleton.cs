using UnityEngine;
using System.Collections;

public class JourneySingleton : Singleton<JourneySingleton>  {


	protected JourneySingleton() {} // guarantee this will be always a singleton only - can't use the constructor!

}
