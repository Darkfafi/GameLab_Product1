using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapPatternPart {

	//Trap List Maker
	List<string> _trapType = new List<string>(); //welke trap soort je wil (lijst van constants)
	List<float> _timeTillNextSpawn = new List<float>(); //hoe lang het duurt tot de volgende trapSpawn
	List<int> _amountOfTrapsInSpawn = new List<int>(); //In deze spawn hoeveel van de gekozen trap wil je hebben.


	public void AddToPart(string trapType, float timeTillNextSpawn,int amountOfTraps = 1){

		_trapType.Add (trapType);
		_timeTillNextSpawn.Add (timeTillNextSpawn);
		_amountOfTrapsInSpawn.Add (amountOfTraps);
	}
	/*
	 * public void AddPartsToPattern(TrapSpawnPattern pattern){
		pattern.trapType = _trapType;
		pattern.timeTillNextSpawn = _timeTillNextSpawn;
		pattern.amountOfTrapsinSpawn = _amountOfTrapsInSpawn;
	}
	 */
	public void ClearPatternPart(){
		_trapType.Clear ();
		_timeTillNextSpawn.Clear ();
		_amountOfTrapsInSpawn.Clear ();
	}

	public List<string> trapType{
		get{return _trapType;}
	}
	public List<float> timeTillNextSpawn{
		get{return _timeTillNextSpawn;}
	}
	public List<int> amountOfTrapsInSpawn{
		get{return _amountOfTrapsInSpawn;}
	}
	
}
