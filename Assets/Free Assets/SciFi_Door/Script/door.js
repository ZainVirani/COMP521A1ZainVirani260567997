#pragma strict


function OnTriggerEnter (obj : Collider) {
	var thedoor = gameObject.FindWithTag("Threshold");
	thedoor.GetComponent.<Animation>().Play("open");
}

function OnTriggerExit (obj : Collider) {
	var thedoor = gameObject.FindWithTag("Threshold");
	thedoor.GetComponent.<Animation>().Play("close");
}