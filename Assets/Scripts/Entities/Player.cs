using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class Player : MonoBehaviour
{
    public GUIHandler gui;

    public float speed;
    public int coinsMax;
    public LayerMask spikeMask;
    public LayerMask coinMask;
    public LineRenderer trajectory;

    private List<Vector3> _targets;
    private Camera _camera;
    private CircleCollider2D _collider;
    private bool isDead;
    private int collectedCoins;
    void Start()
    {
        EnhancedTouchSupport.Enable();
        
        isDead = false;
        collectedCoins = 0;
        gui.coinsViewController.UpdateAmount(collectedCoins);
        
        _camera = Camera.main;
        _targets = new List<Vector3>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable() => TouchSimulation.Enable();

    private void OnDisable() => TouchSimulation.Disable();

    void Update()
    {
        if(!isDead)
        {
            ReceiveNewTarget();

            MoveToActiveTarget();
            
            CheckForSpikes();
            
            CheckForCoins();
        }
        
    }

    private void CheckForCoins()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(
            transform.position, 
            _collider.radius * transform.localScale.x, 
            coinMask);

        if(results.Length != 0)
        {
            collectedCoins += results.Length;

            foreach (var result in results)
                Destroy(result.gameObject);
            
            gui.coinsViewController.UpdateAmount(collectedCoins);
            
            if(collectedCoins == coinsMax)
            {
                isDead = true;
                gui.endScreenViewController.ShowVictory();
            }
        }
    }

    private void CheckForSpikes()
    {
        Collider2D[] results = new Collider2D[1];
        var amount = Physics2D.OverlapCircleNonAlloc(
            transform.position, 
            _collider.radius * transform.localScale.x, 
            results, 
            spikeMask);

        if(amount != 0)
        {
            gui.endScreenViewController.ShowDeath();
            isDead = true;
        }
    }

    private void MoveToActiveTarget()
    {
        if(_targets.Count != 0)
        {
            
            
            Vector2 move = Vector2.MoveTowards(transform.position, _targets[0], speed * Time.deltaTime);
            transform.position = move;

            if (transform.position.Equals(_targets[0]))
            {
                _targets.RemoveAt(0);

                UpdateTrajectory();
            }
        }
    }

    private void ReceiveNewTarget()
    {
        if (Touch.activeFingers.Count != 0)
        {
            Touch crrTouch = Touch.activeFingers[0].currentTouch;
            
            Vector2 sPosition = crrTouch.startScreenPosition;
            Vector2 sToW = _camera.ScreenToWorldPoint(sPosition);

            if(_targets.Count == 0)
            {
                _targets.Add(sToW);
                
                UpdateTrajectory();
            }
            
            else if(!_targets[_targets.Count - 1].Equals(sToW))
            {
                _targets.Add(sToW);
            }
        }
    }

    private void UpdateTrajectory()
    {
        Vector3 to;

        if (_targets.Count == 0)
            to = transform.position;
        else
            to = _targets[0];
        
        trajectory.SetPosition(0, transform.position);
        trajectory.SetPosition(1, to);
    }
}
