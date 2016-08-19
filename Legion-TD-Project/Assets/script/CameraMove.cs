using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Camera _camera;
	public float panSpeed = 2;
	private Vector3 dragOrigin;
	private Vector3 oldPos;

	private float zoomMin = 1f;
	private float zoomMax = 10f;
	private float zoomAmount = 1f;
	private float moveSmooth = 0.1f;


	public float x, y, width, height;


	////////
	// Awake
	void Awake() {
		// DontDestroyOnLoad( this ); if( FindObjectsOfType( GetType() ).Length > 1 ) Destroy( gameObject );
		ResetCamera();
	}

	////////
	// Start
	void Start() {
		ResetCamera();
	}

	////////////////
	// RESET: Camera
	public void ResetCamera() {
		if( _camera == null ) _camera = Camera.main;
		//// Camera Origin
		x = 0; y = 0;
		height = 2f * _camera.orthographicSize;
		width = height * _camera.aspect;
		_camera.rect = new Rect( x, y, width, height );
	}

	/////////
	// Update
	void Update() {
		if( Input.GetAxis( "Mouse ScrollWheel" ) > 0 ) {
			//// ZOOM: in
			_camera.orthographicSize = ( _camera.orthographicSize > zoomMin )
				? _camera.orthographicSize - zoomAmount
				: zoomMin;
		} else if( Input.GetAxis( "Mouse ScrollWheel" ) < 0 ) {
			//// ZOOM: out
			_camera.orthographicSize = ( _camera.orthographicSize < zoomMax )
				? _camera.orthographicSize + zoomAmount
				: zoomMax;
		};

		//// Moving of camera via click / drag
		if( Input.GetMouseButtonDown( 0 ) ) {
			dragOrigin = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 );
			dragOrigin = _camera.ScreenToWorldPoint( dragOrigin );
		};

		if( Input.GetMouseButton( 0 ) ) {
			Vector3 currentPos = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 );
			currentPos = _camera.ScreenToWorldPoint( currentPos );
			Vector3 movePos = dragOrigin - currentPos;
			transform.position += ( movePos * moveSmooth );
		};
	}

}