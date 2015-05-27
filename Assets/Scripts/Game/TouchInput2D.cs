using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput2D : MonoBehaviour {

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;

	private RaycastHit2D hit;

	private bool canControl = true;

	void Start()
    {
	}



	void ToggleInputControls(){
		canControl = ! canControl;
	}

	void Update(){
		if (canControl){
			ManageInputs();
		}
	}
	
	void ManageInputs(){
		MouseInputs ();
		TouchInputs ();

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
				return;	
			}
		}
	}
		
		void MouseInputs(){
			#if UNITY_EDITOR
		
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
			
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider != null)
			{
				GameObject recipient = hit.transform.gameObject;
				touchList.Add(recipient);
			
				if(Input.GetMouseButtonDown(0))
				{
					recipient.SendMessage("OnTouchDown", hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButtonUp(0))
				{
					//GameObject.FindWithTag("Control").SendMessage("Touched", recipient,SendMessageOptions.DontRequireReceiver);
					recipient.SendMessage("OnTouchUp", hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButton(0))
				{
					recipient.SendMessage("OnTouchStay", hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}

			foreach(GameObject g in touchesOld){
                if (g != null && !touchList.Contains(g))
                {
					g.SendMessage("OnTouchExit", hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}
		
		#endif
	}

	void TouchInputs(){
				
		if (Input.touchCount > 0) {
			
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			foreach (Touch touch in Input.touches) {

				hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
				
				if(hit.collider != null)
				{

					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
					
					if(touch.phase == TouchPhase.Began){
						recipient.SendMessage("OnTouchDown", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended){
						//GameObject.FindWithTag("Control").SendMessage("Touched", recipient,SendMessageOptions.DontRequireReceiver);
						recipient.SendMessage("OnTouchUp", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
						recipient.SendMessage("OnTouchStay", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled){
						recipient.SendMessage("OnTouchExit", hit.point,SendMessageOptions.DontRequireReceiver);
					}
				}
				
			}
			
			foreach(GameObject g in touchesOld){
				if(!touchList.Contains(g)){
					g.SendMessage("OnTouchExit", hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}
	}

}
