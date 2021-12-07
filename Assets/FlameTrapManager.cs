using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tzaik.General;
using UnityEngine;
 
namespace Tzaik
{
public class FlameTrapManager : MonoBehaviour
{ 
	#region Fields
	[SerializeField] List<FireTrap> firetraps;
	[SerializeField] List<AcidTrap> acidtraps;
	[SerializeField] float firetrapcooldown;
	[SerializeField] float acidtrapcooldown;
	[SerializeField] float distanceToPlayer = 100f;
	#endregion
	
	#region Properties
	float firetraptimer;
	float acidtraptimer;
	Transform player;
	#endregion
	
	#region Unity Methods 
	void Start()
	{
		firetraptimer = 0f;
		player = GameManager.Instance.Player.transform;
	} 
	void Update()
	{
		Trapcooldown();
	}
	#endregion
	
	#region Class Methods
	private void Trapcooldown()
	{
		if(firetraptimer <= firetrapcooldown) 
			firetraptimer += Time.deltaTime; 
		else 
			CloseTraps(); 

        }

        private void CloseTraps()
        {
            var distancesFire = new Dictionary<FireTrap, float>();
            foreach (var ft in firetraps)
            {
                if (!distancesFire.ContainsKey(ft))
                    distancesFire.Add(ft, Vector3.Distance(GameManager.Instance.playerPosition, ft.transform.position));
            }
            var closefireTraps = distancesFire.OrderBy(x => x.Value).Take(4).ToDictionary(x=> x.Value, x=> x.Key);

			var distanceAcid = new Dictionary<AcidTrap, float>();
            foreach (var at in acidtraps)
            {
                if (!distanceAcid.ContainsKey(at))
                    distanceAcid.Add(at, Vector3.Distance(GameManager.Instance.playerPosition, at.transform.position));
            }
			
            var closeAcidtraps = distancesFire.OrderBy(x => x.Value).Take(4).ToDictionary(x=> x.Value, x=> x.Key);
		 	
			 foreach( var t in closeAcidtraps) 
				t.Value.SetTrigger(); 
			 foreach(var t in closefireTraps)  
				t.Value.SetTrigger(); 
			
			firetraptimer = 0f;
        }
        #endregion

        #region Coroutines
        #endregion
    }
}
