using UnityEngine;

public class AirEnemy : Enemy {
	void Update () {
        RotateTo();
        MoveTo();
        Fly();
	}

    public void Fly()
    {
        float flyspeed = 0;
        if (this.transform.position.y < 2.0f){
            flyspeed = 1.0f;
        }
        this.transform.Translate(new Vector3(0, flyspeed * Time.deltaTime,0));
    }
}
