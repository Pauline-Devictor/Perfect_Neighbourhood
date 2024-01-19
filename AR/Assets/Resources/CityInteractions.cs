using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//City is a gameobject in AR and I want the user to be able to :
// - zoom in/out the city
// - putting the city closer or further than them
// - rotating the city
public class CityInteractions : MonoBehaviour
{

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom in/out
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }

        //Rotate
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;
                DraggedDirection draggedDir = GetDragDirection(deltaPosition);
                if (draggedDir == DraggedDirection.Right)
                {
                    Rotate(0, deltaPosition.magnitude * 0.1f, 0);
                }
                else if (draggedDir == DraggedDirection.Left)
                {
                    Rotate(0, -deltaPosition.magnitude * 0.1f, 0);
                } 
                else if (draggedDir == DraggedDirection.Up)
                {
                    Rotate(deltaPosition.magnitude * 0.1f, 0, 0);
                } 
                else if (draggedDir == DraggedDirection.Down)
                {
                    Rotate(-deltaPosition.magnitude * 0.1f, 0, 0);
                    
                }
            }
        }

        //Move
        if (Input.touchCount == 3)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Move(touch.deltaPosition.x * 0.1f, touch.deltaPosition.y * 0.1f);
            }
        }
    }

    void Zoom(float increment)
    {
        if (increment > 0.1f || increment < -0.1f)
        {
            transform.localScale += new Vector3(increment, increment, increment);
        }
    }

    void Rotate(float X, float Y, float Z)
    {
        transform.Rotate(X, Y, Z);
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        return draggedDir;
    }

    void Move(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }
}
