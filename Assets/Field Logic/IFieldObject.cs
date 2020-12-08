using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldObject {
	Vector2 Place {get;set;}
	bool Destroyed {get; set;}
}
